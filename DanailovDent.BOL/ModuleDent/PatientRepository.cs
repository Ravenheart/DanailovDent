using DanailovDent.BOL.ModuleDent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanailovDent.BOL.ModuleDent
{
    public class PatientRepository
    {
        public DentDb Db;

        public PatientRepository(DentDb db)
        {
            this.Db = db;
        }

        public PatientModel GetPatient(uint patientID)
        {
            var model = Db.patients
                .Where(p => p.PatientID == patientID)
                .Select(p => new PatientModel()
                {
                    PatientID = p.PatientID,
                    PatientIdentNumber = p.PatientIdentNumber,
                    PatientFirstName = p.PatientFirstName,
                    PatientSecondName = p.PatientSecondName,
                    PatientLastName = p.PatientLastName,
                    PatientBirthDate = p.PatientBirthDate,
                    PatientMobile = p.PatientMobile,
                })
                .FirstOrDefault();

            return model;
        }
        public void SavePatient(PatientModel model)
        {
            var poco = Db.patients
                    .Where(p => p.PatientID == model.PatientID)
                    .FirstOrDefault();
            if (poco == null)
                poco = new Pocos.patient();

            poco.PatientIdentNumber = model.PatientIdentNumber;
            poco.PatientFirstName = model.PatientFirstName;
            poco.PatientSecondName = model.PatientSecondName;
            poco.PatientLastName = model.PatientLastName;
            poco.PatientBirthDate = model.PatientBirthDate;
            poco.PatientMobile = model.PatientMobile;

            Db.Save(poco);
            Db.AddRollbackKey(model, nameof(model.PatientID));
            model.PatientID = poco.PatientID;
        }
        public void DeletePatient(PatientModel model)
        {
            if (model.PatientID == 0)
                return;

            var poco = Db.patients
                .Where(p => p.PatientID == model.PatientID)
                .FirstOrDefault();
            if (poco != null)
                Db.Delete(poco);
        }

        public List<PatientNoteModel> GetNotes(uint patientID)
        {
            return this.Db.patient_notes
                .Where(p => p.PatientID == patientID)
                .Select(p => new PatientNoteModel()
                {
                    PatientNoteID = p.PatientNoteID,
                    PatientID = p.PatientID,
                    NoteDateTime = p.NoteDateTime,
                    Note = p.Note
                })
                .ToList();
        }
        public void SaveNote(PatientNoteModel model)
        {
            var poco = this.Db.patient_notes
                .Where(p => p.PatientNoteID == model.PatientNoteID)
                .FirstOrDefault();
            if (poco == null)
                poco = new Pocos.patient_notes();

            poco.PatientID = model.PatientID;
            poco.NoteDateTime = model.NoteDateTime;
            poco.Note = model.Note;

            this.Db.Save(poco);

            this.Db.AddRollbackKey(model, nameof(model.PatientNoteID));
            model.PatientNoteID = poco.PatientNoteID;
        }
        public void DeleteNote(PatientNoteModel model)
        {
            if (model.PatientNoteID == 0)
                return;

            var poco = this.Db.patient_notes
                .Where(p => p.PatientNoteID == model.PatientNoteID)
                .First();

            this.Db.Delete(poco);
        }
    }
}
