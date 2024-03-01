using MVCExam1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MVCExam1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //action per mostrare il totale dei verbali per trasgressore
        //creo model TrasgressoreVerbali
        public ActionResult TotVerbaliPerTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<TrasgressoreVerbali> listTrasgressoreVerbali = new List<TrasgressoreVerbali>();

            try
            {
                //query divisa su più righe per leggibilità 
                //seleziono IDanagrafica, cognome, nome, conteggio dei verbali per trasgressore
                //da tabella Anagrafica e Verbale e li raggruppo per IDanagrafica, cognome e nome
                string query = "SELECT A.IDanagrafica, A.Cognome, A.Nome, COUNT(V.IDanagrafica) AS TotVerbali " +
                              "FROM Anagrafica A JOIN Verbale V ON A.IDanagrafica = V.IDanagrafica " +
                              "GROUP BY A.IDanagrafica, A.Cognome, A.Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TrasgressoreVerbali trasgressoreVerbali = new TrasgressoreVerbali();
                    trasgressoreVerbali.IDanagrafica = Convert.ToInt32(reader["IDanagrafica"]);
                    trasgressoreVerbali.Cognome = reader["Cognome"].ToString();
                    trasgressoreVerbali.Nome = reader["Nome"].ToString();
                    trasgressoreVerbali.TotVerbali = Convert.ToInt32(reader["TotVerbali"]);
                    listTrasgressoreVerbali.Add(trasgressoreVerbali);
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
            
            return View(listTrasgressoreVerbali);
        }

        //action per mostrare il totale dei punti decurtati per trasgressore
        //creo model TrasgressorePunti

        public ActionResult TrasgressorePunti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<TrasgressorePunti> listTrasgressorePunti = new List<TrasgressorePunti>();

            try
            {
                //seleziono IDanagrafica, cognome, nome, somma dei punti decurtati per trasgressore
                //da tabella Anagrafica e Verbale e li raggruppo per IDanagrafica, cognome e nome
                string query = "SELECT A.IDanagrafica, A.Cognome, A.Nome, SUM(V.DecurtamentoPunti) AS TotPunti " +
                              "FROM Anagrafica A JOIN Verbale V ON A.IDanagrafica = V.IDanagrafica " +
                              "GROUP BY A.IDanagrafica, A.Cognome, A.Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TrasgressorePunti trasgressorePunti = new TrasgressorePunti();
                    trasgressorePunti.IDanagrafica = Convert.ToInt32(reader["IDanagrafica"]);
                    trasgressorePunti.Cognome = reader["Cognome"].ToString();
                    trasgressorePunti.Nome = reader["Nome"].ToString();
                    trasgressorePunti.TotPunti = Convert.ToInt32(reader["TotPunti"]);
                    listTrasgressorePunti.Add(trasgressorePunti);
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

            return View(listTrasgressorePunti);
        }


        //action per mostrare il le violazioni che superano i 10 punti
        //creo model Sopra10

        public ActionResult Sopra10()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<Sopra10> listSopra10 = new List<Sopra10>();

            try
            {
                //seleziono IDanagrafica, importo, cognome, nome, data di violazione e decurtamento punti
                //delle violazioni che superano i 10 punti
                string query = "SELECT A.IDanagrafica, A.Cognome, A.Nome, V.Importo, V.DataViolazione, V.DecurtamentoPunti " +
                "FROM Anagrafica A JOIN Verbale V ON A.IDanagrafica = V.IDanagrafica " +
                "WHERE V.DecurtamentoPunti > 10";

                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                //se non ci sono risultati mostro un messaggio di errore 
                if (!reader.HasRows)
                {
                    ViewBag.Message = "Nessun risultato trovato.";
                    return View();
                }

                while (reader.Read())
                {
                    Sopra10 sopra10 = new Sopra10();
                    sopra10.IDanagrafica = Convert.ToInt32(reader["IDanagrafica"]);
                    sopra10.Importo = Convert.ToDecimal(reader["Importo"]);
                    sopra10.Cognome = reader["Cognome"].ToString();
                    sopra10.Nome = reader["Nome"].ToString();
                    sopra10.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    sopra10.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    listSopra10.Add(sopra10);
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

            return View(listSopra10);
        }

        //action per mostrare le violazioni con importo maggiore di 400
        //creo model ImportoSopra400

        public ActionResult ImportoSopra400()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            List<ImportoSopra400> listImportoSopra400 = new List<ImportoSopra400>();

            try
            {
                //seleziono IDanagrafica, importo, cognome, nome
                //delle violazioni con importo maggiore di 400
                string query = "SELECT A.IDanagrafica, A.Cognome, A.Nome, V.Importo " +
                "FROM Anagrafica A JOIN Verbale V ON A.IDanagrafica = V.IDanagrafica " +
                "WHERE V.Importo > 400";

                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                //se non ci sono risultati mostro un messaggio di errore 
                if (!reader.HasRows)
                {
                    ViewBag.Message = "Nessun risultato trovato.";
                    return View();
                }

                while (reader.Read())
                {
                    ImportoSopra400 importoSopra400 = new ImportoSopra400();
                    importoSopra400.IDanagrafica = Convert.ToInt32(reader["IDanagrafica"]);
                    importoSopra400.Importo = Convert.ToDecimal(reader["Importo"]);
                    importoSopra400.Cognome = reader["Cognome"].ToString();
                    importoSopra400.Nome = reader["Nome"].ToString();
                    listImportoSopra400.Add(importoSopra400);
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

            return View(listImportoSopra400);
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}