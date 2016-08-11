using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TreningVozac : System.Web.UI.Page
{
    private OracleConnection connect;

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
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!otvoriKonekcija())
                return;

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM objekt_tip";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "objekt_tip");

                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;
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

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!otvoriKonekcija())
            return;

        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "DELETE FROM objekt_tip WHERE id = :id";
            command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text));
            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM objekt_tip";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "objekt_tip");

                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;
            }
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

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet data = (DataSet)ViewState["dataset"];
        GridView1.EditIndex = -1;
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet data = (DataSet)ViewState["dataset"];
        GridView1.EditIndex = e.NewEditIndex;
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (!otvoriKonekcija())
            return;
        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "UPDATE objekt_tip SET ime=:ime, cena=:cena, trening_cena=:trening_cena, vkupna_jacina=:vkupna_jacina WHERE ID=:ID";
              
            TextBox tb = GridView1.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":ime", tb.Text);

            tb = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            command.Parameters.AddWithValue(":ID", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":cena", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":trening_cena", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":vkupna_jacina", Double.Parse(tb.Text));

            int effect = 0;
           
            effect = command.ExecuteNonQuery(); 
            if(effect != 0)
            {
                    command = new OracleCommand();
                    command.Connection = connect;
                    command.CommandText = "SELECT * FROM objekt_tip";

                    OracleDataAdapter adapter = new OracleDataAdapter();
                    adapter.SelectCommand = command;
                    DataSet data = new DataSet();
                    adapter.Fill(data, "objekt_tip");
                    GridView1.EditIndex = -1;
                    GridView1.DataSource = data;
                    GridView1.DataBind();
                    ViewState["dataset"] = data;
            }

        }
        catch(Exception ex)
        {
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
            GridView1.EditIndex = -1;         
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
            return;
        try
        {
            object id = String.IsNullOrEmpty(TextBox1.Text.Trim()) ? Convert.DBNull : Int32.Parse(TextBox1.Text.Trim());
            object ime = String.IsNullOrEmpty(TextBox2.Text.Trim()) ? Convert.DBNull: TextBox2.Text.Trim();
            int cena = Int32.Parse(String.IsNullOrEmpty(TextBox3.Text.Trim()) ? "0" : TextBox3.Text.Trim());
            int trening_cena = Int32.Parse(String.IsNullOrEmpty(TextBox4.Text.Trim()) ? "0" : TextBox4.Text.Trim());
            double vkupna_jacina = Double.Parse(String.IsNullOrEmpty(TextBox5.Text.Trim()) ? "0" : TextBox5.Text.Trim());


            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO objekt_tip VALUES(:id, :ime, :cena, :trening_cena, :vkupna_jacina)";
            command.Parameters.AddWithValue(":id", id);
            command.Parameters.AddWithValue(":ime", ime);
            command.Parameters.AddWithValue(":cena", cena);
            command.Parameters.AddWithValue(":trening_cena", trening_cena);
            command.Parameters.AddWithValue(":vkupna_jacina", vkupna_jacina);

            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM objekt_tip";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "objekt_tip");
                GridView1.EditIndex = -1;
                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;
            }


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
}