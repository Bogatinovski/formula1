using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OracleClient;

public partial class user_style_Login : System.Web.UI.Page
{
    private OracleConnection connect;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            Session.Abandon();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && otvoriKonekcija())
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "Select id from menadzer where username = :username AND lozinka = :lozinka";
            command.Parameters.AddWithValue(":username", txtUsername.Text);
            command.Parameters.AddWithValue(":lozinka", txtPassword.Text);

            try
            {
                OracleDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    Session["user"] = reader["id"].ToString();
                    lblIzvestuvanje.Text = "Najaven e korisnik so id " + Session["user"].ToString();
                    Response.Redirect("Office.aspx");
                }
                else
                {
                    lblIzvestuvanje.Text = "Не постои корисник со зададените параметри";
                    Session.Abandon();
                }
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
}