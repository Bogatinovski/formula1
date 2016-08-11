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


public partial class ManagerProfile : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;

    private int zaMen;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        zaMen = Int32.Parse(Request.QueryString["id"].ToString());
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
            ispolniAtributiMenadzer();
            ispolniIstorijaVozaci();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            lblIme.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void ispolniAtributiMenadzer()
    {

        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.*, n.ime grupaime FROM menadzer m, nivoime n WHERE m.GRUPA_NIVO = n.id AND m.id = :men_id";
        command.Parameters.AddWithValue(":men_id", zaMen);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            lblIme.Text = reader["ime"].ToString() + " " + reader["prezime"].ToString();
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

    private void ispolniIstorijaVozaci()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT v.id, v.ime, i.* FROM vozacistorija i, vozac v WHERE i.vozac_id = v.id AND i.men_id = :men_id";
        command.Parameters.AddWithValue(":men_id", zaMen);

            OracleDataAdapter adapter = new OracleDataAdapter();
         adapter.SelectCommand = command;
         DataSet ds = new DataSet();
         adapter.Fill(ds, "Bolid");

         gvHistory.DataSource = ds;
         gvHistory.DataBind();
    }

    protected string pecatiVozac(string id, string ime)
    {
        return "<a href=\"DriverProfile.aspx?id=" + id + "\">" + ime + "</a>";
    }

    protected String pecatiIstorija(string sezona, string trka)
    {

        if (sezona == null || sezona.Equals(""))
            return "Не е уште завршен";
        else
            return "Сезона " + sezona + ", Трка " + trka;
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