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
                //seleziono cognome, nome, conteggio dei verbali per trasgressore
                //da tabella Anagrafica e Verbale e li raggruppo per cognome e nome
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