using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string usernamedb = "hout";
            string passworddb = "2468";

            if(usernamedb == username &&  passworddb == password)
            {
                new FrmMain().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect Username Or password!!");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
