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
using DanailovDent.BOL;

namespace DanailovDent
{
    public partial class XF_Main : DevExpress.XtraEditors.XtraForm
    {
        public PatientCatalogManager Manager { get; set; }

        public XF_Main()
        {
            InitializeComponent();

            this.Load += XF_Main_Load;
        }

        void XF_Main_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            this.Manager = PatientCatalogManager.Create();
            RefreshCatalog();
            BindData();
        }

        private void BindData()
        {
            var fil = this.Manager.Filter;
            BindingSource bsFil = new BindingSource();
            bsFil.DataSource = fil;
            PatientID.DataBindings.Add("EditValue", bsFil, nameof(fil.PatientID), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientFirstName.DataBindings.Add("EditValue", bsFil, nameof(fil.PatientFirstName), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientSecondName.DataBindings.Add("EditValue", bsFil, nameof(fil.PatientSecondName), true, DataSourceUpdateMode.OnPropertyChanged);
            PatientLastName.DataBindings.Add("EditValue", bsFil, nameof(fil.PatientLastName), true, DataSourceUpdateMode.OnPropertyChanged);

            BindingSource bsData = new BindingSource();
            bsData.DataSource = this.Manager.Catalog;
            gridControlPatient.DataSource = bsData;
        }

        private void RefreshCatalog()
        {
            int topRowIndex = gridViewPatient.TopRowIndex;
            int focusedRowHandle = gridViewPatient.FocusedRowHandle;

            this.Manager.RefreshCatalog();
            gridViewPatient.RefreshData();

            gridViewPatient.TopRowIndex = topRowIndex;
            gridViewPatient.FocusedRowHandle = focusedRowHandle;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnClear.Enabled = false;

            RefreshCatalog();

            btnSearch.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Manager.ClearFilter();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewPatient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var manager = new PatientManager();
            manager.NewPatient();

            using (XF_PatientNewEdit form = new XF_PatientNewEdit(manager))
            {
                form.ShowDialog();
            }
        }
    }
}