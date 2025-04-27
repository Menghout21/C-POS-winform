using NIT_G2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1.Category
{
    public partial class FrmAddCategory : Form
    {
        FrmCategory objFrmload;
        public FrmAddCategory(FrmCategory objFrmload)
        {
            InitializeComponent();
            this.objFrmload = objFrmload;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sur to Delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    ClsHelper.con.Open();
                    string Delete = "DELETE FROM Category WHERE id=@Id";
                    SqlCommand cmd = new SqlCommand(Delete, ClsHelper.con);
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);
                    cmd.ExecuteNonQuery();
                    ClsHelper.con.Close();
                    

                    MessageBox.Show("Delete Success", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    objFrmload.getdata();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtId.Text == "0") //Insert
                {
                    string Name = txtName.Text;
                    string Description = txtDescription.Text;
                    bool Status = (cboStatus.Text == "Active") ? true : false;

                    ClsHelper.con.Open();
                    string insert = $"INSERT INTO Category(name,description,status) VALUES(@Name,@Description,@Status)";
                    SqlCommand cmd = new SqlCommand(insert, ClsHelper.con);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.ExecuteNonQuery();
                    ClsHelper.con.Close();
                    MessageBox.Show("Insert Success!!");
                    objFrmload.getdata();
                    this.Close();
                    

                }
                else //Update
                {
                    //string Name = txtName.Text;
                    //string Description = txtDescription.Text;
                    //bool Status = (cboStatus.Text == "Active") ? true : false;

                    ClsHelper.con.Open();
                    string insert = $"UPDATE Category SET name=@Name,description=@Description,status=@Status WHERE id=@Id";
                    SqlCommand cmd = new SqlCommand(insert, ClsHelper.con);
                    cmd.Parameters.AddWithValue("@Id",txtId.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Status", (cboStatus.Text == "Active") ? true : false);
                    cmd.ExecuteNonQuery();
                    ClsHelper.con.Close();
                    MessageBox.Show("Update Success!!");
                    objFrmload.getdata();
                    this.Close();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void FrmAddCategory_Load(object sender, EventArgs e)
        {

        }
    }
}
