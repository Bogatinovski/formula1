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

public partial class user_Bolid : System.Web.UI.Page
{
    private OracleConnection connect;
    private int menId;
    private int brojNaDelovi;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
        if(!Page.IsPostBack)
            initialize();
        else
        {
            presmetajVkupno();
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
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = "SELECT bd.*, d.* FROM bolid b, bolid_delovi bd, delovi d WHERE b.id = bd.id AND bd.del_id = d.id AND b.id = :men_id ORDER BY d.id";
            command.Parameters.AddWithValue(":men_id", menId);

            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Bolid");

            gvBolid.DataSource = ds;
            gvBolid.DataBind();

            statusBudzetManager();
            negativenBalans();
            //ako ima update da se onevozmozi kopceto
            imaVekjUpdate();

            brojNaDelovi = zemiBrojNaDelovi();
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

    protected ArrayList LoadDDL(string cena, string nivo)
    {
        
        ArrayList lista = new ArrayList();
        lista.Add("Нема промени");
        lista.Add(cena + " - ниво 1");
        int price = Int32.Parse(cena);
        int level = Int32.Parse(nivo);
        for (int i = 1; i <= level; i++ )
        {
            int dodaj = (int)(i * 1.2 * price);
            lista.Add(dodaj.ToString() + " - ниво " + (i+1));
        }

            return lista;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //DropDownList l = gvBolid.Rows[0].Cells[2].FindControl("ddlNivo") as DropDownList;
        //Label1.Text = Regex.Match(l.SelectedValue.ToString(), @" \d+").Value;
        if (!otvoriKonekcija())
        {
            //ima problem so konekijata;
            return;
        }
        try
        {
            for (int i = 0; i < brojNaDelovi; i++)
            {
                DropDownList l = gvBolid.Rows[i].Cells[2].FindControl("ddlNivo") as DropDownList;
                string test = Regex.Match(l.SelectedValue.ToString(), @"\d+").Value;
                if (test != null && !test.Equals(""))//ako e nesto smeneto
                {
                    int cena = Int32.Parse(Regex.Match(l.SelectedValue.ToString(), @"\d+").Value);
                    int nivo = Int32.Parse(Regex.Match(l.SelectedValue.ToString(), @" \d+").Value);
                    izmeniNivoNaDel((i + 1), nivo);
                }
            }
            odzemiOdBudezet();

            onevozmoziUpdate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("Bolid.aspx");
        }
    }

    private int zemiBrojNaDelovi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT COUNT(*) count FROM delovi";

        OracleDataReader reader = command.ExecuteReader();
        reader.Read();
        return Int32.Parse(reader["count"].ToString());
    }
    private void izmeniNivoNaDel(int del, int nivo)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE bolid_delovi SET nivo = :nivo, istrosenost = 0 WHERE id = :men_id AND del_id = :del_id";
        command.Parameters.AddWithValue(":nivo", nivo);
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":del_id", del);

        command.ExecuteNonQuery();
    }

    private void odzemiOdBudezet()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - :cena WHERE id = :men_id";
        command.Parameters.AddWithValue(":cena", Int32.Parse(Regex.Match(lblVkupno.Text, @"\d+").Value));
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void onevozmoziUpdate(){
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE bolid SET dozvoliupdate = 0 WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
    }

    private void presmetajPHA()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT ";
        
    }

    private void imaVekjUpdate()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT dozvoliupdate FROM bolid WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        OracleDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader["dozvoliupdate"].ToString().Equals("0"))
        {
            btnUpdate.Enabled = false;
            lblIzvestuvanje.Visible = true;
            lblIzvestuvanje.Text = "Веќе имате променето делови на болидот пред трката што следува. После завршувањето на трката ќе можете да пормените делови повторно.";
        }
    }

    private void presmetajVkupno()
    {
        if (!otvoriKonekcija())
        {

        }
        try
        {
            brojNaDelovi = zemiBrojNaDelovi();
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
        for (int i = 0; i < brojNaDelovi; i++)
        {
            DropDownList l = gvBolid.Rows[i].Cells[2].FindControl("ddlNivo") as DropDownList;
                string test = Regex.Match(l.SelectedValue.ToString(), @"\d+").Value;
                if (test != null && !test.Equals(""))//ako e nesto smeneto
                {
                    suma += Int32.Parse(Regex.Match(l.SelectedValue.ToString(), @"\d+").Value);
                }
        }

        lblVkupno.Text = suma.ToString() + "$";
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
            btnUpdate.Enabled = false;
            lblIzvestuvanje.Visible = true;
            lblIzvestuvanje.Text = "Не може да променувате делови, имате негативен баланс";
        }

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
}