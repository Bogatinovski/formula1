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

public partial class Office : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        if (!Page.IsPostBack)
            initialize();
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
            statusBudzetManager();
            zemiSlednaTrka();
            ispolniAtributiMenadzer();
            zemiVozac();
            zemiPersonal();
            zemiObjekti();
            zemiBolid();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            lblSLednaTrka.Text = ex.Message + "  " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void zemiSlednaTrka()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT p.id, p.ime FROM pateka p, trka t WHERE p.id = t.pateka AND (t.sezonabroj, t.trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1)";

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            slednaTrka.NavigateUrl = "Pateka.aspx?id=" + reader["id"].ToString();
            lblSLednaTrka.Text = reader["ime"].ToString();
        }
    }

    private void ispolniAtributiMenadzer()
    {

        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.*, n.ime grupaime FROM menadzer m, nivoime n WHERE m.GRUPA_NIVO = n.id AND m.id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            lblMenIme.Text = reader["ime"].ToString() + " " + reader["prezime"].ToString();
            lblKorisnicko.Text = reader["username"].ToString();
            if (reader["slika"] != null && !reader["slika"].ToString().Equals(""))
                imgProfile.ImageUrl = reader["slika"].ToString();
            lblBudzet.Text = convertToCurrency(reader["budzet"].ToString());
            lblBrTrki.Text = reader["BRTRKI"].ToString();
            lblAvgPoeni.Text = reader["PROSEKPOENI"].ToString();
            lblGrupa.Text = reader["grupaime"].ToString() + " - " + reader["GRUPA_BROJ"].ToString();
            lblPobedi.Text = reader["POBEDI"].ToString();
            lblPoeni.Text = reader["POENI"].ToString();
            lblPolPozicii.Text = reader["POLPOZICII"].ToString();
            lblPodiumi.Text = reader["PODIUMI"].ToString();
            lblZemja.Text = reader["DRZAVA"].ToString();
        }

    }

    private void zemiVozac()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT v.id, v.ime, v.vkupna_jacina, v.plata, v.dolzinadogovor FROM vozac v, vozacistorija i WHERE i.vozac_id = v.id AND i.men_id = :men_id AND end_contract_race IS NULL";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            navVozac.NavigateUrl = "DriverProfile.aspx?id=" + reader["id"].ToString();
            lblVozacIme.Text = reader["ime"].ToString();
            lblVozacJacina.Text = reader["vkupna_jacina"].ToString();
            lblVozacDogovor.Text = reader["dolzinadogovor"].ToString();
            lblVozacPlata.Text = convertToCurrency(reader["plata"].ToString());
        }
    }

    private void zemiPersonal()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT vkupno, suma FROM personal p join (SELECT p.id, SUM(cena*vrednost) suma FROM personal p, personal_menadzer pm, personal_tip t WHERE p.id =:men_id AND p.id=pm.id AND pm.personal_id = t.id GROUP BY p.id) x on p.id=x.id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            lblStaffPlata.Text = convertToCurrency(reader["suma"].ToString());
            lblStaffVkupno.Text = reader["vkupno"].ToString();
        }
    }

    private void zemiObjekti()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT vkupno, suma FROM objekt o join (SELECT o.id, SUM(cena*vrednost) suma FROM objekt o, objekt_menadzer om, objekt_tip t WHERE o.id =:men_id AND o.id=om.id AND om.objekt_id = t.id GROUP BY o.id) x on o.id=x.id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            lblObjektiOdrzuvanje.Text = convertToCurrency(reader["suma"].ToString());
            lblObjektiVkupno.Text = reader["vkupno"].ToString();
        }
    }

    private void zemiBolid()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT * FROM bolid where id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            lblMoknost.Text = reader["MOKNOST"].ToString();
            lblUpravuvanje.Text = reader["UPRAVUVANJE"].ToString();
            lblZabrzuvanje.Text = reader["ZABRZUVANJE"].ToString();
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
}