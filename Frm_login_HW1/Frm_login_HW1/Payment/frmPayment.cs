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
    public partial class frmPayment: Form
    {
        frmPaymentMethod objFrmload;
        public frmPayment(frmPaymentMethod objFrmload)
        {
            InitializeComponent();
            this.objFrmload = objFrmload;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtId.Text == "0")
                {
                    clsPaymentMethod.Name = txtName.Text;
                    clsPaymentMethod.Description = txtDescription.Text;
                    clsPaymentMethod.Status = (cboStatus.Text == "Active");
                    clsPaymentMethod.Insert();
                    MessageBox.Show("Insert Success!");
                    objFrmload.getdata();
                    this.Close();
                }
                else
                {
                    clsPaymentMethod.ID = int.Parse(txtId.Text);
                    clsPaymentMethod.Name = txtName.Text;
                    clsPaymentMethod.Description = txtDescription.Text;
                    clsPaymentMethod.Status = (cboStatus.Text == "Active");
                    clsPaymentMethod.Update();
                    MessageBox.Show("Update Success!");
                    objFrmload.getdata();
                    this.Close();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            if(txtId.Text == "0")
            {
                btnSave.Text = "Save";
                btnDelete.Visible = false;
            }
            else
            {
                btnSave.Text = "Update";
            }

        }

        private void txtid2_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sur to Delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    clsPaymentMethod.ID = int.Parse(txtId.Text);
                    clsPaymentMethod.Delete();
                    objFrmload.getdata();
                    this.Close();
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
    }
}
