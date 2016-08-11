using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DodadiDelovi : System.Web.UI.Page
{
    private OracleConnection connect;


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
            return;

        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO delovi (id, ime, cena) VALUES (:id, :ime, :cena)";

            int id = Int32.Parse(TextBox3.Text);
            string ime = TextBox1.Text;
            int cena = Int32.Parse(TextBox2.Text);

            command.Parameters.AddWithValue(":id", id);
            command.Parameters.AddWithValue(":ime", ime);
            command.Parameters.AddWithValue(":cena", cena);

            int result = command.ExecuteNonQuery();
            if (result != 0)
                labelError.Text = ime + " е успешно внесен во системот!";
            else labelError.Text = "0 внесени редови!";

        }
        catch(Exception ex)
        {
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
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
}