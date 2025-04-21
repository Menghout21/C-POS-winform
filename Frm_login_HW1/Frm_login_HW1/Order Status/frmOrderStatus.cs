using Frm_login_HW1.Payment;
using NIT_G2;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Frm_login_HW1.Order_Status
{
    public partial class frmOrderStatus : Form
    {
        public frmOrderStatus()
        {
            InitializeComponent();

            // Add "No" column for row numbering
            if (!dataGridView1.Columns.Contains("No"))
            {
                DataGridViewTextBoxColumn noCol = new DataGridViewTextBoxColumn();
                noCol.Name = "No";
                noCol.HeaderText = "No";
                noCol.Width = 50;
                dataGridView1.Columns.Insert(0, noCol);
            }

            // Optional: Better appearance
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void frmOrderStatus_Load(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            try
            {
                string SEL = "SELECT * FROM Order_Status";
                dataGridView1.DataSource = ClsHelper.getTable(SEL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClsHelper.setBlurBackground(new frmOrder(this));
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                frmOrder form = new frmOrder(this);
                try
                {
                    form.txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                    form.txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                    form.txtDescription.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
                    form.cboStatus.Text = bool.Parse(dataGridView1.CurrentRow.Cells["Status"].Value.ToString()) ? "Active" : "InActive";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading form data: " + ex.Message);
                }
                ClsHelper.setBlurBackground(form);
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
            // Set the row number in the "No" column
            if (dataGridView1.Columns.Contains("No"))
            {
                dataGridView1.Rows[e.RowIndex].Cells["No"].Value = e.RowIndex + 1;
            }
        }
    }
}
