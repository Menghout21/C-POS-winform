
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
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Frm_login_HW1.Employee
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();

            if (!dataGridView1.Columns.Contains("No"))
            {
                DataGridViewTextBoxColumn noCol = new DataGridViewTextBoxColumn();
                noCol.Name = "No";
                noCol.HeaderText = "No";
                noCol.Width = 50;
                dataGridView1.Columns.Insert(0, noCol);
            }
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            getData();
            btnDelete.Hide();
            btnUpdate.Hide();
        }

        public void getData()
        {
            try
            {
                string SEL = "SELECT * FROM [User]";
                dataGridView1.DataSource = ClsHelper.getTable(SEL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                clsUser.FirstName = txtFirstName.Text;
                clsUser.LastName = txtLastName.Text;
                clsUser.UserName = txtUserName.Text;
                clsUser.Password = txtPassword.Text;
                clsUser.Role = cboRole.Text;
                clsUser.Insert();
                MessageBox.Show("Insert Success!");
                getData();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtUserName.Clear();
                txtPassword.Clear();
                cboRole.SelectedIndex = -1;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    lbId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                    txtFirstName.Text = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
                    txtLastName.Text = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
                    txtUserName.Text = dataGridView1.CurrentRow.Cells["UserName"].Value.ToString();
                    txtPassword.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
                    cboRole.Text = dataGridView1.CurrentRow.Cells["Role"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading form data: " + ex.Message);
                }

                btnSave.Hide();
                btnDelete.Show();
                btnUpdate.Show();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try 
            {
                clsUser.Id = int.Parse(lbId.Text);
                clsUser.FirstName = txtFirstName.Text;
                clsUser.LastName = txtLastName.Text;
                clsUser.UserName = txtUserName.Text;
                clsUser.Password = txtPassword.Text;
                clsUser.Role = cboRole.Text;
                clsUser.Update();
                MessageBox.Show("Update Success!");
                getData();

                txtFirstName.Clear();
                txtLastName.Clear();
                txtUserName.Clear();
                txtPassword.Clear();
                cboRole.SelectedIndex = -1;

                btnDelete.Hide();
                btnUpdate.Hide();
                btnSave.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sur to Delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    clsUser.Id = int.Parse(lbId.Text);
                    clsUser.Delete();
                    getData();

                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtUserName.Clear();
                    txtPassword.Clear();
                    cboRole.SelectedIndex = -1;

                    btnDelete.Hide();
                    btnUpdate.Hide();
                    btnSave.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.Columns["Id"].Visible = false;
            this.dataGridView1.Columns["No"].Width = 50;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dataGridView1.Columns.Contains("No"))
            {
                dataGridView1.Rows[e.RowIndex].Cells["No"].Value = e.RowIndex + 1;
            }
        }
    }
}
