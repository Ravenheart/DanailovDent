using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL.ModuleDent.Models
{
    public class PatientCatalogFilterModel:IModel
    {
        public uint? PatientID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientSecondName { get; set; }
        public string PatientLastName { get; set; }

        public bool IsChanged { get; set; }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
