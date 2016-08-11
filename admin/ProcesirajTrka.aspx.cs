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

public partial class admin_ProcesirajTrka : System.Web.UI.Page
{
    private OracleConnection connect;
    private double dolzina;
    private int kruga;
    private int preteknuvanje;
    private int gorivo;
    private int gumi;
    private double brzina;
    private double temp;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!otvoriKonekcija())
        {
            return;
        }
        try
        {
            prezemiPatekaPodatoci();
            prezemiKvalifikaciPodatoci();
            vnesiRezultati();
            presmetajNagradi();


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " " + ex.StackTrace);
            Label1.Text = ex.Message + " " + ex.StackTrace;
        }
        finally
        {
            zatvoriKonekcija();
        }
    }

    private void prezemiPatekaPodatoci()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT p.*, v.temp FROM pateka p, trka t, vremenski_uslovi v WHERE v.id = t.vreme AND p.id = t.pateka AND p.id = (SELECT pateka FROM trka WHERE sledna = 1)";

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            dolzina = double.Parse(reader["dolzina"].ToString());
            kruga = Int32.Parse(reader["kruga"].ToString());

            string pom = "";
            pom = reader["preteknuvanje"].ToString();
            switch (pom)
            {
                case "mnogu lesno":
                    preteknuvanje = 5;
                    break;
                case "lesno":
                    preteknuvanje = 4;
                    break;
                case "normalno":
                    preteknuvanje = 3;
                    break;
                case "tesko":
                    preteknuvanje = 2;
                    break;
                case "mnogu tesko":
                    preteknuvanje = 1;
                    break;
            }

            pom = reader["gorivo"].ToString();
            switch (pom)
            {
                case "mnogu niska":
                    gorivo = 1;
                    break;
                case "niska":
                    gorivo = 2;
                    break;
                case "sredna":
                    gorivo = 3;
                    break;
                case "visoka":
                    gorivo = 4;
                    break;
                case "mnogu visoka":
                    gorivo = 5;
                    break;
            }

            pom = reader["gumi"].ToString();
            switch (pom)
            {
                case "mnogu niska":
                    gumi = 1;
                    break;
                case "niska":
                    gumi = 2;
                    break;
                case "sredna":
                    gumi = 3;
                    break;
                case "visoka":
                    gumi = 4;
                    break;
                case "mnogu visoka":
                    gumi = 5;
                    break;
            }

            brzina = double.Parse(reader["brzina"].ToString());
            temp = double.Parse(reader["temp"].ToString());

        }
    }

    private void prezemiKvalifikaciPodatoci()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT r.men_id, r.gumi, r.gorivo, r.rizikpreteknuvanje, r.rizikbranenje, r.rizikslobodno, kval.vreme, kval.vreme - mini.vreme as zaostanuvanje " +
                                "FROM racesetup r,  " +
                                "(SELECT k.men_id, SUM(k.vreme) vreme  " +
                                           " FROM kvalifikaci k " +
                                           " WHERE (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) GROUP BY k.men_id " +
                               " ) kval, " +
                                "(SELECT m.grupa_broj, m.grupa_nivo, MIN(kvalvkupno.vreme) vreme FROM " +
                                       " (SELECT men_id, SUM(k.vreme) vreme FROM kvalifikaci k WHERE (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) GROUP BY k.men_id) kvalvkupno " +
                                      "  join menadzer m on m.id = kvalvkupno.men_id " +
                                       " GROUP BY (m.grupa_broj, m.grupa_nivo) " +
                               " ) mini,  " +
                               " menadzer m " +
                               " WHERE (r.sezonabroj, r.trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1)  " +
                               " AND r.men_id = kval.men_id AND m.id = kval.men_id AND m.grupa_broj = mini.grupa_broj AND m.grupa_nivo = mini.grupa_nivo";

        OracleDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            zemiPozicijaKvalifikaci(reader);
            presmetajBolidTrosenje(reader);
        }
    }

    private void zemiPozicijaKvalifikaci(OracleDataReader readerInput)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT pozicija FROM (SELECT  x.men_id, row_number() OVER (ORDER BY x.vreme ASC) as pozicija " +
		                       "FROM (SELECT k.men_id, SUM(k.vreme) vreme FROM kvalifikaci k, menadzer m WHERE m.id = k.men_id AND m.grupa_broj = (SELECT grupa_broj FROM menadzer WHERE id = :men_id) AND m.grupa_nivo = (SELECT grupa_nivo FROM menadzer WHERE id = :men_id) AND (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) GROUP BY k.men_id) x) cela " +
                                "WHERE cela.men_id = :men_id";
        command.Parameters.AddWithValue(":men_id", Int32.Parse(readerInput["men_id"].ToString()));

        OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            presmetajVreme(readerInput, reader);
        }
    }

    private void presmetajVreme(OracleDataReader readerPrv, OracleDataReader readerVtor)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT b.moknost, b.upravuvanje, b.zabrzuvanje, v.vkupna_jacina, p.brzina, obj.vkupno objvkupno, per.vkupno pervkupno " +
                                "FROM bolid b, vozacistorija i, vozac v, personal per, objekt obj, pateka p, trka t " +
                                "WHERE b.id = i.men_id AND i.vozac_id = v.id AND b.id = per.id AND obj.id = per.id AND b.id = :men_id " +
                                "AND i.end_contract_race IS NULL AND p.id = t.pateka AND t.sledna = 1";
        command.Parameters.AddWithValue(":men_id", Int32.Parse(readerPrv["men_id"].ToString()));

        OracleDataReader reader = command.ExecuteReader();
        double suma = 0.0;
        if (reader.Read())
        {
            suma = Double.Parse(reader["brzina"].ToString()) + (300.0 / (Double.Parse(reader["moknost"].ToString()) + Double.Parse(reader["upravuvanje"].ToString()) + Double.Parse(reader["zabrzuvanje"].ToString()))) + (800.0 / Double.Parse(reader["vkupna_jacina"].ToString()));
            suma += (0.2 * Double.Parse(readerPrv["gumi"].ToString()));
            suma += (0.01 * Double.Parse(readerPrv["gorivo"].ToString()));
            suma += (400.0 / (Double.Parse(reader["pervkupno"].ToString())) + Double.Parse(reader["objvkupno"].ToString()));
            suma += zaostanuvanjePoradiSetup(Int32.Parse(readerPrv["men_id"].ToString()));
            suma -= (0.03 * Double.Parse(readerPrv["rizikslobodno"].ToString()));
            Random r = new Random();
            double rand = r.Next(-10, 4);
            suma += (1 / rand); //random nekoe zaostanuvanje
        }

        suma *= kruga;
        suma -= (11 - Int32.Parse(readerVtor["pozicija"].ToString())) * 0.7;

        OracleCommand commandInsert = new OracleCommand();
        commandInsert.Connection = connect;
        commandInsert.CommandText = "UPDATE racesetup SET vreme = :suma WHERE men_id = :men_id AND (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1)";
        commandInsert.Parameters.AddWithValue(":suma", suma);
        commandInsert.Parameters.AddWithValue(":men_id", Int32.Parse(readerPrv["men_id"].ToString()));

        commandInsert.ExecuteNonQuery();
    }

    private double zaostanuvanjePoradiSetup(int menId)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT p.del_id, ABS(p.podesuvanje - r.podesuvanje) as razlika FROM podesuvanjapateka p, racesetuppodesuvanja r WHERE r.del_id = p.del_id AND (r.sezonabroj, r.trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) AND r.men_id = :men_id AND pateka = (SELECT p.id FROM trka t, pateka p WHERE t.pateka = p.id AND t.sledna = 1) ORDER BY p.del_id";
        command.Parameters.AddWithValue(":men_id", menId);
        
        OracleDataReader reader = command.ExecuteReader();

        double vrati = 0.0;

        while (reader.Read())
        {
            int setup = Int32.Parse(reader["razlika"].ToString());
            vrati +=  setup / 150;
        }
        return vrati;
    }

    private void presmetajBolidTrosenje(OracleDataReader readerInput)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT id, trosenje FROM delovi";

        OracleDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            OracleCommand commandInsert = new OracleCommand();
            commandInsert.Connection = connect;
            commandInsert.CommandText = "UPDATE bolid_delovi SET istrosenost = CASE WHEN istrosenost + :trosenje > 99 THEN 99 ELSE istrosenost + :trosenje END " +
                              "WHERE id = :men_id AND del_id = :del_id";
            commandInsert.Parameters.AddWithValue(":trosenje", reader["trosenje"].ToString());
            commandInsert.Parameters.AddWithValue(":men_id", readerInput["men_id"].ToString());
            commandInsert.Parameters.AddWithValue(":del_id", reader["id"].ToString());

            commandInsert.ExecuteNonQuery();
        }
    }

    private void vnesiRezultati()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT * FROM grupa";

        OracleDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            vnesiGrupa(reader);
        }
    }

    private void vnesiGrupa(OracleDataReader readerInput)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT m.id, r.vreme FROM menadzer m, racesetup r WHERE r.men_id = m.id AND (r.sezonabroj, r.trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) AND m.grupa_nivo = :grupa_nivo AND m.grupa_broj = :grupa_broj ORDER BY r.vreme ASC";
        command.Parameters.AddWithValue(":grupa_nivo", Int32.Parse(readerInput["grupa_nivo"].ToString()));
        command.Parameters.AddWithValue(":grupa_broj", Int32.Parse(readerInput["grupa_broj"].ToString()));

        OracleDataReader reader = command.ExecuteReader();
        int i = 1;
        while (reader.Read())
        {
            vnesiPozicija(reader, i);
            i++;
        }

    }

    private void vnesiPozicija(OracleDataReader readerInput, int pozicija)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "INSERT INTO rezultati VALUES((SELECT sezonabroj FROM trka WHERE sledna = 1), (SELECT trkabroj FROM trka WHERE sledna = 1), (SELECT grupa_nivo FROM menadzer WHERE id = :men_id), (SELECT grupa_broj FROM menadzer WHERE id = :men_id), :men_id, :pozicija)";
        command.Parameters.AddWithValue(":men_id", Int32.Parse(readerInput["id"].ToString()));
        command.Parameters.AddWithValue(":pozicija", pozicija);

        command.ExecuteNonQuery();

    }

    private void presmetajNagradi()
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "SELECT DISTINCT r.men_id, (nagrada - (pozicija - 1) * 200000 - v.plata - pers.plata - obj.plata) as nagrada " +
                               " FROM rezultati r " +
                               " join nivoime n on r.grupa_nivo = n.id " +
                                "join vozacistorija i on r.men_id = i.men_id " +
                                "join vozac v on i.vozac_id = v.id " +
                                "join ( " +
                                "    SELECT p.id, SUM(p.vrednost * ptip.cena) as plata " +
                                "    FROM personal_menadzer p " +
                                 "   join personal_tip ptip on ptip.id = p.personal_id " +
                                  "  GROUP BY p.id " +
                               " ) pers on pers.id = r.men_id " +
                               " join ( " +
                               "     SELECT o.id, SUM(o.vrednost * otip.cena) as plata " +
                               "     FROM objekt_menadzer o " +
                                "    join objekt_tip otip on otip.id = o.objekt_id " +
                                 "   GROUP BY o.id " +
                                ") obj on obj.id = r.men_id " +
                                "WHERE (sezonabroj, trkabroj) = (SELECT sezonabroj, trkabroj FROM trka WHERE sledna = 1) " +
                                "AND i.end_contract_season IS NULL";

        OracleDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            odzemiOdBudget(Int32.Parse(reader["men_id"].ToString()), Int32.Parse(reader["nagrada"].ToString()));
        }

    }

    private void odzemiOdBudget(int menId, int suma)
    {
        OracleCommand command = new OracleCommand();
        command.Connection = connect;
        command.CommandText = "UPDATE menadzer SET budzet = budzet + :suma WHERE id = :id";
        command.Parameters.AddWithValue(":suma", suma);
        command.Parameters.AddWithValue(":id", menId);

        command.ExecuteNonQuery();
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
}