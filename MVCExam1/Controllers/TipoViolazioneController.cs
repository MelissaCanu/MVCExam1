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
    public class TipoViolazioneController : Controller
    {   
        //in questo controller mi connetto al db, mostro l'elenco delle violazioni e permetto di aggiungerne una nuova

        // GET: TipoViolazione
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<TipoViolazione> listViolazione = new List<TipoViolazione>();

            try 
            { 
                //query per selezionare tutte le violazioni dalla tabella TipoViolazione
                string query = "SELECT * FROM TipoViolazione WHERE Contestabile = 1";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipoViolazione violazione = new TipoViolazione();
                    violazione.IDviolazione = Convert.ToInt32(reader["IDviolazione"]);
                    violazione.Descrizione = reader["Descrizione"].ToString();
                    violazione.Contestabile = Convert.ToBoolean(reader["Contestabile"]);
                    listViolazione.Add(violazione);
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

            return View(listViolazione);
        }

        // GET: Create - Aggiungi violazione

        public ActionResult CreateVio()
        {   
            return View();
        }

        // POST: Create - Aggiungi violazione

        [HttpPost]
        public ActionResult CreateVio(TipoViolazione violazione) 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                //query per inserire una nuova violazione nella tabella TipoViolazione
                string query = "INSERT INTO TipoViolazione (Descrizione, Contestabile) VALUES (@Descrizione, @Contestabile)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
                cmd.Parameters.AddWithValue("@Contestabile", violazione.Contestabile);
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