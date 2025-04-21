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

namespace Frm_login_HW1.Payment
{
    public partial class frmPaymentMethod: Form
    {
        public frmPaymentMethod()
        {
            InitializeComponent();
        }
        private void frmPaymentMethod_Load(object sender, EventArgs e)
        {
            this.getdata();
        }
        public void getdata()
        {
            string SEL = "SELECT * FROM Payment_Method";
            dataGridView1.DataSource = ClsHelper.getTable(SEL);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPayment form = new frmPayment(this);
            form.txtId.Text = "0";
            ClsHelper.setBlurBackground(form);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            frmPayment form = new frmPayment(this);
            form.txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            form.txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            form.txtDescription.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            form.cboStatus.Text = bool.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString()) ? "Active" : "InActive";
            ClsHelper.setBlurBackground(form);
        }
    }
}
