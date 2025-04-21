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
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
            // Add "No" column for row numbering
            if (!dataGridView1.Columns.Contains("No"))
            {
                DataGridViewTextBoxColumn noCol = new DataGridViewTextBoxColumn();
                noCol.Name = "No";
                noCol.HeaderText = "No";
                noCol.Width = 20;
                dataGridView1.Columns.Insert(0, noCol);
            }

            // Optional: Better appearance
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            this.getdata();
        }
        public void getdata()
        {
            string sql = "SELECT * FROM Product";
            dataGridView1.DataSource = ClsHelper.getTable(sql);
            DataGridViewImageColumn Imagecolumn = new DataGridViewImageColumn();
            Imagecolumn = (DataGridViewImageColumn)dataGridView1.Columns["Image"];
            Imagecolumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            this.dataGridView1.Columns["CategoryId"].Visible = false;
            this.dataGridView1.Columns["No"].Width = 40;
            this.dataGridView1.Columns["Image"].Width = 40;
            this.dataGridView1.Columns["Status"].Width = 40;
            this.dataGridView1.Columns["CreateAt"].Width = 100;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClsHelper.setBlurBackground(new frmAddProduct(this));
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            frmAddProduct form = new frmAddProduct(this);
            try
            {
                form.txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                form.txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                form.txtDescription.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
                form.txtQty.Text = dataGridView1.CurrentRow.Cells["Qty"].Value.ToString();
                form.txtPrice.Text = dataGridView1.CurrentRow.Cells["Price"].Value.ToString();
                form.cboStatus.Text = bool.Parse(dataGridView1.CurrentRow.Cells["Status"].Value.ToString()) ? "Active" : "InActive";
                byte[] imageData = (byte[])dataGridView1.CurrentRow.Cells["Image"].Value;

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image img = Image.FromStream(ms);
                    form.pictureBox1.Image = img;
                }
                ClsHelper.setBlurBackground(form);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView1.Columns.Contains("Id"))
            {
                dataGridView1.Columns["Id"].Visible = false;
            }

            // Set specific column widths if needed
            if (dataGridView1.Columns.Count > 1)
            {
                dataGridView1.Columns[1].Width = 150; // Assuming Name column
            }
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
