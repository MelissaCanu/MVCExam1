using MVCExam1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCExam1.Controllers
{

    //in questo controller mi connetto al db, mostro l'elenco dei verbali e permetto di aggiungerne uno nuovo

    public class VerbaleController : Controller
    {
        // GET: Verbale
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<Verbale> listVerbale = new List<Verbale>();

            try
            {
                //query per selezionare tutti i verbali dalla tabella Verbale
                string query = "SELECT * FROM Verbale";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Verbale verbale = new Verbale();
                    verbale.IDverbale = Convert.ToInt32(reader["IDverbale"]);
                    verbale.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    verbale.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                    verbale.NominativoAgente = reader["NominativoAgente"].ToString();
                    verbale.DataTrascrizioneVerbale = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                    verbale.Importo = Convert.ToDecimal(reader["Importo"]);
                    verbale.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    verbale.IDanagrafica = Convert.ToInt32(reader["IDanagrafica"]);
                    verbale.IDviolazione = Convert.ToInt32(reader["IDviolazione"]);
                    listVerbale.Add(verbale);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }


            return View(listVerbale);
        }

        // GET: Create - Aggiungi verbale

        public ActionResult CreateVer()
        {   
            return View();
        }

        // POST: Create - Aggiungi verbale

        [HttpPost]
        public ActionResult CreateVer(Verbale verbale)
        {   
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                //query per inserire un nuovo verbale nella tabella Verbale
                string query = "INSERT INTO Verbale (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDanagrafica, IDviolazione) VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IDanagrafica, @IDviolazione)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                cmd.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                cmd.Parameters.AddWithValue("@IDanagrafica", verbale.IDanagrafica);
                cmd.Parameters.AddWithValue("@IDviolazione", verbale.IDviolazione);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return RedirectToAction("Index");
        }

    }
}