using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;



public partial class Register : System.Web.UI.Page
{
    private OracleConnection connect;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            postaviDatum();


    }

    private void postaviDatum()
    {

        ArrayList denovi = new ArrayList();
        ArrayList godini = new ArrayList();
        ArrayList meseci = new ArrayList();

        denovi.Add("Ден");
        meseci.Add("Месец");
        godini.Add("Година");

        for (int i = 1; i <= 31; i++)
        {
            denovi.Add(i);
        }

        ddlDen.DataSource = denovi;
        ddlDen.DataBind();

        for (int i = 1920; i <= 2005; i++)
        {
            godini.Add(i);
        }
        ddlGodina.DataSource = godini;
        ddlGodina.DataBind();
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
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        
        if (Page.IsValid && otvoriKonekcija())
        {
            //za da se zemi vo koja grupa ima najmalku menadzeri
            /*int maxMenadzeriPoGrupa = 10;
            OracleCommand commandGet = new OracleCommand();
            commandGet.Connection = connect;
            commandGet.CommandText = "SELECT g.grupa_broj FROM"+
                                        "grupa g"+
                                        "left outer join menadzer m on g.grupa_broj=m.grupa_broj"+
                                        "GROUP BY g.grupa_broj"+
                                        "HAVING COUNT(DISTINCT m.men_id) = ("+
                                        "SELECT MIN(COUNT(DISTINCT m.men_id)) FROM"+
                                        "grupa g"+
                                        "left outer join menadzer m on g.grupa_broj=m.grupa_broj" +
                                        "GROUP BY g.grupa_broj) AND COUNT(DISTINCT m.men_id) < " + maxMenadzeriPoGrupa;

            ArrayList slobodniGrupi = new ArrayList();
            int novaGrupa=-1;
            try
            {
                OracleDataReader reader = commandGet.ExecuteReader();
                while (reader.Read())
                {
                    slobodniGrupi.Add(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/

            if (postoiKorisnickoIme() || postoiEmail())
                return;
            

            OracleCommand commandInsert = new OracleCommand();
            commandInsert.Connection = connect;
            commandInsert.CommandText = "INSERT INTO listanachekanje (men_id, men_ime, men_prezime, email, username, lozinka, men_drzava, datum) VALUES (seq_listachekanje.nextval, :ime, :prezime, :email, :username, :lozinka, :drzava, :datum)";
            commandInsert.Parameters.AddWithValue(":ime", txtName.Text);
            commandInsert.Parameters.AddWithValue(":prezime", txtLastName.Text);
            commandInsert.Parameters.AddWithValue(":email", txtEmail.Text);
            commandInsert.Parameters.AddWithValue(":username", txtUsername.Text);
            commandInsert.Parameters.AddWithValue(":lozinka", txtPassword.Text);
            commandInsert.Parameters.AddWithValue(":drzava", ddlCountry.SelectedItem.Text);
            commandInsert.Parameters.AddWithValue(":datum", new DateTime(Convert.ToInt32(ddlGodina.SelectedItem.Text), Convert.ToInt32(ddlMesec.SelectedItem.Text), Convert.ToInt32(ddlDen.SelectedItem.Text))); 
            
            try
            {
                commandInsert.ExecuteNonQuery();
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

    private bool postoiKorisnickoIme()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT username FROM (SELECT username FROM listanachekanje l UNION( SELECT username FROM menadzer )) WHERE username = :username";
        command.Parameters.AddWithValue(":username", txtUsername.Text);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            Label1.Text = "Постои корисник со исто корисничко име. Пробајте со друго";
            return true;
        }
        return false;
        
    }

    private bool postoiEmail()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT email FROM (SELECT email FROM listanachekanje l UNION( SELECT email FROM menadzer )) WHERE email = :email";
        command.Parameters.AddWithValue(":email", txtEmail.Text);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            Label1.Text = "Постои корисник со истa е-маил адреса. Пробајте со друга";
            return true;
        }
        return false;
    }
}