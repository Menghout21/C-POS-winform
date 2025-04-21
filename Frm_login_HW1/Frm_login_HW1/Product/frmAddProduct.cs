using Frm_login_HW1.Order_Status;
using NIT_G2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1.Product
{
    public partial class frmAddProduct : Form
    {
        FrmProduct frmLoad;

        public frmAddProduct(FrmProduct frmLoad)
        {
            InitializeComponent();
            this.frmLoad = frmLoad;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            initCboCategory();
            if (txtId.Text == "0")
            {
                btnsave.Text = "Save";
                btndelete.Visible = false;
            }
            else
            {
                btnsave.Text = "Update";
            }
        }

        public void initCboCategory()
        {
            string sql = "SELECT Id, Name FROM Category";
            ClsHelper.BoundComboBox(cboCategory, sql);
        }

        private void btnBrowsImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void btnDelImage_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            clsProduct clsProduct = new clsProduct();
            clsProduct.Id = int.Parse(txtId.Text);
            clsProduct.Name = txtName.Text;
            clsProduct.Description = txtDescription.Text;
            clsProduct.Qty = int.Parse(txtQty.Text);
            clsProduct.Price = double.Parse(txtPrice.Text);
            clsProduct.CategoryID = int.Parse(cboCategory.SelectedValue.ToString()); // You can change this later to a selected value
            clsProduct.Status = cboCategory.Text == "Active" ? true : false;

            // Convert the image from the PictureBox into a byte array
            if (pictureBox1.Image != null)
            {
                Image img = new Bitmap(pictureBox1.Image);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageByteArray = ms.ToArray();
                clsProduct.Image = imageByteArray;
            }
            else
            {
                clsProduct.Image = null;
            }

            if (txtId.Text == "0")
            {
                clsProduct.Insert();
                MessageBox.Show("Insert Success!!");

                frmLoad.getdata();   // Refresh main form
                ClearInput();        // Clear fields but keep form open
            }
            else
            {
                clsProduct.Update();
                MessageBox.Show("Update Success!!");

                frmLoad.getdata();   // Refresh main form
                this.Close();        // Close form after update
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    clsProduct clsProduct = new clsProduct();
                    clsProduct.Id = int.Parse(txtId.Text);
                    clsProduct.Delete();

                    frmLoad.getdata();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // 🔄 Clear all form fields
        private void ClearInput()
        {
            txtId.Text = "0";
            txtName.Clear();
            txtDescription.Clear();
            txtQty.Clear();
            txtPrice.Clear();
            cboCategory.SelectedIndex = -1;
            pictureBox1.Image = null;
            txtName.Focus();
        }
    }
}
