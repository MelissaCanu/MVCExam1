using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCExam1.Models
{
    public class TrasgressorePunti
    {
        public int IDanagrafica { get; set; }

        public string Cognome { get; set; }

        public string Nome { get; set; }

        public int TotPunti { get; set; }
    }
}
