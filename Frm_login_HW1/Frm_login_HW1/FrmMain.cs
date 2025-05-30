﻿using Frm_login_HW1.Category;
using Frm_login_HW1.Customer;
using Frm_login_HW1.Dashboard;
using Frm_login_HW1.Employee;
using Frm_login_HW1.POS;
using Frm_login_HW1.POSNew;
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
using Frm_login_HW1.Product;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Frm_login_HW1.OrderHistory;

namespace Frm_login_HW1
{
    public partial class FrmMain : Form
    {
        
        public FrmMain()
        {
            InitializeComponent();
            //userRole = role;
        }

        // Load event (first time form loads)
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Open the default dashboard form
            ClsHelper.Instance.openChildForm(pnlBody, new FrmDashboard());
            //SetRoleBasedVisibility();
        }


        // Button click to navigate to employee form
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmEmployee());
        }

        // Button click to navigate to product form
        private void btnProduct_Click(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmProduct());
        }

        // Button click to navigate to category form
        private void btnCategory_Click(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmCategory());
        }

        // Button click to navigate to POS form
        private void btnPos_Click(object sender, EventArgs e)
        {
            new FrmPos2().Show();
            this.Hide();
        }

        // Button click to navigate to customer form
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmCustomer());
        }

        // Button click to navigate to dashboard
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmDashboard());
        }

        // Button click to log out
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to LogOut?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                new FrmLogin().Show();
                this.Hide();
            }
        }

        // Button click to exit the application
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to close?", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClsHelper.exitProgram();  // Exit the program
            }
        }

        // Button click to navigate to product form (duplicate event handler, could be removed)
        private void btnProduct_Click_1(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmProduct());
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnOrderHistory_Click(object sender, EventArgs e)
        {
            ClsHelper.Instance.openChildForm(pnlBody, new FrmOrderHistory());
        }
    }
}
