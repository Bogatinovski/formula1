using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Collections;

public partial class user_Shortlist : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();

        if (!Page.IsPostBack)
        {
           initialize();
        }
        if (gvDostapniVozaci.Rows.Count > 0)
        {
            gvDostapniVozaci.UseAccessibleHeader = true;
            gvDostapniVozaci.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvNedostapniVozaci.Rows.Count > 0)
        {
            gvNedostapniVozaci.UseAccessibleHeader = true;
            gvNedostapniVozaci.HeaderRow.TableSection = TableRowSection.TableHeader;
        } 
    }

    private void initialize()
    {
        if(!otvoriKonekcija())
        {
            //ne uspea konekcijata
            return;
        }
        try
        {
            zemiDostapniVozaci();
            zemiNedostapniVozaci();
            statusBudzetManager();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            lblNemaDostapniVozaci.Text = ex.Message;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void zemiDostapniVozaci()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT v.* FROM shortlist s, vozac v, vozacistorija i WHERE s.id = v.id AND i.vozac_id = v.id AND i.end_contract_season IS NULL AND s.tip = 1 AND s.men_id = :men_id AND i.men_id = :sopstvenik " +
                                "UNION ( " +
                                "SELECT DISTINCT v.* FROM shortlist s, vozac v, vozacistorija i WHERE s.id = v.id AND i.vozac_id(+) = v.id AND (i.end_contract_season IS NOT NULL OR (i.end_contract_season IS NULL AND i.start_contract_season IS NULL) ) AND s.tip = 1 AND s.men_id = :men_id " +
                                ") ";
        //ako go nema nikoj dotogas angazirano togas i pocetok i start se null
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":sopstvenik", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "DostapniVozaci");

            gvDostapniVozaci.DataSource = ds;
            gvDostapniVozaci.DataBind();
        }
        else
        {
            lblNemaDostapniVozaci.Visible = true;
        }
    }

    private void zemiNedostapniVozaci()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT v.* FROM shortlist s, vozac v, vozacistorija i WHERE s.id = v.id AND i.vozac_id = v.id AND i.end_contract_season IS NULL AND s.tip = 1 AND s.men_id = :men_id AND i.men_id <> :sopstvenik";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":sopstvenik", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "NedostapniVozai");

            gvNedostapniVozaci.DataSource = ds;
            gvNedostapniVozaci.DataBind();
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

    protected void checkLoggedUser()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            menId = Convert.ToInt32(Session["user"].ToString());
        }
    }

    protected void gvNedostapniVozaci_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        brisiOdShortlist(Convert.ToInt32(gvNedostapniVozaci.Rows[e.RowIndex].Cells[0].Text));
    }

    private void brisiOdShortlist(int id)
    {
        if (!otvoriKonekcija())
        {
            //problem so konekcija
            return;
        }
        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "DELETE FROM shortlist WHERE men_id = :men_id AND id = :id AND tip =1";
            command.Parameters.AddWithValue(":men_id", menId);
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
            Response.Redirect("Shortlist.aspx");
        }
    }

    protected void gvDostapniVozaci_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        brisiOdShortlist(Convert.ToInt32(gvDostapniVozaci.Rows[e.RowIndex].Cells[0].Text));
    }

    private void dodajVoShortlist(int id)
    {
        if (!otvoriKonekcija())
        {
            //problem so konekcija
            return;
        }
        try
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "INSERT INTO shortlist VALUES(:mend_id, :id, 1)";
            command.Parameters.AddWithValue(":men_id", menId);
            command.Parameters.AddWithValue(":id", id);

            command.ExecuteNonQuery();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("Shortlist.aspx");
        }
    }

    protected string convertToCurrency(int broj)
    {
        return string.Format("{0:C0}", broj);
    }

    protected string convertToCurrency(string broj)
    {
        int br = Int32.Parse(broj);
        return string.Format("{0:C0}", br);
    }

    protected int convertToNumber(string broj)
    {
        return Int32.Parse(broj.Replace(",", "").Replace("$", ""));
    }

    private void statusBudzetManager()
    {
        OracleCommand command1 = new OracleCommand();
        command1.Connection = connect;
        command1.CommandText = "SELECT ime || ' ' || prezime as name, budzet FROM menadzer WHERE id = :men_id";
        command1.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader1 = command1.ExecuteReader();
        if (reader1.Read())
        {
            lblBudzetStatus.Text = convertToCurrency(reader1["budzet"].ToString());
            lblManagerStatus.Text = reader1["name"].ToString();
        }
    }
   
}