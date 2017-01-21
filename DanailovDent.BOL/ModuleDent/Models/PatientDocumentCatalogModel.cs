using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanailovDent.BOL.ModuleDent.Models
{
    public class PatientDocumentCatalogModel
    {
        public uint DocID { get; set; }
        public uint PatientID { get; set; }
        public string DocTitle { get; set; }
        public DateTime DocDateTime { get; set; }
    }
}
