using DanailovDent.BOL.ModuleDent.Models;
using DanailovDent.BOL.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL.ModuleDent
{
    public class PatientCatalogManager
    {
        public static PatientCatalogManager Create()
        {
            return new PatientCatalogManager();
        }

        protected PatientCatalogManager()
        {
            this.Filter = new PatientCatalogFilterModel();
            this.Catalog = new List<PatientCatalogModel>();
        }

        public PatientCatalogFilterModel Filter { get; set; }
        public List<PatientCatalogModel> Catalog { get; set; }

        public void RefreshCatalog()
        {
            using (var db = DB.Create())
            {
                this.Catalog.Clear();

                var fil = this.Filter;
                var q = PredicateBuilder.True<patient>();
                if (fil.PatientID.HasValue)
                    q = q.And(p => p.PatientID == fil.PatientID.Value);
                if (!string.IsNullOrWhiteSpace(fil.PatientFirstName))
                    q = q.And(p => p.PatientFirstName.StartsWith(fil.PatientFirstName));
                if (!string.IsNullOrWhiteSpace(fil.PatientSecondName))
                    q = q.And(p => p.PatientSecondName.StartsWith(fil.PatientSecondName));
                if (!string.IsNullOrWhiteSpace(fil.PatientLastName))
                    q = q.And(p => p.PatientLastName.StartsWith(fil.PatientLastName));

                var items = db.patients
                    .Where(q)
                    .Select(p => new PatientCatalogModel()
                    {
                        PatientID = p.PatientID,
                        PatientIdentNumber = p.PatientIdentNumber,
                        PatientFirstName = p.PatientFirstName,
                        PatientSecondName = p.PatientSecondName,
                        PatientLastName = p.PatientLastName,
                        PatientMobile = p.PatientMobile,
                    })
                    .ToList();
                this.Catalog.AddRange(items);
            }
        }
        public void ClearFilter()
        {
            this.Filter.PatientID = null;
            this.Filter.PatientFirstName = string.Empty;
            this.Filter.PatientSecondName = string.Empty;
            this.Filter.PatientLastName = string.Empty;
        }
    }
}
