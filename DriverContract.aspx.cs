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

public partial class user_DriverContract : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    private int vozacId;
    private int salary;
    private int signingFee;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        
        if (!imaVozac())
        {
            vozacInfo.Visible = false;
            nemaVozac.Visible = true;
        }
        this.DataBind();
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
                vozacId = Convert.ToInt32(reader["id"].ToString());
                salary = Int32.Parse(reader["plata"].ToString());
                signingFee = Int32.Parse(reader["nagradapripotpisuvanje"].ToString());
                negativenBalans();
                onevozmoziProdolzuvanje();
                statusBudzetManager();
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            lblNacionalnost.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
        return false;
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

        lblPlata.Text = convertToCurrency(reader["plata"].ToString());
        lblPodiumBonus.Text = reader["podiumbonus"].ToString();
        lblPobedaBonus.Text = reader["pobedabonus"].ToString();
        lblDogovor.Text = reader["dolzinadogovor"].ToString();
    }

    /*private void onevozmoziPonuda()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.men_id FROM momentalniponudi m WHERE men_id = :men_id AND vozac_id = :vozac_id";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":vozac_id", vozacId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            //ima aktivna ponuda od ovoj menadzer
        }
    }*/


    private void onevozmoziProdolzuvanje()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT v.dolzinaprodolzenie FROM vozac v WHERE id = :vozac_id AND (dolzinaprodolzenie IS NOT NULL AND dolzinaprodolzenie <> 0)";
        command.Parameters.AddWithValue(":vozac_id", vozacId);

        OracleDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            //ima prodolzen dogovor
            btnProdolzi.Enabled = false;
            lblNeMoziProdolzuvanje.Visible = true;
            lblNeMoziProdolzuvanje.Text = "Не може да го продолжите договорот бидејќи веќе го имате продолжено на уште " + reader["dolzinaprodolzenie"].ToString();
            if (!reader["dolzinaprodolzenie"].ToString().Equals("1"))
                lblNeMoziProdolzuvanje.Text += " трки";
            else
                lblNeMoziProdolzuvanje.Text += " тркa";
        }
    }

    protected string signingFeeProdolzvuanje(int step)
    {
        return ((signingFee * (60 + (10 * step)))/100).ToString();   
    }

    protected string salaryProdolzuvanje(int step)
    {
        return (salary * (1.25 - ( 0.02 * step))).ToString();
    }

    protected void btnProdolzi_Click(object sender, EventArgs e)
    {

        if (!otvoriKonekcija())
        {
            //neuspesn konektiranje
            return;
        }
        try
        {
            prodolziGoDogovorot();
            odzemiOdBudezet();

            
        }
        catch (Exception ex)
        {
            //lblOA.Text = ex.Message + " " + ex.StackTrace;
            Console.WriteLine(ex.Message);
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("DriverContract.aspx");
        }
    }

    private void prodolziGoDogovorot(){
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE vozac v SET dolzinaprodolzenie = :dolzina, prodolzenieplata = :plata, prodolzenienagrada = :nagrada WHERE id = :vozac_id";
        command.Parameters.AddWithValue(":dolzina", Int32.Parse(Regex.Match(ddlProdolzuvanje.SelectedItem.Text, @"\d+").Value));
        command.Parameters.AddWithValue(":plata", Convert.ToInt32(salaryProdolzuvanje(Int32.Parse(ddlProdolzuvanje.SelectedValue))));
        command.Parameters.AddWithValue(":nagrada", Convert.ToInt32(signingFeeProdolzvuanje(Int32.Parse(ddlProdolzuvanje.SelectedValue))));
        command.Parameters.AddWithValue(":vozac_id", vozacId);

        command.ExecuteNonQuery();
    }

    private void odzemiOdBudezet()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - :signingfee WHERE id = :men_id";
        command.Parameters.AddWithValue(":signingfee", Int32.Parse(signingFeeProdolzvuanje(Int32.Parse(ddlProdolzuvanje.SelectedValue))));
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void negativenBalans()
    {
       
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT budzet FROM menadzer WHERE id = :id";
            command.Parameters.AddWithValue(":id", menId);

            OracleDataReader reader = command.ExecuteReader();
            if (reader.Read() && Convert.ToInt32(reader["budzet"].ToString()) < 0)
            {
                btnProdolzi.Enabled = false;
                lblNeMoziProdolzuvanje.Visible = true;
                lblNeMoziProdolzuvanje.Text = "Не може да го продoлжите договорот, имате негативен баланс";
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