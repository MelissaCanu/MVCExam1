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
    public class AnagraficaController : Controller
    {
        //in questo controller mi connetto al db, mostro l'elenco dei trasgressori e permetto di aggiungerne uno nuovo

        // GET: Anagrafica - Elenco trasgressori

        public ActionResult Index()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<Anagrafica> listAnagrafica = new List<Anagrafica>();

            try
            {
                //query per selezionare tutti i trasgressori dalla tabella Anagrafica 
                string query = "SELECT * FROM Anagrafica";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Anagrafica anagrafica = new Anagrafica();
                    anagrafica.IDanagrafica = Convert.ToInt32(reader["IDanagrafica"]);
                    anagrafica.Cognome = reader["Cognome"].ToString();
                    anagrafica.Nome = reader["Nome"].ToString();
                    anagrafica.Indirizzo = reader["Indirizzo"].ToString();
                    anagrafica.Citta = reader["Citta"].ToString();
                    anagrafica.CAP = reader["CAP"].ToString();
                    anagrafica.CodFisc = reader["CodFisc"].ToString();
                    listAnagrafica.Add(anagrafica);
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return View(listAnagrafica);

        }

        // GET: Create - Aggiungi trasgressore

        public ActionResult CreateT()
        {
            return View();
        }

        // POST: Create - Aggiungi trasgressore

        [HttpPost]
        public ActionResult CreateT(Anagrafica anagrafica)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {   
                // query per inserire un nuovo trasgressore nella tabella Anagrafica 
                string query = "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Citta, CAP, CodFisc) VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @CodFisc)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                cmd.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                cmd.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                cmd.Parameters.AddWithValue("@CodFisc", anagrafica.CodFisc);
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