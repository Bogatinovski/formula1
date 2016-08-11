using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenadzerPodatoci : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        if(!IsPostBack)
            initialize();
    }

    private void initialize()
    {
        if (!otvoriKonekcija())
            return;

        try
        {
            statusBudzetManager();
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT ime, prezime, slika, lozinka, email, mestoziveenje FROM menadzer where id=:id";
            command.Parameters.AddWithValue(":id", menId);

            OracleDataReader reader = command.ExecuteReader();
            reader.Read();

            TextBox1.Text = Convert.IsDBNull(reader["ime"]) ? " " : (string)reader["ime"];
            TextBox2.Text = Convert.IsDBNull(reader["prezime"]) ? " " : (string)reader["prezime"];
            TextBox3.Text = Convert.IsDBNull(reader["slika"]) ? " " : (string)reader["slika"];
            TextBox4.Text = Convert.IsDBNull(reader["lozinka"]) ? " " : (string)reader["lozinka"];
            TextBox5.Text = Convert.IsDBNull(reader["email"]) ? " " : (string)reader["email"];
            TextBox6.Text = Convert.IsDBNull(reader["mestoziveenje"]) ? " " : (string)reader["mestoziveenje"];
            TextBox7.Text = TextBox4.Text;

        }
        catch (Exception ex)
        {
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (!otvoriKonekcija())
            return;

        try
        {
            string ime, prezime, lozinka, slika, email, mesto;
            ime = prezime = lozinka = slika = email = mesto = "";
            if (!String.IsNullOrEmpty(TextBox1.Text.Trim()))
                ime = TextBox1.Text.Trim();
            if (!String.IsNullOrEmpty(TextBox2.Text.Trim()))
                prezime = TextBox2.Text.Trim();
            if (!String.IsNullOrEmpty(TextBox3.Text.Trim()))
                slika = TextBox3.Text.Trim();
            if (!String.IsNullOrEmpty(TextBox4.Text.Trim()))
                lozinka = TextBox4.Text.Trim();
            if (!String.IsNullOrEmpty(TextBox5.Text.Trim()))
                email = TextBox5.Text.Trim();
            if (!String.IsNullOrEmpty(TextBox6.Text.Trim()))
                mesto = TextBox6.Text.Trim();


            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "UPDATE menadzer SET ime=:ime, prezime=:prezime, lozinka=:lozinka, slika=:slika, email=:email, mestoziveenje=:mesto WHERE id=:id";

            command.Parameters.AddWithValue(":ime", TextBox1.Text);
            command.Parameters.AddWithValue(":prezime", prezime);
            command.Parameters.AddWithValue(":lozinka", lozinka);
            command.Parameters.AddWithValue(":slika", slika);
            command.Parameters.AddWithValue(":email", email);
            command.Parameters.AddWithValue(":mesto", mesto);
            command.Parameters.AddWithValue(":id", menId);

            int result = command.ExecuteNonQuery();
            labelError.Text = result.ToString() + " rows edited!";
            labelError.Text = ime;
        }
        catch (Exception ex)
        {
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
            //Response.Redirect("MenadzerPodatoci.aspx");
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
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
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
}