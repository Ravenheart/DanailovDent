using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanailovDent.BOL.ModuleDent.Models
{
    public class PatientNoteModel
    {
        public uint PatientNoteID { get; set; }
        public uint PatientID { get; set; }
        public DateTime NoteDateTime { get; set; }
        public string Note { get; set; }
    }
}
