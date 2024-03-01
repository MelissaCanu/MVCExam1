using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCExam1.Models
{
    public class TipoViolazione
    {
        public int IDviolazione { get; set; }
        public string Descrizione { get; set; }
        public bool Contestabile { get; set; }
    }
}