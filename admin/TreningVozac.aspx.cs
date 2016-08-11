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
                command.CommandText = "SELECT * FROM trening_vozac order by id";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "trening_vozac");

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
            command.CommandText = "DELETE FROM trening_vozac WHERE id = :id";
            command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text));
            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM trening_vozac order by id";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "trening_vozac");

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
            command.CommandText = "UPDATE TRENING_VOZAC SET TIP_ID=:TIP_ID, KONCENTRACIJA=:KONCENTRACIJA, TALENT=:TALENT, AGRESIVNOST=:AGRESIVNOST, ISKUSTVO=:ISKUSTVO, TEHNIKA=:TEHNIKA, IZDRZLIVOST=:IZDRZLIVOST, MOTIVACIJA=:MOTIVACIJA, HARIZMA=:HARIZMA, TEZINA=:TEZINA WHERE ID=:ID";
              
            TextBox tb = GridView1.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":TIP_ID", Int32.Parse(tb.Text));

            tb = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            command.Parameters.AddWithValue(":ID", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":KONCENTRACIJA", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":TALENT", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":AGRESIVNOST", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[5].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":ISKUSTVO", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[6].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":TEHNIKA", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[7].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":IZDRZLIVOST", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[8].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":MOTIVACIJA", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[9].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":HARIZMA", Int32.Parse(tb.Text));

            tb = GridView1.Rows[e.RowIndex].Cells[10].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":TEZINA", Int32.Parse(tb.Text));  

            int effect = 0;
           
            effect = command.ExecuteNonQuery(); 
            if(effect != 0)
            {
                    command = new OracleCommand();
                    command.Connection = connect;
                    command.CommandText = "SELECT * FROM trening_vozac order by id";

                    OracleDataAdapter adapter = new OracleDataAdapter();
                    adapter.SelectCommand = command;
                    DataSet data = new DataSet();
                    adapter.Fill(data, "trening_vozac");
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
            return;
        try
        {
            int id = Int32.Parse(String.IsNullOrEmpty(TextBox1.Text.Trim()) ? "0" : TextBox1.Text.Trim());
            int tip_id = Int32.Parse(String.IsNullOrEmpty(TextBox2.Text.Trim()) ? "0" : TextBox2.Text.Trim());
            int koncentracija = Int32.Parse(String.IsNullOrEmpty(TextBox3.Text.Trim()) ? "0" : TextBox3.Text.Trim());
            int talent = Int32.Parse(String.IsNullOrEmpty(TextBox4.Text.Trim()) ? "0" : TextBox4.Text.Trim());
            int agresivnost = Int32.Parse(String.IsNullOrEmpty(TextBox5.Text.Trim()) ? "0" : TextBox5.Text.Trim());
            int iskustvo = Int32.Parse(String.IsNullOrEmpty(TextBox6.Text.Trim()) ? "0" : TextBox6.Text.Trim());
            int tehnika = Int32.Parse(String.IsNullOrEmpty(TextBox7.Text.Trim()) ? "0" : TextBox7.Text.Trim());
            int izdrzlivost = Int32.Parse(String.IsNullOrEmpty(TextBox8.Text.Trim()) ? "0" : TextBox8.Text.Trim());
            int motivacija = Int32.Parse(String.IsNullOrEmpty(TextBox9.Text.Trim()) ? "0" : TextBox9.Text.Trim());
            int harizma = Int32.Parse(String.IsNullOrEmpty(TextBox10.Text.Trim()) ? "0" : TextBox10.Text.Trim());
            int tezina = Int32.Parse(String.IsNullOrEmpty(TextBox11.Text.Trim()) ? "0" : TextBox11.Text.Trim());



            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO trening_vozac VALUES(:id, :tip_id, :koncentracija, :talent, :agresivnost, :iskustvo, :tehnika, :izdrzlivost, :motivacija, :harizma, :tezina)";
            command.Parameters.AddWithValue(":id", id);
            command.Parameters.AddWithValue(":tip_id", tip_id);
            command.Parameters.AddWithValue(":koncentracija", koncentracija);
            command.Parameters.AddWithValue(":talent", talent);
            command.Parameters.AddWithValue(":agresivnost", agresivnost);
            command.Parameters.AddWithValue(":iskustvo", iskustvo);
            command.Parameters.AddWithValue(":tehnika", tehnika);
            command.Parameters.AddWithValue(":izdrzlivost", izdrzlivost);
            command.Parameters.AddWithValue(":motivacija", motivacija);
            command.Parameters.AddWithValue(":harizma", harizma);
            command.Parameters.AddWithValue(":tezina", tezina);

            int result = command.ExecuteNonQuery();
            if(result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM trening_vozac order by id";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "trening_vozac");
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