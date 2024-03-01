using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace MVCExam1.Models
{
    public class Verbale
    {
        public int IDverbale { get; set; }

        //aggiungo displayformat per formattare la data
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataViolazione { get; set; }

        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }

        public int DecurtamentoPunti { get; set; }

        public int IDanagrafica { get; set; }

        public int IDviolazione { get; set; }
    }
}