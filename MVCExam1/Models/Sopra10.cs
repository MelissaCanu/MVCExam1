using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCExam1.Models
{
    public class Sopra10
    {
        public int IDanagrafica { get; set; }

        public decimal Importo { get; set; }
        public string Cognome { get; set; }

        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataViolazione { get; set; }

        public int DecurtamentoPunti { get; set; }


    }
}
