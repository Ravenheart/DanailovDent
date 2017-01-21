using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DanailovDent.BOL.ModuleDent;

namespace DanailovDent
{
    public partial class XF_PatientNewEdit : DevExpress.XtraEditors.XtraForm
    {
        public PatientManager Manager { get; private set; }

        public XF_PatientNewEdit(PatientManager manager)
        {
            InitializeComponent();

            this.Manager = manager;
            this.Load += XF_PatientNewEdit_Load;
        }

        private void XF_PatientNewEdit_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            BindData();
        }

        private void BindData()
        {
            var mod = this.Manager.ActiveModel;
            BindingSource bsMod = new BindingSource();
            bsMod.DataSource = mod;

            PatientIdentNumber.DataBindings.Add("EditValue", bsMod, nameof(mod.PatientIdentNumber), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientFirstName.DataBindings.Add("EditValue", bsMod, nameof(mod.PatientFirstName), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientSecondName.DataBindings.Add("EditValue", bsMod, nameof(mod.PatientSecondName), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientLastName.DataBindings.Add("EditValue", bsMod, nameof(mod.PatientLastName), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientBirthDate.DataBindings.Add("EditValue", bsMod, nameof(mod.PatientBirthDate), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientMobile.DataBindings.Add("EditValue", bsMod, nameof(mod.PatientMobile), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnNewDocument_Click(object sender, EventArgs e)
        {

        }

        private void btnNewNote_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var check = this.Manager.Save();
            if (check.IsFailed)
            {
                Mess.Error(check);
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}