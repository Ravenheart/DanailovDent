using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL
{
    public class CheckResult
    {
        public CheckResult()
        {
            this.Details = new List<CheckResultDetail>();
        }

        public bool Success
        {
            get
            {
                return !this.Details.Any(s => s.Type == CheckResultDetail.ErrorType.Error);
            }
        }
        public bool IsFailed
        {
            get { return !Success; }
        }

        public string ErrorProperty
        {
            get
            {
                var det = this.Errors.FirstOrDefault();
                if (det == null)
                    return string.Empty;

                return det.PropertyName;
            }
        }

        public Object ObjectInstance { get; set; }

        public List<CheckResultDetail> Details { get; set; }

        public List<CheckResultDetail> Errors
        {
            get
            {
                return this.Details.Where(d => d.Type == CheckResultDetail.ErrorType.Error).ToList();
            }
        }

        public List<CheckResultDetail> Warnings
        {
            get
            {
                return this.Details.Where(d => d.Type == CheckResultDetail.ErrorType.Warning).ToList();
            }
        }

        public String Print(String separator = "\r\n")
        {
            return String.Join(separator, this.Errors.Select(s => s.Message));
        }

        public String PrintWarnings(String separator = "\r\n")
        {
            return String.Join(separator, this.Warnings.Select(s => s.Message));
        }

        /// <summary>
        /// Добавя нова грешка в списъка
        /// </summary>
        /// <param name="message">Съобщение</param>
        /// <param name="property">Property за което се отнася</param>
        public void AddError(string message, string property = "")
        {
            this.Details.Add(new CheckResultDetail(CheckResultDetail.ErrorType.Error, property, message));
        }

        /// <summary>
        /// Добавя предупреждение в списъка
        /// </summary>
        /// <param name="message">Съобщение</param>
        /// <param name="property">Property за което се отнася</param>
        public void AddWarning(string message, string property = "")
        {
            this.Details.Add(new CheckResultDetail(CheckResultDetail.ErrorType.Warning, property, message));
        }

        /// <summary>
        /// Добавя информация само с флаг "None"
        /// </summary>
        /// <param name="message">Съобщение</param>
        /// <param name="property">Property за което се отнася</param>
        public void AddInfo(string message, string property = "")
        {
            this.Details.Add(new CheckResultDetail(CheckResultDetail.ErrorType.None, property, message));
        }

        /// <summary>
        /// Копира детайлите от сорса към дестинацията
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        internal static void CopyDetails(CheckResult source, CheckResult destination)
        {
            destination.Details.AddRange(source.Details);
        }

        /// <summary>
        /// Създава нова инстанция на CheckResult с една грешка
        /// </summary>
        /// <param name="message">Описание на грешката</param>
        /// <param name="property">Property-то на което е грешката</param>
        /// <returns></returns>
        public static CheckResult Error(string message, string property = "")
        {
            CheckResult res = new CheckResult();
            res.Details.Add(new CheckResultDetail(CheckResultDetail.ErrorType.Error, property, message));
            return res;
        }

        /// <summary>
        /// Създава нова инстанция на CheckResult с една грешка
        /// </summary>
        /// <param name="ex">Грешка</param>
        /// <returns></returns>
        public static CheckResult Error(Exception ex)
        {
            CheckResult res = new CheckResult();
            res.Details.Add(new CheckResultDetail(CheckResultDetail.ErrorType.Error, string.Empty, ex.ToString()));
            return res;
        }
    }
}
