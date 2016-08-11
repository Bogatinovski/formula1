using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListaCekanje : System.Web.UI.Page
{
    private OracleConnection connect;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
            return;

        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT * from LISTANACHEKANJE";

            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;

            DataSet data = new DataSet();
            adapter.Fill(data, "LISTANACHEKANJE");
            GridView1.DataSource = data;
            GridView1.DataBind();
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
            labelError.Text = ex.Message + "\n" + ex.StackTrace;
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
            command.CommandText = "DELETE FROM listanachekanje WHERE men_id = :id";
            command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text));
            int vrati = command.ExecuteNonQuery();
            if (vrati > 0)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * from LISTANACHEKANJE";

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;

                DataSet data = new DataSet();
                adapter.Fill(data, "LISTANACHEKANJE");
                GridView1.DataSource = data;
                GridView1.DataBind();
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TextBox1.Text.Equals(""))
        {
            labelError.Text = "Изберете група";
            return;
        }
            
        if (!otvoriKonekcija())
            return;

        try
        {
            List<int> ids = new List<int>();

            // Vnesuvanje vo tabelata menadzer 
            OracleCommand  command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO menadzer (id, ime, prezime, budzet, email, username, lozinka, drzava, grupa_broj, grupa_nivo, BRTRKI, POENI, POBEDI, PODIUMI, POLPOZICII, PROSEKPOENI) "
            + "VALUES(:id, :ime, :prezime, 30000000, :email, :username, :lozinka, :drzava, :grupa_broj, 1, 0, 0, 0, 0, 0, 0)";
            command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.SelectedRow.Cells[0].Text));
            command.Parameters.AddWithValue(":ime", GridView1.SelectedRow.Cells[1].Text);
            command.Parameters.AddWithValue(":prezime", GridView1.SelectedRow.Cells[2].Text);
            command.Parameters.AddWithValue(":email", GridView1.SelectedRow.Cells[3].Text);
            command.Parameters.AddWithValue(":username", GridView1.SelectedRow.Cells[4].Text);
            command.Parameters.AddWithValue(":lozinka", GridView1.SelectedRow.Cells[5].Text);
            command.Parameters.AddWithValue(":drzava", GridView1.SelectedRow.Cells[6].Text);
            command.Parameters.AddWithValue(":grupa_broj", Int32.Parse(TextBox1.Text));

            command.ExecuteNonQuery();

            /// Dodeluvanje vozac
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT id FROM vozac v WHERE v.vkupna_jacina < 85 and  v.id not in (select vozac_id from vozacistorija where END_CONTRACT_SEASON is null)";

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
                ids.Add(Int32.Parse(reader["id"].ToString()));
            int result = ids[new Random().Next(0, ids.Count)];

            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO vozacistorija (men_id, vozac_id, START_CONTRACT_SEASON, START_CONTRACT_RACE) " +
                "VALUES(:men_id, :vozac_id, :start_contract_season, :start_contract_race)";
            command.Parameters.AddWithValue(":men_id", Int32.Parse(GridView1.SelectedRow.Cells[0].Text));
            command.Parameters.AddWithValue(":vozac_id", result);
            command.Parameters.AddWithValue(":start_contract_season", 1);
            command.Parameters.AddWithValue(":start_contract_race", 1);
            command.ExecuteNonQuery();
            

            ///// Brisenje od tabelata Listanachekanje
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "DELETE FROM LISTANACHEKANJE WHERE men_id = :men_id";
            command.Parameters.AddWithValue(":men_id", Int32.Parse(GridView1.SelectedRow.Cells[0].Text));
            command.ExecuteNonQuery(); 



            /// Dodavanje bolid
             command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO bolid (id, moknost, upravuvanje, zabrzuvanje, DOZVOLIUPDATE) VALUES(:id, 13, 13, 13, 1)";
            command.Parameters.AddWithValue(":id", Convert.ToInt32(GridView1.SelectedRow.Cells[0].Text));

            command.ExecuteNonQuery(); 
                                       
            
            /// Dodavanje vo bolid_delovi za noviot bolid
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT id FROM delovi";
            reader = command.ExecuteReader();
           
            
            ids = new List<int>();
            while(reader.Read())
                ids.Add(Int32.Parse(reader["id"].ToString()));

            foreach (int id in ids)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "INSERT INTO bolid_delovi VALUES(:id, :del_id, 1, 0)";
                command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.SelectedRow.Cells[0].Text));
                command.Parameters.AddWithValue(":del_id", id);
                command.ExecuteNonQuery();
            }

            ///Dodavanje vo Objeki
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO objekt VALUES(:id, 0, 1)";
            command.Parameters.AddWithValue(":id", Convert.ToInt32(GridView1.SelectedRow.Cells[0].Text));
            command.ExecuteNonQuery();

            /// Dodavanje vo menadzer_objekt
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT id FROM objekt_tip";
            reader = command.ExecuteReader();


            ids = new List<int>();
            while (reader.Read())
                ids.Add(Int32.Parse(reader["id"].ToString()));

            foreach (int id in ids)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "INSERT INTO objekt_menadzer VALUES(:id, :del_id, 0)";
                command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.SelectedRow.Cells[0].Text));
                command.Parameters.AddWithValue(":del_id", id);
                command.ExecuteNonQuery();
            }

            ///Dodavanje vo Personal
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO personal VALUES(:id, 12, 1)";
            command.Parameters.AddWithValue(":id", Convert.ToInt32(GridView1.SelectedRow.Cells[0].Text));
            command.ExecuteNonQuery();

            /// Dodavanje vo menadzer_objekt
            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT id FROM personal_tip";
            reader = command.ExecuteReader();


            ids = new List<int>();
            while (reader.Read())
                ids.Add(Int32.Parse(reader["id"].ToString()));

            foreach (int id in ids)
            {
                command = new OracleCommand();
                command.Connection = connect;
                command.CommandText = "INSERT INTO personal_menadzer VALUES(:id, :del_id, 10)";
                command.Parameters.AddWithValue(":id", Int32.Parse(GridView1.SelectedRow.Cells[0].Text));
                command.Parameters.AddWithValue(":del_id", id);
                command.ExecuteNonQuery();
            }

            command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT * from LISTANACHEKANJE order by men_id";

            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;

            DataSet data = new DataSet();
            adapter.Fill(data, "LISTANACHEKANJE");
            GridView1.DataSource = data;
            GridView1.DataBind();
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