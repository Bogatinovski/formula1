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

public partial class user_Default : System.Web.UI.Page
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
        if (gvCurrOffers.Rows.Count > 0)
        {
            gvCurrOffers.UseAccessibleHeader = true;
            gvCurrOffers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private void initialize()
    {
        if (!otvoriKonekcija())
        {
            //ne uspea konekcijata
            return;
        }
        try
        {
            zemiMomentalniPonudi();
            statusBudzetManager();
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

    private void zemiMomentalniPonudi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT x.*, y.brponudi FROM (SELECT m.*, v.ime FROM momentalniponudi m, vozac v WHERE v.id = m.vozac_id AND men_id = :id ORDER BY prioritet) x join (SELECT vozac_id, count(*) as brponudi FROM momentalniponudi GROUP BY vozac_id) y on x.vozac_id = y.vozac_id";
        command.Parameters.AddWithValue(":id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "MomPonudi");

            gvCurrOffers.DataSource = ds;
            gvCurrOffers.DataBind();

            lblAktivniPonudi.Visible = false;
        }
        else
        {

        }



    }

    protected string pecatiIme(string id, string ime)
    {
        return   ime ;
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