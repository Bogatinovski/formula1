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

public partial class user_Personal : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;


    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        if(!Page.IsPostBack)
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

            ispolniAtributi();
            negativenBalans();
            statusBudzetManager();
            imaVekjeTrenirano();

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
    private void ispolniAtributi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT p.vrednost, t.cena, t.ime, t.trening_cena, t.id, per.vkupno FROM personal_menadzer p, personal_tip t, personal per WHERE per.id = p.id AND p.personal_id = t.id AND p.id = :men_id ORDER BY t.id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "PersonalAtributi");

        gvPersonalAttributes.DataSource = ds;
        gvPersonalAttributes.DataBind();

        gvTreninzi.DataSource = ds;
        gvTreninzi.DataBind();

        //se inicijalizira dropdown za treniranje
        int plata = 0;
        OracleDataReader reader = command.ExecuteReader();
        List<ListItem> lista = new List<ListItem>();
        while (reader.Read())
        {
            lista.Add(new ListItem(reader["ime"].ToString(), reader["id"].ToString()));
            plata += (Int32.Parse(reader["cena"].ToString()) * Int32.Parse(reader["vrednost"].ToString()));
            lblVkupno.Text = reader["vkupno"].ToString();
        }

        ddlTrening.DataTextField = "Text";
        ddlTrening.DataValueField = "Value";
        ddlTrening.DataSource = lista;
        ddlTrening.DataBind();

        //presmetka na vkupna cena za plata za personal
        lblPlata.Text = convertToCurrency(plata);

        
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (!otvoriKonekcija())
        {
            //ima problem so konekijata;
            return;
        }
        try
        {
            izmeniVrednosti();
            odzemiOdBudezet();
            presmetajVkupno();
            onevozmoziTreniranje();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("Personal.aspx");
        }
    }
   
    private void izmeniVrednosti()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE personal_menadzer SET vrednost = ROUND(vrednost + ((SELECT trening_vrednost FROM personal_tip WHERE id = :personal_id) - (SELECT trening_vrednost FROM personal_tip WHERE id = :personal_id) * vrednost / 100) + 1) WHERE id = :men_id AND personal_id = :personal_id";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":personal_id", Int32.Parse(ddlTrening.SelectedValue));

        command.ExecuteNonQuery();
    }

    private void odzemiOdBudezet()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - (SELECT trening_cena FROM personal_tip WHERE id = :tip_id) WHERE id = :men_id";
        command.Parameters.AddWithValue(":tip_id", Int32.Parse(ddlTrening.SelectedValue));
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void onevozmoziTreniranje(){
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE personal SET dozvolitreniranje = 0 WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void presmetajVkupno()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE personal SET vkupno = ROUND((SELECT SUM(vrednost * vkupna_jacina) FROM personal_tip t, personal_menadzer p WHERE p.personal_id = t.id AND p.id = :men_id)) WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void imaVekjeTrenirano()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT dozvolitreniranje FROM personal WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader["dozvolitreniranje"].ToString().Equals("0"))
        {
            btnTreniraj.Enabled = false;
            lblIzvestuvanje.Visible = true;
            lblIzvestuvanje.Text = "Имате тренирано во пресрет на следната трка. После завршувањето на трката ќе можете повторно да го тренирате вашиот персонал.";
        }
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
            btnTreniraj.Enabled = false;
            lblIzvestuvanje.Visible = true;
            lblIzvestuvanje.Text = "Не може да го тренирате персоналот, имате негативен баланс";
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