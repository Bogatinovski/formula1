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
                command.CommandText = "SELECT * FROM GRUPA";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "GRUPA");

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
            command.CommandText = "DELETE FROM GRUPA WHERE grupa_broj = :grupa_broj and grupa_nivo = :grupa_nivo";
            command.Parameters.AddWithValue(":grupa_broj", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text));
            command.Parameters.AddWithValue(":grupa_nivo", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[1].Text));

            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM GRUPA";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "GRUPA");

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
            object grupa_broj = String.IsNullOrEmpty(TextBox1.Text.Trim()) ? Convert.DBNull : Int32.Parse(TextBox1.Text.Trim());
            object grupa_nivo = String.IsNullOrEmpty(TextBox2.Text.Trim()) ? Convert.DBNull : TextBox2.Text.Trim();

            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO GRUPA VALUES(:grupa_broj, :grupa_nivo)";
            command.Parameters.AddWithValue(":grupa_broj", grupa_broj);
            command.Parameters.AddWithValue(":grupa_nivo", grupa_nivo);


            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM GRUPA";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "GRUPA");
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