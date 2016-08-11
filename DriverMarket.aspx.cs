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

public partial class user_DriverMarket : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        initialize();
        if (gvDriverMarket.Rows.Count > 0)
        {
            gvDriverMarket.UseAccessibleHeader = true;
            gvDriverMarket.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
       
    }

    private void initialize()
    {
        if (!otvoriKonekcija())
        {
            //problem so konekcija
            return;
        }
        try
        {
            checkLoggedUser();
            statusBudzetManager();
            vcitajVozaci();
            
        }
        catch (Exception ex)
        {
            lblNemaDostapniVozaci.Visible = true;
            lblNemaDostapniVozaci.Text = "Има проблем со конекцијата" + ex.Message;
        }
        finally{
            zatvoriKonekcija();
        }
        
    }

    private void vcitajVozaci()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT v.id, v.ime, v.vkupna_jacina, v.godini, nvl(v.nagradapripotpisuvanje,0) nagradapripotpisuvanje, nvl(v.plata,0) plata, nvl(m.count, 0) brponudi " +
                                "FROM vozac v " +
                                "left outer join (SELECT vozac_id, COUNT(*) count FROM momentalniponudi GROUP BY vozac_id ) m on m.vozac_id = v.id " +
                                "WHERE v.id not in (select vozac_id from vozacistorija where END_CONTRACT_SEASON is null)";

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "DostapniVozaci");

            gvDriverMarket.DataSource = ds;
            gvDriverMarket.DataBind();
        }
        else
        {
            lblNemaDostapniVozaci.Visible = true;
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