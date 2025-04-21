using Frm_login_HW1.Category;
using Frm_login_HW1.Customer;
using Frm_login_HW1.Dashboard;
using Frm_login_HW1.Employee;
using Frm_login_HW1.POS;
using Frm_login_HW1.Payment;
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
using Frm_login_HW1.Order_Status;
using Frm_login_HW1.Product;

namespace Frm_login_HW1
{
    
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmDashboard());
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmProduct());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmCategory());
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmPos());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmCustomer());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmEmployee());
        }

        private void pnlNavbar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new FrmDashboard());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to close?", "Close Appliction", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClsHelper.exitProgram();// Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new frmPaymentMethod());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsHelper.openChildForm(pnlBody, new frmOrderStatus());
        }
    }
}
