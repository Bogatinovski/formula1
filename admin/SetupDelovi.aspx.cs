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
                command.CommandText = "SELECT * FROM SETUPDELOVI";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "SETUPDELOVI");

                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;

                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT id FROM delovi where delovi.id not in (select id from setupdelovi)";

                OracleDataReader reader = command.ExecuteReader();
                DropDownList1.Items.Clear();
                while (reader.Read())
                    DropDownList1.Items.Add(reader[0].ToString());
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
            command.CommandText = "DELETE FROM SETUPDELOVI WHERE id = :id";
            command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text));
            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM SETUPDELOVI";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "SETUPDELOVI");

                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;

                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT id FROM delovi where delovi.id not in (select id from setupdelovi)";

                OracleDataReader reader = command.ExecuteReader();
                DropDownList1.Items.Clear();
                while (reader.Read())
                    DropDownList1.Items.Add(reader[0].ToString());
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
            object id = String.IsNullOrEmpty(DropDownList1.SelectedValue.ToString().Trim()) ? Convert.DBNull : Int32.Parse(DropDownList1.SelectedValue.ToString().Trim());
            labelError.Text = DropDownList1.SelectedValue.ToString();
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO SETUPDELOVI VALUES(:id)";
            command.Parameters.AddWithValue(":id", id);


            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM SETUPDELOVI";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "SETUPDELOVI");
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
            Response.Redirect("SetupDelovi.aspx");
        }

    }
}