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
using System.Text.RegularExpressions;

public partial class user_Objekti : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    private int brojNaTipoviObjekti;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        if(!Page.IsPostBack)
            initialize();
        else
        {
            presmetajVkupnoPlakjanje();
        }
        
    }

    private void initialize()
    {
        if (!otvoriKonekcija())
        {
            //ima problem so konekcijata
            return;
        }
        try
        {

            ispolniAtributi();
            negativenBalans();
            statusBudzetManager();
            imaVekjeTrenirano();
            brojNaTipoviObjekti = zemiBrojNaTipoviObjekti();

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
    private void ispolniAtributi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT o.vrednost, t.cena, t.ime, t.trening_cena, t.id, obj.vkupno FROM objekt_menadzer o, objekt_tip t, objekt obj WHERE obj.id = o.id AND o.objekt_id = t.id AND o.id = :men_id ORDER BY t.id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataAdapter adapter = new OracleDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "PersonalAtributi");

        gvFacilitiesLevel.DataSource = ds;
        gvFacilitiesLevel.DataBind();

        gvTreninzi.DataSource = ds;
        gvTreninzi.DataBind();

        //se inicijalizira dropdown za treniranje
        int plata = 0;
        OracleDataReader reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            plata += (Int32.Parse(reader["cena"].ToString()) * Int32.Parse(reader["vrednost"].ToString()));
            lblVkupno.Text = reader["vkupno"].ToString();
        }

        //presmetka na vkupna cena za plata za personal
        lblPlata.Text = convertToCurrency(plata);

        
    }

    protected ArrayList LoadDDL(string cena, string nivo)
    {

        ArrayList lista = new ArrayList();
        lista.Add("Не се менува");
        int price = Int32.Parse(cena);
        int level = Int32.Parse(nivo);
        int i;
        for (i = 0; i < level; i++)
        {
            lista.Add("Намали го нивото на " + i.ToString());
        }
        lista.Add("--------------------------------------------------");
        for (i = level + 1; i <= level + 20; i++)
        {
            int dodaj = (int)(i * price);
            lista.Add("Надгради на ниво " + i.ToString() + " - " + convertToCurrency(dodaj));
        }

            return lista;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //DropDownList l = gvTreninzi.Rows[2].Cells[1].FindControl("ddlNivo") as DropDownList;
        //Label1.Text = Regex.Match(l.SelectedValue.ToString(), @" \d+").Value;
        //lblVkupnoPlakjanje.Text = Regex.Match(l.SelectedValue.ToString(), @"\$\d*.\d*").Value.Replace("$", "").Replace(",", "");

        if (!otvoriKonekcija())
        {
            //ima problem so konekijata;
            return;
        }
        try
        {
            for (int i = 0; i < brojNaTipoviObjekti; i++)
            {
                DropDownList l = gvTreninzi.Rows[i].Cells[1].FindControl("ddlNivo") as DropDownList;
                string test = Regex.Match(l.SelectedValue.ToString(), @" \d+").Value;
                if (test != null && !test.Equals(""))//ako e nesto smeneto
                {                        
                    int nivo = Int32.Parse(Regex.Match(l.SelectedValue.ToString(), @" \d+").Value);
                    izmeniNivoNaObjekt((i + 1), nivo);

                }
            }

            odzemiOdBudezet();
            presmetajVkupno();
            onevozmoziTreniranje();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
           // Label1.Text = ex.Message + " " + ex.StackTrace; 
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("Objekti.aspx");
        }
    }

    private int zemiBrojNaTipoviObjekti()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT COUNT(*) count FROM objekt_tip";

        OracleDataReader reader = command.ExecuteReader();
        reader.Read();
        return Int32.Parse(reader["count"].ToString());
    }

    private void izmeniNivoNaObjekt(int objekt, int nivo)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE objekt_menadzer SET vrednost = :nivo WHERE id = :men_id AND objekt_id = :objekt_id";
        command.Parameters.AddWithValue(":nivo", nivo);
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":objekt_id", objekt);

        command.ExecuteNonQuery();
    }

    private void odzemiOdBudezet()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - :cena WHERE id = :men_id";
        command.Parameters.AddWithValue(":cena", convertToNumber(lblVkupnoPlakjanje.Text));
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void onevozmoziTreniranje(){
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE objekt SET dozvolitreniranje = 0 WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void presmetajVkupno()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE objekt SET vkupno = ROUND((SELECT SUM(vrednost * vkupna_jacina) FROM objekt_tip t, objekt_menadzer o WHERE o.objekt_id = t.id AND o.id = :men_id)) WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void imaVekjeTrenirano()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT dozvolitreniranje FROM objekt WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader["dozvolitreniranje"].ToString().Equals("0"))
        {
            btnTreniraj.Enabled = false;
            lblIzvestuvanje.Visible = true;
            lblIzvestuvanje.Text = "Ги имате надргардено објектите во пресрет на следната трка. После завршувањето на истата ќе можете повторно да ги надградите.";
        }
    }



    private void negativenBalans()
    {

        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT budzet FROM menadzer WHERE id = :id";
        command.Parameters.AddWithValue(":id", menId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read() && Convert.ToInt32(reader["budzet"].ToString()) < 0)
        {
            btnTreniraj.Enabled = false;
            lblIzvestuvanje.Visible = true;
            lblIzvestuvanje.Text = "Не може да ги надградите објектите, имате негативен баланс";
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

    private void presmetajVkupnoPlakjanje()
    {
        if (!otvoriKonekcija())
        {

        }
        try
        {
            brojNaTipoviObjekti = zemiBrojNaTipoviObjekti();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            zatvoriKonekcija();
        }
        int suma = 0;
        //lblVkupno.Text = brojNaDelovi.ToString();
        for (int i = 0; i < brojNaTipoviObjekti; i++)
        {
            DropDownList l = gvTreninzi.Rows[i].Cells[1].FindControl("ddlNivo") as DropDownList;
            string test = Regex.Match(l.SelectedValue.ToString(), @"\$\d*.\d*.\d*").Value.Replace("$", "").Replace(",", "");
            if (test != null && !test.Equals(""))//ako e nesto smeneto
            {
                suma += Int32.Parse(Regex.Match(l.SelectedValue.ToString(), @"\$\d*.\d*.\d*").Value.Replace("$", "").Replace(",", ""));
            }
        }

        lblVkupnoPlakjanje.Text = convertToCurrency(suma);
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