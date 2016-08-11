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


public partial class RaceSetup : System.Web.UI.Page
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
                lblNemaVozac.Text = "Немате ангажирано возач и не можете да возите!";
                lblNemaVozac.Visible = true;
                return;
            }
            statusBudzetManager();
            deloviKoiImaSetup();
            ispolniPodatociSetupRelated();
            ispolniPodatociNONSetupRelated();
            ispolniGumi();
            if (imaIzvozeno())
                btnDrive.Enabled = false;

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
            if (imaIzvozeno())
                btnDrive.Enabled = false;
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
            vozi();
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("RaceSetup.aspx");
        }

    }

    private void vozi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "INSERT INTO racesetup VALUES ((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), :men_id, :vreme, :gumi, :gorivo, :rizikpreteknuvanje, :rizikbranenje, :rizikslobodno)";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":vreme", (presmetajBrzina(Int32.Parse(txtSlobodno.Text))*60));
        command.Parameters.AddWithValue(":gumi", Int32.Parse(ddlGumi.SelectedValue));
        command.Parameters.AddWithValue(":gorivo", Int32.Parse(txtGorivo.Text));
        command.Parameters.AddWithValue(":rizikpreteknuvanje", Int32.Parse(txtPreteknuvanje.Text));
        command.Parameters.AddWithValue(":rizikbranenje", Int32.Parse(txtBranenje.Text));
        command.Parameters.AddWithValue(":rizikslobodno", Int32.Parse(txtSlobodno.Text));


        command.ExecuteNonQuery();
        lista = new ArrayList();
        deloviKoiImaSetup();
        for (int i = 0; i < lista.Count; i++)
        {
            TextBox tb = gvVezbiSetup.Rows[i].Cells[3].FindControl("TextBox1") as TextBox;
            OracleCommand command1 = new OracleCommand();
            command1.Connection = connect;
            command1.CommandText = "INSERT INTO racesetuppodesuvanja VALUES ((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), :men_id, :del_id, :podesuvanje)";
            command1.Parameters.AddWithValue(":men_id", menId);
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

  
    private bool imaIzvozeno()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT men_id FROM racesetup WHERE (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) AND men_id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

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
    
}