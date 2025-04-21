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
            ClsHelper.setBlurBackground(form);        
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            this.getdata();
        }
        public void getdata()
        {
            string sql = "SELECT *FROM Category";
            dataGridView1.DataSource = ClsHelper.getTable(sql);
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmAddCategory form = new FrmAddCategory(this);
            form.txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            form.txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            form.txtDescription.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            form.cboStatus.Text = (bool.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString())) ? "Active" : "InActive";
            form.btnSave.Text = "Update";
            ClsHelper.setBlurBackground(form);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
