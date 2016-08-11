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

public partial class Kvalifikacii : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    private int level;
    private int number;

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
            
            Int32.TryParse(Request.QueryString["level"].ToString(), out level);
            Int32.TryParse(Request.QueryString["id"].ToString(), out number);
            ispolniLista();
            prikaziKvalifikacii1();
            prikaziKvalifikacii2();
            prikaziVkupno();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void ispolniLista()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT n.ime, n.id, g.grupa_broj FROM grupa g, nivoime n WHERE g.GRUPA_NIVO = n.id ORDER BY n.id, g.grupa_broj";
        ArrayList lista = new ArrayList();
        //ddlGrupi.Items.Insert(0, new ListItem("--Select--"));
        OracleDataReader reader = command.ExecuteReader();
        int i = 0;
        int select = 0;
        while (reader.Read())
        {
            string id = reader["id"].ToString();
            string grupa =reader["grupa_broj"].ToString();
            lista.Add(new ListItem(reader["ime"].ToString() + " " + reader["grupa_broj"].ToString(), id + " " + grupa));
            if (id.Equals(level.ToString()) && grupa.Equals(number.ToString()))
                select = i;
            i++;
        }

        ddlGrupi.DataTextField = "Text";
        ddlGrupi.DataValueField = "Value";
        ddlGrupi.DataSource = lista;
        ddlGrupi.DataBind();

        ddlGrupi.SelectedIndex = select;

    }

    private void prikaziKvalifikacii1()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.ime || ' ' || m.prezime as \"ime\", k.vreme FROM kvalifikaci k, menadzer m WHERE k.men_id = m.id AND tip=:tip AND m.grupa_nivo = :grupa_nivo AND m.grupa_broj = :grupa_broj AND (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1)";
        command.Parameters.AddWithValue(":grupa_broj", number);
        command.Parameters.AddWithValue(":grupa_nivo", level);
        command.Parameters.AddWithValue(":tip", 1);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "Bolid");

        gvPrvaKval.DataSource = ds;
        gvPrvaKval.DataBind();

    }

    protected string prikaziVreme(string vreme)
    {
        TimeSpan t = TimeSpan.FromSeconds(Double.Parse(vreme));

        string answer = string.Format("{1:D2}:{2:D2}.{3:D3}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);

        return answer;
    }

    private void prikaziKvalifikacii2()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.ime || ' ' || m.prezime as \"ime\", k.vreme FROM kvalifikaci k, menadzer m WHERE k.men_id = m.id AND tip=:tip AND m.grupa_nivo = :grupa_nivo AND m.grupa_broj = :grupa_broj AND (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1)";
        command.Parameters.AddWithValue(":grupa_broj", number);
        command.Parameters.AddWithValue(":grupa_nivo", level);
        command.Parameters.AddWithValue(":tip", 2);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "Kval2");

        gvVtoraKval.DataSource = ds;
        gvVtoraKval.DataBind();

    }

    private void prikaziVkupno()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.ime || ' ' || m.prezime as \"ime\", x.vreme, x.vreme - (SELECT MIN(SUM(k.vreme)) vreme FROM kvalifikaci k, menadzer m WHERE k.men_id = m.id AND m.grupa_nivo = :grupa_nivo AND m.grupa_broj = :grupa_broj AND (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) GROUP BY m.id) zaostanuvanje FROM menadzer m join (SELECT m.id, SUM(k.vreme) vreme FROM kvalifikaci k, menadzer m WHERE k.men_id = m.id AND m.grupa_nivo = :grupa_nivo AND m.grupa_broj = :grupa_broj AND (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) GROUP BY m.id) x on x.id = m.id";
        command.Parameters.AddWithValue(":grupa_broj", number);
        command.Parameters.AddWithValue(":grupa_nivo", level);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "Vkupno");

        gvVkupno.DataSource = ds;
        gvVkupno.DataBind();

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
    protected void ddlGrupi_TextChanged(object sender, EventArgs e)
    {
        int nivo = Int32.Parse(Regex.Match(ddlGrupi.SelectedValue.ToString(), @"\d+").Value);
        int grupa = Int32.Parse(Regex.Match(ddlGrupi.SelectedValue.ToString(), @" \d+").Value);
        Response.Redirect("Kvalifikacii.aspx?level=" + nivo.ToString() + "&id=" + grupa.ToString());
    }
}