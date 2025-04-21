using Frm_login_HW1.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1.Order_Status
{
    public partial class frmOrder: Form
    {
        frmOrderStatus objFrmload;
        public frmOrder(frmOrderStatus objFrmload)
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
                    clsOrderStatus.Name = txtName.Text;
                    clsOrderStatus.Description = txtDescription.Text;
                    clsOrderStatus.Status = (cboStatus.Text == "Active");
                    clsOrderStatus.Insert();
                    MessageBox.Show("Insert Success!");
                    objFrmload.getData();
                    this.Close();
                }
                else
                {
                    clsOrderStatus.ID = int.Parse(txtId.Text);
                    clsOrderStatus.ID = int.Parse(txtId.Text);
                    clsOrderStatus.Name = txtName.Text;
                    clsOrderStatus.Description = txtDescription.Text;
                    clsOrderStatus.Status = (cboStatus.Text == "Active");
                    clsOrderStatus.Update();
                    MessageBox.Show("Update Success!");
                    objFrmload.getData();
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            if (txtId.Text == "0")
            {
                btnSave.Text = "Save";
                btnDelete.Visible = false;
            }
            else
            {
                btnSave.Text = "Update";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sur to Delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    clsOrderStatus.ID = int.Parse(txtId.Text);
                    clsOrderStatus.Delete();
                    objFrmload.getData();
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
