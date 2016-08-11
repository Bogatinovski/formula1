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
                command.CommandText = "SELECT * FROM KOMENTARI ORDER BY id";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "KOMENTARI");

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
            command.CommandText = "DELETE FROM KOMENTARI WHERE id = :id";
            command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text));
            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM KOMENTARI ORDER BY id";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "KOMENTARI");

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
   
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
            return;
        try
        {
            object id = String.IsNullOrEmpty(TextBox1.Text.Trim()) ? Convert.DBNull : Int32.Parse(TextBox1.Text.Trim());
            int del_id = Int32.Parse(String.IsNullOrEmpty(TextBox2.Text.Trim()) ? "0" : TextBox2.Text.Trim());
            object tekst = String.IsNullOrEmpty(TextBox3.Text.Trim()) ? Convert.DBNull : TextBox3.Text.Trim();
            int poveke = CheckBox1.Checked ? 1 : 0;

            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO komentari VALUES(:id, :del_id, :tekst, :poveke)";
            command.Parameters.AddWithValue(":id", id);
            command.Parameters.AddWithValue(":del_id", del_id);
            command.Parameters.AddWithValue(":tekst", tekst);
            command.Parameters.AddWithValue(":poveke", poveke);

            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM komentari ORDER BY id";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "KOMENTARI");
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
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (!otvoriKonekcija())
            return;
        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "UPDATE KOMENTARI SET del_id=:del_id, tekst=:tekst, povekje=:povekje WHERE ID=:ID";

            TextBox tb = GridView1.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":del_id", Int32.Parse(tb.Text));

            tb = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            command.Parameters.AddWithValue(":ID", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":tekst", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":povekje", Int32.Parse(tb.Text));

            int effect = 0;

            effect = command.ExecuteNonQuery();
            if (effect != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM KOMENTARI";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "KOMENTARI");
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
            GridView1.EditIndex = -1;
        }
    }
}