using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL.ModuleDent.Models
{
    public class PatientModel
    {
        public uint PatientID { get; set; }

        public string PatientIdentNumber { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientSecondName { get; set; }

        public string PatientLastName { get; set; }

        public DateTime PatientBirthDate { get; set; }

        public string PatientMobile { get; set; }
    }
}
