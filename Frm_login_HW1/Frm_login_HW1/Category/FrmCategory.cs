using NIT_G2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1.Category
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddCategory form= new FrmAddCategory(this);
            form.btnDelete.Visible = false;
            ClsHelper.Instance.setBlurBackground(form);        
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            this.getdata();
        }
        public void getdata()
        {
            string sql = "SELECT *FROM Category";
            dataGridView1.DataSource = ClsHelper.Instance.getTable(sql);
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmAddCategory form = new FrmAddCategory(this);
            form.txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            form.txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            form.txtDescription.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
            form.cboStatus.Text = (bool.Parse(dataGridView1.CurrentRow.Cells["Status"].Value.ToString())) ? "Active" : "InActive";
            form.btnSave.Text = "Update";
            ClsHelper.Instance.setBlurBackground(form);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["No"].Value = (e.RowIndex + 1);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.Columns["Id"].Visible = false;
        }
    }
}
