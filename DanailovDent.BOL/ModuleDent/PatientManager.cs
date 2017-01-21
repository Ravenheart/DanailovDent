using DanailovDent.BOL.ModuleDent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL.ModuleDent
{
    public class PatientManager
    {
        public void NewPatient()
        {
            this.ActiveModel = new PatientModel();
        }
        public void LoadPatient(uint patientID)
        {
            using (var db = DB.Create())
            {
                PatientRepository rep = new PatientRepository(db);
                this.ActiveModel = rep.GetPatient(patientID);
            }
        }

        public PatientManager()
        {
            this.Documents = new List<PatientDocumentCatalogModel>();
            this.Notes = new List<PatientNoteModel>();
        }

        public PatientModel ActiveModel { get; set; }
        public List<PatientDocumentCatalogModel> Documents { get; set; }
        public List<PatientNoteModel> Notes { get; set; }

        public void RefreshDocuments()
        {
        }
        public void RefreshNotes()
        {
            using (var db = DB.Create())
            {
                PatientRepository rep = new PatientRepository(db);

                this.Notes.Clear();
                this.Notes.AddRange(rep.GetNotes(this.ActiveModel.PatientID));
            }
        }

        public CheckResult Save()
        {
            using (var db = DB.Create())
            {
                db.BeginTransaction();

                var validator = new PatientValidator(db);
                var check = validator.ValidateSave(this.ActiveModel);
                if (check.IsFailed)
                {
                    db.RollbackTransaction();
                    return check;
                }

                var rep = new PatientRepository(db);
                rep.SavePatient(this.ActiveModel);

                db.CommitTransaction();

                return check;
            }
        }
        public CheckResult Delete()
        {
            using (var db = DB.Create())
            {
                db.BeginTransaction();

                var validator = new PatientValidator(db);
                var check = validator.ValidateDelete(this.ActiveModel);
                if (check.IsFailed)
                {
                    db.RollbackTransaction();
                    return check;
                }

                var rep = new PatientRepository(db);
                rep.DeletePatient(this.ActiveModel);

                db.CommitTransaction();

                return check;
            }
        }
    }
}
