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

public partial class user_DriverTraining : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    private int vozacId;
    private ArrayList listaTreninzi;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
       
            if (!imaVozac())
            {
                vozacInfo.Visible = false;
                nemaVozac.Visible = true;
            }
            else
            {
                //ima vozac
                if (!Page.IsPostBack)
                {
                izlistajGiTreninzite();
                negativenBalans();
                }
            }
        
        

    }

    private bool imaVozac()
    {
        OracleCommand command = new OracleCommand();
        if (!otvoriKonekcija())
        {
            //ima problem so konekcijata
        }
        try
        {
            command.Connection = connect;
            command.CommandText = "SELECT v.* FROM vozacistorija i, vozac v WHERE men_id = :men_id AND end_contract_season IS NULL AND i.vozac_id = v.id";
            command.Parameters.AddWithValue(":men_id", menId);
            lblMotivation.Text = menId.ToString();
            OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ispecatiAtributiNaVozac(reader);
                statusBudzetManager();
                vozacId = Convert.ToInt32(reader["id"].ToString());
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //lblNacionalnost.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
        return false;
    }

    private void izlistajGiTreninzite()
    {
        OracleCommand command = new OracleCommand();
        if (!otvoriKonekcija())
        {
            //ima problem so konekcijata
        }
        try
        {
            command.Connection = connect;
            command.CommandText = "SELECT ime, cena FROM vozac_tipovi_trening ORDER BY id";

            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Trening");
            gvTrening.DataSource = ds;
            gvTrening.DataBind();
            
            listaTreninzi = new ArrayList();

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listaTreninzi.Add(reader["ime"].ToString());
            }
            ddlTreninzi.DataSource = listaTreninzi;
            ddlTreninzi.DataBind();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            lblOsvoeniPoeni.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
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

    private void ispecatiAtributiNaVozac(OracleDataReader reader)
    {
        lblIme.Text = reader["ime"].ToString();
        lblNacionalnost.Text = reader["nacionalnost"].ToString();
        lblTituli.Text = reader["tituli"].ToString();
        lblBrTrki.Text = reader["brtrki"].ToString();
        lblPobedi.Text = reader["pobedi"].ToString();
        lblPodiumi.Text = reader["podiumi"].ToString();
        lblOsvoeniPoeni.Text = reader["poeni"].ToString();
        lblNajbrzKurg.Text = reader["najbrzikrugovi"].ToString();
        lblPolPozicii.Text = reader["polpozicii"].ToString();
        lblAvgPtsPerRace.Text = reader["prosekpoeni"].ToString();

        lblOA.Text = reader["vkupna_jacina"].ToString();
        lblConcentration.Text = reader["koncentracija"].ToString();
        lblTalent.Text = reader["talent"].ToString();
        lblAggresiveness.Text = reader["agresivnost"].ToString();
        lblExperience.Text = reader["iskustvo"].ToString();
        lblTI.Text = reader["tehnika"].ToString();
        lblStamina.Text = reader["izdrzlivost"].ToString();
        lblMotivation.Text = reader["motivacija"].ToString();
        lblCharisma.Text = reader["harizma"].ToString();

        lblWeight.Text = reader["tezina"].ToString();
        lblAge.Text = reader["godini"].ToString();
    }

    protected void btnTreninrajVozac_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
        {
            //ima problem so konekcijata
            return;
        }
        try
        {
            OracleDataReader readerVrednosti = prezemiTreningVrednosti();//gi zema site varjanti od daden tip
            
            int brNaTreninzi = brNaTreninziOdTip();
            Random random = new Random();
            int treningBr = random.Next(brNaTreninzi);

            int i = 0;
            while (true)
            {
                readerVrednosti.Read();
                if (i == treningBr)
                    break;
                i++;
            }

            izmeniVrednostiVozac(readerVrednosti, treningBr);
            presmetajOA();
            odzemiOdBudget(Convert.ToInt32(readerVrednosti["cena"].ToString()));
            setirajNulaAkoNegativen(vozacId);


            Response.Redirect("DriverTraining.aspx");

            


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally{
            zatvoriKonekcija();
        }
    }

    private OracleDataReader prezemiTreningVrednosti(){
        //gi zema vrednostite za treninzi od daden tip
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT t.*, tip.cena FROM trening_vozac t, vozac_tipovi_trening tip WHERE t.tip_id=tip.id AND tip.ime = :ime";
        command.Parameters.AddWithValue(":ime", ddlTreninzi.SelectedItem.Text);
        
        OracleDataReader reader = command.ExecuteReader();
        return reader;
    }

    private void izmeniVrednostiVozac(OracleDataReader reader, int treningBr)
    {
        
        //mu se dodavata vrednostite od treningot na vozacot
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE vozac SET koncentracija = koncentracija + :koncentracija, " + 
                                "agresivnost = agresivnost + :agresivnost, tehnika = tehnika + :tehnika, " + 
                                "izdrzlivost = izdrzlivost + :izdrzlivost, motivacija = motivacija + :motivacija, " + 
                                "harizma = harizma + :harizma, tezina = tezina + :tezina WHERE id = :id";
        command.Parameters.AddWithValue(":koncentracija", Convert.ToInt32(reader["koncentracija"].ToString()));
        command.Parameters.AddWithValue(":agresivnost", Convert.ToInt32(reader["agresivnost"].ToString()));
        command.Parameters.AddWithValue(":tehnika", Convert.ToInt32(reader["tehnika"].ToString()));
        command.Parameters.AddWithValue(":izdrzlivost", Convert.ToInt32(reader["izdrzlivost"].ToString()));
        command.Parameters.AddWithValue(":motivacija", Convert.ToInt32(reader["motivacija"].ToString()));
        command.Parameters.AddWithValue(":harizma", Convert.ToInt32(reader["harizma"].ToString()));
        command.Parameters.AddWithValue(":tezina", Convert.ToInt32(reader["tezina"].ToString()));
        command.Parameters.AddWithValue(":id", vozacId);

        command.ExecuteNonQuery();
    }

    private int brNaTreninziOdTip()
    {
        //se zema kolku varijanti ima od daden tip
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT COUNT(*) as \"count\" FROM trening_vozac t, vozac_tipovi_trening tip WHERE t.tip_id=tip.id AND tip.ime = :ime GROUP BY t.tip_id";
        command.Parameters.AddWithValue(":ime", ddlTreninzi.SelectedItem.Text);

        OracleDataReader reader = command.ExecuteReader();
        reader.Read();
        return Convert.ToInt32(reader["count"].ToString());
    }

    private void presmetajOA()
    {
        OracleDataReader reader = prezemiVozacAtributi();
        reader.Read();
        int suma = 0;
        suma += Convert.ToInt32(reader["koncentracija"].ToString()) * 8;
        suma += Convert.ToInt32(reader["talent"].ToString()) * 12;
        suma += Convert.ToInt32(reader["agresivnost"].ToString()) * 7;
        suma += Convert.ToInt32(reader["iskustvo"].ToString()) * 4;
        suma += Convert.ToInt32(reader["tehnika"].ToString()) * 6;
        suma += Convert.ToInt32(reader["izdrzlivost"].ToString()) * 7;
        suma += Convert.ToInt32(reader["harizma"].ToString()) * 4;
        suma += Convert.ToInt32(reader["motivacija"].ToString()) * 4;
        suma -= Convert.ToInt32(reader["tezina"].ToString()) * 4;
        suma /= 48;

        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE vozac SET vkupna_jacina = :oa WHERE id = :id";
        command.Parameters.AddWithValue(":oa", suma);
        command.Parameters.AddWithValue(":id", vozacId);

        command.ExecuteNonQuery();
    }

    private void odzemiOdBudget(int suma)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - :suma WHERE id = :id";
        command.Parameters.AddWithValue(":suma", suma);
        command.Parameters.AddWithValue(":id", menId);

        command.ExecuteNonQuery();
    }

    private OracleDataReader prezemiVozacAtributi()
    {
        //gi zema atributite na vozacot
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT koncentracija, talent, agresivnost, iskustvo, tehnika, izdrzlivost, motivacija, harizma, tezina FROM vozac WHERE id = :id";
        command.Parameters.AddWithValue(":id", vozacId);

        return command.ExecuteReader();
    }

    private void setirajNulaAkoNegativen(int vozacId)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE vozac SET koncentracija = CASE WHEN koncentracija < 0 THEN 0 WHEN koncentracija > 250 THEN 250 ELSE koncentracija END, " +
                              "agresivnost = CASE WHEN agresivnost < 0 THEN 0 WHEN agresivnost > 250 THEN 250 ELSE agresivnost END, " +
                              "tehnika = CASE WHEN tehnika < 0 THEN 0 WHEN tehnika > 250 THEN 250 ELSE tehnika END, " +
                              "izdrzlivost = CASE WHEN izdrzlivost < 0 THEN 0 WHEN izdrzlivost > 250 THEN 250 ELSE izdrzlivost END, " +
                              "harizma = CASE WHEN harizma < 0 THEN 0 WHEN harizma > 250 THEN 250 ELSE harizma END, " +
                              "tezina = CASE WHEN tezina < 0 THEN 0 WHEN tezina > 250 THEN 250 ELSE tezina END, " +
                              " motivacija = CASE WHEN motivacija <0 THEN 0 WHEN motivacija > 250 THEN 250 ELSE motivacija END " + 
                              " WHERE id = :id";
        command.Parameters.AddWithValue(":id", vozacId);

        command.ExecuteNonQuery();
    }

    private void negativenBalans()
    {
        if (!otvoriKonekcija())
            return;
        try{
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT budzet FROM menadzer WHERE id = :id";
            command.Parameters.AddWithValue(":id", menId);

            OracleDataReader reader = command.ExecuteReader();
            if (reader.Read() && Convert.ToInt64(reader["budzet"].ToString()) < 0)
            {
                btnTreninrajVozac.Enabled = false;
                lblNeMoziTreniranje.Visible = true;
                lblNeMoziTreniranje.Text += " Имате негативен баланс на буџетот и поради тоа не смеете да го тренирате возачот.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //lblOsvoeniPoeni.Text = ex.Message;
        }
        finally
        {
            zatvoriKonekcija();
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