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

public partial class _Default : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    private int pateka;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        Int32.TryParse(Request.QueryString["id"].ToString(), out pateka);
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
            prezemiVrednosti();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void prezemiVrednosti()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT * FROM pateka WHERE id=:id";
        command.Parameters.AddWithValue(":id", pateka);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            imgProfile.ImageUrl = reader["SLIKA"].ToString();
            lblBKruga.Text = reader["KRUGA"].ToString();
            lblDolzina.Text = reader["DOLZINA"].ToString();
            lblDolzKrug.Text = reader["KRUGDOLZINA"].ToString();
            lblGorivo.Text = reader["GORIVO"].ToString();
            lblGumi.Text = reader["GUMI"].ToString();
            lblIme.Text = reader["IME"].ToString();
            lblPreteknuvanje.Text = reader["PRETEKNUVANJE"].ToString();

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