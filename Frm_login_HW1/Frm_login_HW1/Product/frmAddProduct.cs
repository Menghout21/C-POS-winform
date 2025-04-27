using NIT_G2;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Frm_login_HW1.Product
{
    public partial class frmAddProduct : Form
    {
        FrmProduct frmLoad;
        ProductRepository productRepo = new ProductRepository();

        public frmAddProduct(FrmProduct frmLoad)
        {
            InitializeComponent();
            this.frmLoad = frmLoad;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            initCboCategory();
            initCboStatus();

            if (txtId.Text == "0")
            {
                btnsave.Text = "Save";
                btndelete.Visible = false;
            }
            else
            {
                btnsave.Text = "Update";
                btndelete.Visible = true;
            }
        }

        private void initCboCategory()
        {
            string sql = "SELECT Id, Name FROM Category";
            ClsHelper.Instance.BoundComboBox(cboCategory, sql);
        }

        private void initCboStatus()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("Active");
            cboStatus.Items.Add("Inactive");
            cboStatus.SelectedIndex = 0; // Default to "Active"
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
            try
            {
                Product product = new Product()
                {
                    Id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : int.Parse(txtQty.Text),
                    Price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : double.Parse(txtPrice.Text),
                    CategoryID = cboCategory.SelectedValue != null ? int.Parse(cboCategory.SelectedValue.ToString()) : 0,
                    Status = cboStatus.Text == "Active",
                    Image = GetImageBytes()
                };
                if (txtId.Text == "0")
                {
                    // Insert new product
                    productRepo.Insert(product);
                    MessageBox.Show("Insert Success!!");
                    ClearInput();
                }
                else
                {
                    // Update existing product
                    productRepo.Update(product);
                    MessageBox.Show("Update Success!!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing product fields: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            

            frmLoad.getdata(); // refresh main form
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int id = int.Parse(txtId.Text);
                    productRepo.Delete(id);
                    MessageBox.Show("Delete Success!!");
                    frmLoad.getdata();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private byte[] GetImageBytes()
        {
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;
        }

        private void ClearInput()
        {
            txtId.Text = "0";
            txtName.Clear();
            txtDescription.Clear();
            txtQty.Clear();
            txtPrice.Clear();
            cboCategory.SelectedIndex = -1;
            cboStatus.SelectedIndex = 0;
            pictureBox1.Image = null;
            txtName.Focus();
        }
    }
}
