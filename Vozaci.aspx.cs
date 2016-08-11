using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Vozaci : System.Web.UI.Page
{
    private OracleConnection connect;
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
                command.CommandText = "SELECT * FROM vozac";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();

                adapter.Fill(data, "VOZAC");
                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;

            }
            catch (Exception ex)
            {
                labelError.Text = ex.Message;
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
            labelError.Text = ex.Message;
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet data = (DataSet)ViewState["dataset"];
        GridView1.EditIndex = e.NewEditIndex;
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet data = (DataSet)ViewState["dataset"];
        GridView1.EditIndex = -1;
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
            command.CommandText = "UPDATE vozac set ime = :ime, nacionalnost=:nacionalnost, tituli=:tituli, vkupna_jacina=:vkupnaJacina, " +
            "koncentracija=:koncentracija, talent=:talent, agresivnost=:agresivnost, iskustvo=:iskustvo, tehnika=:tehnika, " +
            "izdrzlivost=:izdrzlivost, motivacija=:motivacija, harizma=:harizma, godini=:godini, plata=:plata, tezina=:tezina, " +
            "NAGRADAPRIPOTPISUVANJE=:nagradapotpisuvanje WHERE id=:id";

            TextBox tb = GridView1.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":ime", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":nacionalnost", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":tituli", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":vkupnaJacina", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":koncentracija", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[5].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":talent", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[6].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":agresivnost", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[7].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":iskustvo", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[8].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":tehnika", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[9].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":izdrzlivost", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[10].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":motivacija", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[11].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":harizma", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[12].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":godini", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[13].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":plata", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[14].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":tezina", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[14].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":nagradapotpisuvanje", tb.Text);

            tb = GridView1.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox;
            command.Parameters.AddWithValue(":id", GridView1.DataKeys[e.RowIndex].Value);

            int effect = 0;
            effect = command.ExecuteNonQuery();

            if (effect != 0)
            {
                OracleDataAdapter adapter = new OracleDataAdapter();
                command = new OracleCommand();
                command.CommandText = "SELECT * FROM vozac";
                command.Connection = connect;

                adapter.SelectCommand = command;
                DataSet data = new DataSet();

                GridView1.EditIndex = -1;
                adapter.Fill(data, "VOZAC");
                GridView1.DataSource = data;
                GridView1.DataBind();
                ViewState["dataset"] = data;
            }

        }
        catch (Exception ex)
        {
            labelError.Text = ex.Message;
        }
        finally
        {
            zatvoriKonekcija();
            GridView1.EditIndex = -1;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        brisiVozac(Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
    }

    private void brisiVozac(int id)
    {
        if (!otvoriKonekcija())
            return;
        
        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "DELETE FROM vozac WHERE id = :id";
            command.Parameters.AddWithValue(":id", id);

            command.ExecuteReader();

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
            return;
        try
        {
            int id = Int32.Parse(String.IsNullOrEmpty(TextBox17.Text.Trim()) ? "0" : TextBox17.Text.Trim());
            object ime = String.IsNullOrEmpty(TextBox1.Text.Trim()) ? Convert.DBNull : TextBox1.Text.Trim();
            object nacionalnost = String.IsNullOrEmpty(TextBox2.Text.Trim()) ? Convert.DBNull : TextBox2.Text.Trim();
            int tituli = Int32.Parse(String.IsNullOrEmpty(TextBox3.Text.Trim()) ? "0" : TextBox3.Text.Trim());
            int vkupna_jacina = Int32.Parse(String.IsNullOrEmpty(TextBox4.Text.Trim()) ? "0" : TextBox4.Text.Trim());
            int koncentracija = Int32.Parse(String.IsNullOrEmpty(TextBox5.Text.Trim()) ? "0" : TextBox5.Text.Trim());
            int talent = Int32.Parse(String.IsNullOrEmpty(TextBox6.Text.Trim()) ? "0" : TextBox6.Text.Trim());
            int agresivnost = Int32.Parse(String.IsNullOrEmpty(TextBox7.Text.Trim()) ? "0" : TextBox7.Text.Trim());
            int iskustvo = Int32.Parse(String.IsNullOrEmpty(TextBox9.Text.Trim()) ? "0" : TextBox9.Text.Trim());
            int tehnika = Int32.Parse(String.IsNullOrEmpty(TextBox8.Text.Trim()) ? "0" : TextBox8.Text.Trim());
            int izdrzlivost = Int32.Parse(String.IsNullOrEmpty(TextBox10.Text.Trim()) ? "0" : TextBox10.Text.Trim());
            int motivacija = Int32.Parse(String.IsNullOrEmpty(TextBox11.Text.Trim()) ? "0" : TextBox11.Text.Trim());
            int harizma = Int32.Parse(String.IsNullOrEmpty(TextBox12.Text.Trim()) ? "0" : TextBox12.Text.Trim());
            int godini = Int32.Parse(String.IsNullOrEmpty(TextBox13.Text.Trim()) ? "0" : TextBox13.Text.Trim());
            int plata = Int32.Parse(String.IsNullOrEmpty(TextBox14.Text.Trim()) ? "0" : TextBox14.Text.Trim());
            int tezina = Int32.Parse(String.IsNullOrEmpty(TextBox15.Text.Trim()) ? "0" : TextBox15.Text.Trim());
            int nagrada = Int32.Parse(String.IsNullOrEmpty(TextBox16.Text.Trim()) ? "0" : TextBox16.Text.Trim());



            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO vozac (id, ime, nacionalnost, tituli, vkupna_jacina, koncentracija, talent, agresivnost, " +
                "iskustvo, tehnika, izdrzlivost, motivacija, harizma, godini, plata, tezina, PRODOLZENIENAGRADA) VALUES (:id, :ime, :nacionalnost, " +
                ":tituli, :vkupna_jacina, :koncentracija, :talent, :agresivnost, :iskustvo, :tehnika, :izdrzlivost, :motivacija, " +
                ":harizma, :godini, :plata, :tezina, :nagrada)";
            command.Parameters.AddWithValue(":id", id);
            command.Parameters.AddWithValue(":ime", ime);
            command.Parameters.AddWithValue(":nacionalnost", nacionalnost);
            command.Parameters.AddWithValue(":tituli", tituli);
            command.Parameters.AddWithValue(":vkupna_jacina", vkupna_jacina);
            command.Parameters.AddWithValue(":koncentracija", koncentracija);
            command.Parameters.AddWithValue(":talent", talent);
            command.Parameters.AddWithValue(":agresivnost", agresivnost);
            command.Parameters.AddWithValue(":iskustvo", iskustvo);
            command.Parameters.AddWithValue(":tehnika", tehnika);
            command.Parameters.AddWithValue(":izdrzlivost", izdrzlivost);
            command.Parameters.AddWithValue(":motivacija", motivacija);
            command.Parameters.AddWithValue(":harizma", harizma);
            command.Parameters.AddWithValue(":godini", godini);
            command.Parameters.AddWithValue(":plata", plata);
            command.Parameters.AddWithValue(":tezina", tezina);
            command.Parameters.AddWithValue(":nagrada", nagrada);

            int result = command.ExecuteNonQuery();
            if (result != 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM vozac";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                DataSet data = new DataSet();
                adapter.Fill(data, "vozac");
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