using DanailovDent.BOL.ModuleDent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL.ModuleDent
{
    public class PatientValidator
    {
        private DentDb Db;

        public PatientValidator(DentDb db)
        {
            this.Db = db;
        }

        public CheckResult ValidateSave(PatientModel model)
        {
            CheckResult res = new CheckResult();

            if (string.IsNullOrWhiteSpace(model.PatientFirstName))
                res.AddError("Моля, въведете име на пациента!", nameof(model.PatientFirstName));
            if (string.IsNullOrWhiteSpace(model.PatientLastName))
                res.AddError("Моля, въведете фамилия на пациента!", nameof(model.PatientLastName));
            if (model.PatientBirthDate == DateTime.MinValue)
                res.AddError("Моля, въведете рожденна дата на пациента!", nameof(model.PatientBirthDate));
            if (string.IsNullOrWhiteSpace(model.PatientMobile))
                res.AddError("Моля, въведете мобилен телефон на пациента!", nameof(model.PatientMobile));

            return res;
        }

        public CheckResult ValidateDelete(PatientModel model)
        {
            CheckResult res = new CheckResult();

            return res;
        }
    }
}
