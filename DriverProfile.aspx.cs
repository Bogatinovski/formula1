using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;

public partial class user_DriverProfile : System.Web.UI.Page
{
    private OracleConnection connect;
    private OracleCommand commandDriverProfile;
    private OracleCommand commandDriverHistory;
    private OracleCommand commandCurrManager;
    private OracleCommand commandCurrOffers;
    private OracleDataAdapter adapterIstorija;
    private DataSet dsIstorija;
    private OracleDataAdapter adapterCurrOffers;
    private DataSet dsCurrOffers;
    private int menId;
    private int vozacId;
    private bool imaPonudaOdMenadzerot;

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLoggedUser();
       
        bool eBroj = Int32.TryParse(Request.QueryString["id"], out vozacId);// se proverva dali kako
        //parametar e prosleden broj i se zacuvav id na vozacot

        if (Request.QueryString["id"] != null && !Page.IsPostBack)
        {
            if (!otvoriKonekcija())
            {
                //problem so konekcija
                return;
            }
            try
            {
                statusBudzetManager();
                eNaListaZelbi();
                prezemiVozacPodatoci();
                prezemiMomentalenSopstvenik();
                prezemiIstorijaVozaci();

                //se zemat site podatoci za vozacot
                OracleDataReader reader = commandDriverProfile.ExecuteReader();

                if (reader.Read())//ako postoi takov vozac so dadeno id
                {
                    //istorijata vo sekoj slucaj postoi
                    OracleDataReader readerHistory = commandDriverHistory.ExecuteReader();
                    if (readerHistory.HasRows)
                    {
                        lblIzvestuvanje.Visible = false;
                        adapterIstorija.Fill(dsIstorija, "History");
                        gvHistory.DataSource = dsIstorija;
                        gvHistory.DataBind();
                    }
                    else
                    {
                        gvHistory.Visible = false;
                    }

                    //momentalen menadzer
                    OracleDataReader readerCurrManager = commandCurrManager.ExecuteReader();
                    string currMenId = null;
                    bool hasManager = readerCurrManager.HasRows;
                    if (hasManager)
                    {
                        //има сопственик
                        readerCurrManager.Read();
                        lblOwner.Text = readerCurrManager["ime"].ToString();
                        linkManager.NavigateUrl = "ManagerProfile.aspx?id=" + readerCurrManager["id"].ToString();
                        currMenId = readerCurrManager["id"].ToString();
                        lblGroup.Text = readerCurrManager["grupa"].ToString();
                        linkGroup.NavigateUrl = "Standings.aspx?level=" + readerCurrManager["grupa_nivo"].ToString() + "&id=" + readerCurrManager["grupa_broj"].ToString();
                        lblSalary.Text = String.Format("{0:C0}", Convert.ToDecimal(readerCurrManager["plata"].ToString()));
                        lblContractDuration.Text = readerCurrManager["dolzinadogovor"].ToString() + " трки";

                        deaktivirajPonuda("Овој возач веќе има склучено договор со друг менаџер");
                    }
                    else
                    {
                        //nema menadzer vo momentov
                        lblCurrContract.Text = "Понуди во моментов";

                        prezemiMomentalniPonudi();

                        OracleDataReader readerCurrOffers = commandCurrOffers.ExecuteReader();
                        if (readerCurrOffers.HasRows)
                        {
                            //ima ponudi za vozacot
                            currOwner.Visible = false;
                            noCurrOwner.Visible = false;
                            gvCurrOffers.Visible = true;

                            adapterCurrOffers.Fill(dsCurrOffers, "History");
                            gvCurrOffers.DataSource = dsCurrOffers;
                            gvCurrOffers.DataBind();

                            txtPonudaPlata.Text = reader["plata"].ToString();
                            txtPonudaPotpisuvanje.Text = reader["nagradapripotpisuvanje"].ToString();

                            while (readerCurrOffers.Read())
                            {
                                imaPonudaOdMenadzerot = false;
                                if (readerCurrOffers["id"].ToString().Equals(Session["user"].ToString()))
                                {
                                    imaPonudaOdMenadzerot = true;
                                }
                            }
                            if (imaPonudaOdMenadzerot)
                            {
                                deaktivirajPonuda("Веќе имате активна понуда кон овој возач. Доколку сакате да испратите нова понуда <a href=\"YourCurrOffers.aspx\">избришете ја претходната,</a> па потоа поставете нова!");
                            }

                        }
                        else
                        {
                            //nema aktivni ponudi
                            currOwner.Visible = false;
                            noCurrOwner.Visible = true;
                            txtPonudaPlata.Text = reader["plata"].ToString();
                            txtPonudaPotpisuvanje.Text = reader["nagradapripotpisuvanje"].ToString();
                        }



                    }

                    lblIme.Text = reader["ime"].ToString();
                    lblNacionalnost.Text = reader["nacionalnost"].ToString();
                    lblTituli.Text = reader["tituli"].ToString();
                    lblBrTrki.Text = reader["brtrki"].ToString();
                    lblPobedi.Text = reader["pobedi"].ToString();
                    lblPodiumi.Text = reader["podiumi"].ToString();
                    lblOsvoeniPoeni.Text = reader["poeni"].ToString();
                    lblNajbrzKurg.Text = reader["najbrzikrugovi"].ToString();
                    lblPolPozicii.Text = reader["polpozicii"].ToString();
                    lblAvgPtsPerRace.Text = reader["prosekpoeni"].ToString();

                    lblOA.Text = reader["vkupna_jacina"].ToString();



                    if (currMenId != null && Session["user"] != null && currMenId.Equals(Session["user"].ToString()))
                    {
                        //ako e vo sopstvenost na menadzerot
                        deaktivirajPonuda("Овој возач веќе има склучено договор со Вас");
                    }
                    if ((currMenId != null && Session["user"] != null && currMenId.Equals(Session["user"].ToString())) || !hasManager)
                    {
                        //ako e vo sopstvenost na menadzerot ili nema menadzer
                        lblConcentration.Text = reader["koncentracija"].ToString();
                        lblTalent.Text = reader["talent"].ToString();
                        lblAggresiveness.Text = reader["agresivnost"].ToString();
                        lblExperience.Text = reader["iskustvo"].ToString();
                        lblTI.Text = reader["tehnika"].ToString();
                        lblStamina.Text = reader["izdrzlivost"].ToString();
                        lblMotivation.Text = reader["motivacija"].ToString();
                        lblCharisma.Text = reader["harizma"].ToString();


                    }

                    lblWeight.Text = reader["tezina"].ToString();
                    lblAge.Text = reader["godini"].ToString();

                }
                else
                {
                    //vozacot so dadeno id ne postoi
                    id1.Visible = false;
                    lblNemaVozac.Visible = true;
                }

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
        else
        {
            //ne e specificirano id na vozac
        }
    }

    private void prezemiVozacPodatoci()
    {
        //informaci za vozac
        commandDriverProfile = new OracleCommand();
        commandDriverProfile.Connection = connect;
        commandDriverProfile.CommandText = "SELECT * FROM vozac WHERE id = :id";
        commandDriverProfile.Parameters.AddWithValue(":id", vozacId);

    }

    private void prezemiMomentalenSopstvenik()
    {
        //za da go zemam momentalniot menadzer
        commandCurrManager = new OracleCommand();
        commandCurrManager.Connection = connect;
        commandCurrManager.CommandText = "Select m.id, m.ime || ' ' || m.prezime as \"ime\", n.ime || ' - ' || m.grupa_broj as \"grupa\", m.grupa_nivo, m.grupa_broj, d.plata, d.dolzinadogovor " +
                                        "FROM menadzer m, vozac d, vozacistorija v, nivoime n " +
                                        "WHERE m.id=v.men_id AND d.id = v.vozac_id AND d.id = :id " +
                                        "AND n.id=m.grupa_nivo AND end_contract_season IS NULL";
        commandCurrManager.Parameters.AddWithValue(":id", vozacId);

        
    }

    private void prezemiIstorijaVozaci()
    {
        //informacii za istorija na vozac so menadzeri
        commandDriverHistory = new OracleCommand();
        commandDriverHistory.Connection = connect;
        commandDriverHistory.CommandText = "Select m.id, m.ime || ' ' || m.prezime as \"menadzer\", d.* FROM " +
                                            "VOZACISTORIJA d " +
                                            "join MENADZER m on m.id=d.men_id " +
                                            "where d.vozac_id = :id";
        commandDriverHistory.Parameters.AddWithValue(":id", vozacId);

        adapterIstorija = new OracleDataAdapter();
        adapterIstorija.SelectCommand = commandDriverHistory;
        dsIstorija = new DataSet();
    }

    private void prezemiMomentalniPonudi()
    {
        //dali ima momentalni ponudi
        commandCurrOffers = new OracleCommand();
        commandCurrOffers.Connection = connect;
        commandCurrOffers.CommandText = "SELECT m.id, m.ime || ' '  || m.prezime as \"menadzer\", n.ime || ' - ' || m.grupa_broj as \"grupa\", m.grupa_nivo, m.grupa_broj " +
                                            "FROM menadzer m, momentalniponudi p, nivoime n " +
                                            "WHERE m.id = p.men_id AND n.id=m.grupa_nivo AND p.vozac_id = :id ORDER BY prioritet";
        commandCurrOffers.Parameters.AddWithValue(":id", vozacId);

        adapterCurrOffers = new OracleDataAdapter();
        adapterCurrOffers.SelectCommand = commandCurrOffers;
        dsCurrOffers = new DataSet();
    }

    private void eNaListaZelbi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT men_id FROM shortlist WHERE tip = 1 AND men_id = :men_id AND id = :id";
        command.Parameters.AddWithValue(":men_id", menId);
        command.Parameters.AddWithValue(":id", vozacId);

        OracleDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            btnShortlist.Text = "Отстрани го од листа на желби";    
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

    protected String pecatiIstorija(string sezona, string trka)
    {

        if (sezona == null || sezona.Equals(""))
            return "Не е уште завршен";
        else
            return "Сезона " + sezona + ", Трка " + trka;
    }

    protected string pecatiImeMenadzerIstorija(string id, string ime)
    {
        return "<a href=\"ManagerProfile.aspx?id=" + id + "\">" + ime + "</a>";
    }

    protected string pecatiGrupa(string nivo, string grBroj, string ime)
    {
        return "<a href=\"Standings.aspx?level=" + nivo + "&id=" + grBroj +"\">" + ime + "</a>";
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

    private void deaktivirajPonuda(string poraka){
        lblIzvestuvanjePonuda.Visible = true;
        lblIzvestuvanjePonuda.Text = poraka;
        txtPonudaDogovor.Enabled = false;
        txtPonudaPlata.Enabled = false;
        txtPonudaPodium.Enabled = false;
        txtPonudaPotpisuvanje.Enabled = false;
        txtPonudaPobeda.Enabled = false;
        btnOfferContact.Enabled = false;
    }

    protected void btnOfferContact_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        OracleCommand command = new OracleCommand();
        if (!otvoriKonekcija())
            return;
        try
        {
            command.Connection = connect;
            command.CommandText = "INSERT INTO momentalniponudi VALUES (:men_id, :vozac_id, :dolzinadogovor, :plata, :nagradapotpisuvanje, :pobedabonus, :podiumbonus, :prioritet)";
            command.Parameters.AddWithValue(":men_id", Convert.ToInt32(Session["user"].ToString()));
            command.Parameters.AddWithValue(":vozac_id", Convert.ToInt32(Request.QueryString["id"].ToString()));
            command.Parameters.AddWithValue(":dolzinadogovor", txtPonudaDogovor.Text);
            command.Parameters.AddWithValue(":plata", Convert.ToInt32(txtPonudaPlata.Text.Replace(",","")));
            command.Parameters.AddWithValue(":nagradapotpisuvanje", Convert.ToInt32(txtPonudaPotpisuvanje.Text.Replace(",", "")));
            int pobeda;
            Int32.TryParse(txtPonudaPobeda.Text, out pobeda);
            command.Parameters.AddWithValue(":pobedabonus", pobeda);
            int podium;
            Int32.TryParse(txtPonudaPodium.Text, out podium);
            command.Parameters.AddWithValue(":podiumbonus", podium);
            command.Parameters.AddWithValue(":prioritet", 1);

            odzemiOdBudezet();

            deaktivirajPonuda("");

            command.ExecuteNonQuery();


        }catch(Exception ex){
            Console.WriteLine(ex.Message);
            lblIzvestuvanjePonuda.Visible = true;
            lblIzvestuvanjePonuda.Text = "Настана грешка. Ве молиме обидете се повторно";
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("YourCurrOffers.aspx");
        }
    }

    protected void btnShortlist_Click(object sender, EventArgs e)
    {
        if (btnShortlist.Text.Equals("Отстрани го од листа на желби"))
        {
            brisiOdShortlist(Convert.ToInt32(Request.QueryString["id"].ToString()));
        }
        else
        {
            dodajVoShortlist(Convert.ToInt32(Request.QueryString["id"].ToString()));
        }
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
            //lblTalent.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
            Response.Redirect("Shortlist.aspx");
        }
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
            command.CommandText = "INSERT INTO shortlist VALUES(:men_id, :id, 1)";
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

    private void odzemiOdBudezet()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet - 500000 WHERE id = :men_id";
        command.Parameters.AddWithValue(":men_id", menId);

        command.ExecuteNonQuery();
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