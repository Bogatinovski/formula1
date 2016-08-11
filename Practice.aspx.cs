using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Collections;
using System.Text.RegularExpressions;

public partial class user_Practice : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    protected bool imaPrvaKval;
    protected bool imaVtoraKval;
    private ArrayList lista;
    private ArrayList listaZaKomentari;
    private Hashtable komentari;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        if (!Page.IsPostBack)
            initialize();
        else
        {
            initializePostBack();
        }

        

    }

    private void initialize()
    {
        if (!otvoriKonekcija())
        {
            //ima problem so konekcijata
            return;
        }
        try
        {
            if (nemaVozac())
            {
                lblNemaVozac.Text = "Немате ангажирано возач и не можете да возите ни вежби ни квалификации!";
                lblNemaVozac.Visible = true;
                return;
            }
            statusBudzetManager();
            deloviKoiImaSetup();
            ispolniPodatociSetupRelated();
            ispolniPodatociNONSetupRelated();
            ispolniGumi();
            ispolniRizici();
            lblIzvozeniKrugovi.Text = brojNaIzraboteniVezbi().ToString();
            if (lblIzvozeniKrugovi.Text.Equals("5"))
                btnDrive.Enabled = false;
            if (!lblIzvozeniKrugovi.Text.Equals("0"))
                prikaziDosegasniVezbi();
            imaPrvaKval = imaIzvozenoQual(1);
            imaVtoraKval = imaIzvozenoQual(2);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Label2.Text = ex.Message;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void prikaziDosegasniVezbi()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Круг", typeof(string)),
                            new DataColumn("Време", typeof(string))
                            });

        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT ime FROM delovi d, setupdelovi s WHERE d.id = s.id ORDER BY s.id";
        
        OracleDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            dt.Columns.Add(new DataColumn(reader["ime"].ToString(), typeof(string)));
        }
        

        dt.Columns.Add(new DataColumn("Гуми", typeof(string)));
        
        command.Connection = connect;
        command.CommandText = "SELECT brojvezba, vreme, ime, komentar_id, podesuvanje FROM vezbi v natural join vezbipodesuvanja vp join tipovigumi g on v.gumi = g.id WHERE men_id = :men_id ORDER BY brojvezba";
        command.Parameters.AddWithValue(":men_id", menId);

        komentari = new Hashtable();
        ArrayList komList = new ArrayList();
        int i = 0;
        reader = command.ExecuteReader();
        DataRow dr = dt.NewRow();
        int j = 2;
        while (reader.Read())
        {
            if (i != Int32.Parse(reader["brojvezba"].ToString()) && i == 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(Double.Parse(reader["vreme"].ToString()));

                string answer = string.Format("{1:D2}:{2:D2}.{3:D3}", 
    			                t.Hours, 
    			                t.Minutes, 
    			                t.Seconds, 
    			                t.Milliseconds);
                dr[0] = reader["brojvezba"].ToString();
                dr[1] = answer;
                dr[8] = reader["ime"].ToString();
                i = Int32.Parse(reader["brojvezba"].ToString());
                

            }
            else
                if(i != Int32.Parse(reader["brojvezba"].ToString()))
            {
                komentari.Add(i.ToString(), komList);
                komList = new ArrayList();

                dt.Rows.Add(dr.ItemArray);
                dr = dt.NewRow();
                TimeSpan t = TimeSpan.FromSeconds(Double.Parse(reader["vreme"].ToString()));

                string answer = string.Format("{1:D2}:{2:D2}.{3:D3}",
                                t.Hours,
                                t.Minutes,
                                t.Seconds,
                                t.Milliseconds);
                dr[0] = reader["brojvezba"].ToString();
                dr[1] = answer;
                dr[8] = reader["ime"].ToString();
                i = Int32.Parse(reader["brojvezba"].ToString());
                j = 2;

                
            }
            dr[j] = reader["podesuvanje"].ToString();
            komList.Add(reader["komentar_id"].ToString());
            j++;
        }
        dt.Rows.Add(dr.ItemArray);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        
    }

    private void initializePostBack()
    {
        if (!otvoriKonekcija())
        {
            //ima problem so konekcijata
            return;
        }
        try
        {
           
            deloviKoiImaSetup();
            
            
            lblIzvozeniKrugovi.Text = brojNaIzraboteniVezbi().ToString();
            if (lblIzvozeniKrugovi.Text.Equals("5"))
                btnDrive.Enabled = false;
            imaPrvaKval = imaIzvozenoQual(1);
            imaVtoraKval = imaIzvozenoQual(2);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Label2.Text = ex.Message;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void ispolniPodatociSetupRelated()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT d.ime, b.nivo, b.istrosenost FROM setupdelovi s, delovi d, bolid_delovi b WHERE b.del_id = d.id AND d.id = s.id AND b.id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "SetupRealtedParts");

        gvVezbiSetup.DataSource = ds;
        gvVezbiSetup.DataBind();
    }

    private void ispolniPodatociNONSetupRelated()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT d.ime, b.nivo, b.istrosenost FROM delovi d, bolid_delovi b WHERE b.del_id = d.id AND b.id = :men_id AND d.id NOT IN (SELECT id FROM setupdelovi) ORDER BY d.id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "SetupNonRealtedParts");

        gvVezbiNonSetup.DataSource = ds;
        gvVezbiNonSetup.DataBind();
    }

    private void ispolniGumi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT * FROM tipovigumi ORDER BY id";


        OracleDataReader reader = command.ExecuteReader();
        List<ListItem> listagumi = new List<ListItem>();
        while (reader.Read())
        {
            listagumi.Add(new ListItem(reader["ime"].ToString(), reader["id"].ToString()));
        }
        ddlGumi.DataTextField = "Text";
        ddlGumi.DataValueField = "Value";
        ddlGumi.DataSource = listagumi;
        ddlGumi.DataBind();

    }


    private void ispolniRizici()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT * FROM rizik ORDER BY id";


        OracleDataReader reader = command.ExecuteReader();
        List<ListItem> listarizici = new List<ListItem>();
        while (reader.Read())
        {
            listarizici.Add(new ListItem(reader["ime"].ToString(), reader["id"].ToString()));
        }
        ddlRizik.DataTextField = "Text";
        ddlRizik.DataValueField = "Value";
        ddlRizik.DataSource = listarizici;
        ddlRizik.DataBind();

    }

    private void odzemiOdBudezet()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - :cena WHERE id = :men_id";
        //command.Parameters.AddWithValue(":cena", convertToNumber(lblVkupnoPlakjanje.Text));
        //command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private bool otvoriKonekcija()
    {
        try
        {
            connect = new OracleConnection();
            connect.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;

            if (connect != null)
            {
                connect.Open();
                return true;
            }
            else
                return false;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private void zatvoriKonekcija()
    {
        try
        {
            connect.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected void checkLoggedUser()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            menId = Convert.ToInt32(Session["user"].ToString());
        }
    }

    protected string convertToCurrency(int broj)
    {
        return string.Format("{0:C0}", broj);
    }

    protected string convertToCurrency(string broj)
    {
        int br = Int32.Parse(broj);
        return string.Format("{0:C0}", br);
    }

    protected int convertToNumber(string broj)
    {
        return Int32.Parse(broj.Replace(",", "").Replace("$", ""));
    }
    protected void btnDrive_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
        {
            //problem so konekcija
            return;
        }
        try
        {
            if (radioType.SelectedValue.Equals("0"))
            {
                voziVezba();
            }
            else
                if (radioType.SelectedValue.Equals("1"))
                {
                    voziKvalifikaci(1);
                }
                else
                    if (radioType.SelectedValue.Equals("2"))
                    {
                        voziKvalifikaci(2);
                    }
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("Practice.aspx");
        }

    }

    private void voziVezba()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "INSERT INTO vezbi VALUES ((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), :men_id, :brojvezba, :vreme, :gumi)";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":brojvezba", (Int32.Parse(lblIzvozeniKrugovi.Text) + 1));
        command.Parameters.AddWithValue(":vreme", presmetajBrzina(0));
        command.Parameters.AddWithValue(":gumi", Int32.Parse(ddlGumi.SelectedValue));

        command.ExecuteNonQuery();
        lista = new ArrayList();
        deloviKoiImaSetup();
        for (int i = 0; i < lista.Count; i++)
        {
            TextBox tb = gvVezbiSetup.Rows[i].Cells[3].FindControl("TextBox1") as TextBox;
            OracleCommand command1 = new OracleCommand();
            command1.Connection = connect;
            command1.CommandText = "INSERT INTO vezbipodesuvanja VALUES ((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), :men_id, :brojvezba, :del_id, (SELECT id FROM (SELECT id FROM komentari WHERE del_id = :del_id AND povekje = :komentar ORDER BY round(dbms_random.value() * 8)) x WHERE rownum = 1), :podesuvanje)";
            command1.Parameters.AddWithValue(":men_id", menId);
            command1.Parameters.AddWithValue(":brojvezba", (Int32.Parse(lblIzvozeniKrugovi.Text) + 1));
            command1.Parameters.AddWithValue(":del_id", Int32.Parse(lista[i].ToString()));
            command1.Parameters.AddWithValue(":komentar", Int32.Parse(listaZaKomentari[i].ToString()));
            command1.Parameters.AddWithValue(":podesuvanje", Int32.Parse(tb.Text));

            command1.ExecuteNonQuery();
        }
    }

    private void voziKvalifikaci(int kvalBr)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "INSERT INTO kvalifikaci VALUES ((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), :men_id, :tip, :vreme, :gumi)";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":tip", kvalBr);
        command.Parameters.AddWithValue(":vreme", presmetajBrzina(Int32.Parse(ddlRizik.SelectedValue)));
        command.Parameters.AddWithValue(":gumi", Int32.Parse(ddlGumi.SelectedValue));

        command.ExecuteNonQuery();
        lista = new ArrayList();
        deloviKoiImaSetup();
        for (int i = 0; i < lista.Count; i++)
        {
            TextBox tb = gvVezbiSetup.Rows[i].Cells[3].FindControl("TextBox1") as TextBox;
            OracleCommand command1 = new OracleCommand();
            command1.Connection = connect;
            command1.CommandText = "INSERT INTO kvalifikacipodesuvanja VALUES ((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), :men_id, :tip, :del_id, :podesuvanje)";
            command1.Parameters.AddWithValue(":men_id", menId);
            command1.Parameters.AddWithValue(":tip", kvalBr);
            command1.Parameters.AddWithValue(":del_id", Int32.Parse(lista[i].ToString()));
            command1.Parameters.AddWithValue(":podesuvanje", Int32.Parse(tb.Text));

            command1.ExecuteNonQuery();
        }
    }

    private double presmetajBrzina(int rizik)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT b.moknost, b.upravuvanje, b.zabrzuvanje, v.vkupna_jacina, p.brzina, obj.vkupno objvkupno, per.vkupno pervkupno " +
                                "FROM bolid b, vozacistorija i, vozac v, personal per, objekt obj, pateka p, trka t " +
                                "WHERE b.id = i.men_id AND i.vozac_id = v.id AND b.id = per.id AND obj.id = per.id AND b.id = :men_id " +
                                "AND i.end_contract_race IS NULL AND p.id = t.pateka AND t.sledna = 1";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        double suma = 0.0;
        if (reader.Read())
        {
            suma = Double.Parse(reader["brzina"].ToString()) + (300.0 / (Double.Parse(reader["moknost"].ToString()) + Double.Parse(reader["upravuvanje"].ToString()) + Double.Parse(reader["zabrzuvanje"].ToString()))) + (800.0 / Double.Parse(reader["vkupna_jacina"].ToString()));
            suma += (0.2 * Double.Parse(ddlGumi.SelectedValue));
            suma += (400.0 / (Double.Parse(reader["pervkupno"].ToString())) + Double.Parse(reader["objvkupno"].ToString()));
            suma += zaostanuvanjePoradiSetup();
            Random r = new Random();
            double rand = r.Next(-5, 5);
            if (rizik != 0)
            {
                Random ran = new Random();
                double random = r.Next((-1 * rizik), rizik);
                suma += (0.3 * random);
            }
            suma += (1 / rand); //random nekoe zaostanuvanje
        }

        return suma;
    }

    private int brojNaIzraboteniVezbi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT COUNT(*) count FROM vezbi WHERE (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) AND men_id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();

        reader.Read();
        return Int32.Parse(reader["count"].ToString());
    }

    protected void radioType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if ((radioType.SelectedValue.Equals("0") && lblIzvozeniKrugovi.Text.Equals("5")) || (radioType.SelectedValue.Equals("1") && imaPrvaKval) || (radioType.SelectedValue.Equals("2") && imaVtoraKval))
        {
            //ako gi ima izvozeno site vezbi, ili ima izvozeno kval1 ili kval2
            btnDrive.Enabled = false;
        }
        else
        {
            btnDrive.Enabled = true;
        }

        if (radioType.SelectedValue.Equals("0"))
        {
            //ako e vezbanje nema rizik
            ddlRizik.Enabled = false;
        }
        else
        {
            //kvalifikaci se
            ddlRizik.Enabled = true;
        }
    }

    private bool imaIzvozenoQual(int kvalBr)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT men_id count FROM kvalifikaci WHERE (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) AND men_id = :men_id AND tip = :tip";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":tip", kvalBr);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
            return true;
        else
            return false;
    }

    private bool nemaVozac()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT men_id FROM vozacistorija WHERE men_id = :men_id AND end_contract_race IS NULL";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
            return true;
        return false;
    }

    private void deloviKoiImaSetup()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT id FROM setupdelovi ORDER BY id";

        OracleDataReader reader = command.ExecuteReader();
        lista = new ArrayList();
        while (reader.Read())
        {
            lista.Add(reader["id"].ToString());
        }
    }

    private double zaostanuvanjePoradiSetup()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT * FROM podesuvanjapateka WHERE pateka = (SELECT p.id FROM trka t, pateka p WHERE t.pateka = p.id AND t.sledna = 1) ORDER BY del_id";

        OracleDataReader reader = command.ExecuteReader();
        listaZaKomentari = new ArrayList();
        double vrati = 0.0;
        int i = 0;
        while (reader.Read())
        {
            TextBox tb = gvVezbiSetup.Rows[i].Cells[3].FindControl("TextBox1") as TextBox;
            int setup = Int32.Parse(tb.Text);
            int potrebnoPodesuvanje = Int32.Parse(reader["podesuvanje"].ToString());
            int razlika = setup - potrebnoPodesuvanje;
            if (razlika < -60)
                listaZaKomentari.Add(0);
            else
                if (razlika > 60)
                    listaZaKomentari.Add(1);
                else
                    listaZaKomentari.Add(2);
            vrati += Math.Abs(razlika) / 150;
            lblNemaVozac.Visible = true;
            lblNemaVozac.Text = i.ToString();
            i++;


        }
        return vrati;
    }

    private void statusBudzetManager()
    {
        OracleCommand command1 = new OracleCommand();
        command1.Connection = connect;
        command1.CommandText = "SELECT ime || ' ' || prezime as name, budzet FROM menadzer WHERE id = :men_id";
        command1.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader1 = command1.ExecuteReader();
        if (reader1.Read())
        {
            lblBudzetStatus.Text = convertToCurrency(reader1["budzet"].ToString());
            lblManagerStatus.Text = reader1["name"].ToString();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        prikaziKomentari(Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
    }

    private void prikaziKomentari(int vezba){
        if (!otvoriKonekcija())
        {
            return;
        }
        try
        {
            OracleCommand command1 = new OracleCommand();
            command1.Connection = connect;
            command1.CommandText = "SELECT k.tekst, d.ime FROM vezbipodesuvanja v, komentari k, delovi d WHERE v.komentar_id = k.id AND d.id = v.del_id AND v.men_id = :men_id AND brojvezba = :brojvezba ORDER BY d.id";
            command1.Parameters.AddWithValue(":men_id", menId);
            command1.Parameters.AddWithValue(":brojvezba", vezba);
            comments.InnerHtml = "";
            OracleDataReader reader = command1.ExecuteReader();
            if (!reader.HasRows)
                comments.InnerHtml += "<p style=\"color: green;\">Возачот нема никакви забелешки!</p>"; 
            while (reader.Read())
            {
                comments.InnerHtml += "<p><span style=\"color: #FFA500;\">" + reader["ime"].ToString() + ":</span> " + reader["tekst"].ToString() + "</p>";
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            zatvoriKonekcija();
        }
       
    }
}