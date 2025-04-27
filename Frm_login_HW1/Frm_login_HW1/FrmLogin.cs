using NIT_G2;
using System;
using System.Data;
using System.Data.SqlClient;
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
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string usernameStatic = "super_admin";
            string passwordStatic = "@123$$$";

            try
            {
                // ✅ Check static super_admin login
                if (username == usernameStatic && password == passwordStatic)
                {
                    new FrmMain().Show();
                    this.Hide();
                    return;
                }

                // ✅ Check in User table
                ClsHelper.Open();

                string sql = "SELECT COUNT(1) FROM [User] WHERE UserName = @username AND Password = @password";
                using (SqlCommand cmd = new SqlCommand(sql, ClsHelper.con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int userExists = (int)cmd.ExecuteScalar();

                    if (userExists == 1)
                    {
                        new FrmMain().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClsHelper.Close();
            }
        }


        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
