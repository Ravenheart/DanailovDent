using DanailovDent.BOL;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanailovDent
{
    public class Mess
    {

        public static DialogResult Info(string message, string caption = "Информация")
        {

            return XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Question(string message, string caption = "Въпрос")
        {
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult Error(string message, string caption = "Грешка")
        {
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Error(CheckResult result, string caption = "Грешка")
        {
            return XtraMessageBox.Show(result.Print(), caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Warning(CheckResult result, string caption = "Предупреждение")
        {
            return XtraMessageBox.Show(result.PrintWarnings(), caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Warning(string message, string caption = "Предупреждение")
        {
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowException(Exception exception)
        {
            return XtraMessageBox.Show("Грешка: " + exception.ToString(), "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
