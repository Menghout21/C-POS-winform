using Frm_login_HW1.Dashboard;
using Frm_login_HW1.POS;
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

namespace Frm_login_HW1.POSNew
{
    public partial class FrmPos2 : Form
    {
        
        public FrmPos2()
        {
            InitializeComponent();
            
        }

        private void FrmPos2_Load(object sender, EventArgs e)
        {
            getcategory();
            getProduct(0);

            if (!dataGridView1.Columns.Contains("btnRemove"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Text = "Remove";
                btn.Name = "btnRemove";
                btn.UseColumnTextForButtonValue = true;
            }
        }
        public void getcategory()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT Id, Name FROM [Category]";
                dt = ClsHelper.Instance.getTable(sql);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Button btn = new Button();
                        btn.Text = dr["Name"].ToString().ToUpper();
                        btn.Tag = dr["Id"].ToString();
                        btn.Height = 60;
                        btn.Width = 50;
                        btn.Dock = DockStyle.Top;
                        btn.Font = new Font("Bahnschrift", 11, FontStyle.Bold);
                        btn.Click += btn_Click;
                        pCategory.Controls.Add(btn);
                    }

                    // Create the "All" button
                    Button btnAll = new Button();
                    btnAll.Text = "All";
                    btnAll.Tag = 0;
                    btnAll.Height = 60;
                    btnAll.Width = 50;
                    btnAll.Dock = DockStyle.Top;
                    btnAll.Font = new Font("Bahnschrift", 11, FontStyle.Bold);
                    btnAll.Click += btnAll_Click;
                    pCategory.Controls.Add(btnAll);

                    // Trigger the btnAll click manually
                    btnAll.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void getProduct(int CategoryId)
        {
            try
            {
                tableLayoutPanel1.Controls.Clear();
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM Product";
                if (CategoryId != 0)
                {
                    sql += " WHERE CategoryId = " + CategoryId;
                }
                dt = ClsHelper.Instance.getTable(sql);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //Button btn = new Button();
                        //btn.Text = dr["Name"].ToString().ToUpper();
                        //btn.Tag = dr["Id"].ToString();
                        //btn.Height = 60;
                        //btn.Width = 50;
                        //btn.Dock = DockStyle.Fill;
                        //btn.Font = new Font("Bahnschrift", 11, FontStyle.Bold);
                        //btn.Click += btn_Click;
                        //tableLayoutPanel1.Controls.Add(btn);
                        Item pItem = new Item();
                        pItem.txtName.Text = dr["Name"].ToString();
                        pItem.txtDescription.Text = dr["Description"].ToString();
                        pItem.txtPrice.Text = "$ " + dr["Price"].ToString();

                        if (dr["Image"] != DBNull.Value && !string.IsNullOrEmpty(dr["Image"].ToString()))
                        {
                            PictureBox pictureBox = new PictureBox();
                            byte[] imageData = (byte[])dr["Image"];
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image img = Image.FromStream(ms);
                                pItem.pictureBox1.Image = img;
                            }
                        }
                        else
                        {
                            pItem.pictureBox1.Image = null;  // Set default image if none exists
                        }

                        pItem.btnAdd.Click += btnAdd_Click;
                        pItem.btnAdd.Tag = dr;
                        pItem.Dock = DockStyle.Fill;
                        tableLayoutPanel1.Controls.Add(pItem);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Categoryid = int.Parse(btn.Tag.ToString());
            getProduct(Categoryid);

        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            getProduct(0);

        }
        private static FrmMain mainFormInstance;
        private void btnBack_Click_1(object sender, EventArgs e)
        {
            if (mainFormInstance == null || mainFormInstance.IsDisposed)
            {
                mainFormInstance = new FrmMain();
            }

            mainFormInstance.Show();
            ClsHelper.Instance.openChildForm(mainFormInstance.pnlBody, new FrmDashboard());
            this.Hide();
        }


        public void btnAdd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataRow dr = (DataRow)btn.Tag;

            double price = double.Parse(dr["Price"].ToString());
            int qty = 1;  // Default quantity is 1

            double amount = price * qty;

            bool isExist = false;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (dr["Id"].ToString() == item.Cells["Id"].Value.ToString())
                {
                    isExist = true;
                    item.Cells["Qty"].Value = int.Parse(item.Cells["Qty"].Value.ToString()) + 1;
                    item.Cells["Amount"].Value = (int.Parse(item.Cells["Qty"].Value.ToString()) * price).ToString();
                    break;
                    
                }
            }

            if (!isExist)
            {
                dataGridView1.Rows.Add(new object[] {
                    "1",  
                    dr["Id"].ToString(),
                    dr["Name"].ToString(),
                    dr["Description"].ToString(),
                    qty,
                    price.ToString(),
                    amount.ToString()
                });
            }

            getTotal();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["No"].Value = (e.RowIndex + 1);
        }

        public void getTotal()
        {
            double total = 0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells["Price"].Value != null)
                {

                    int qty = int.Parse(item.Cells["Qty"].Value.ToString());
                    double price = double.Parse(item.Cells["Price"].Value.ToString());
                    total += (qty * price);
                }

            }
            txtReciveAmount.Text =  total.ToString("F2");
            txtTotalAmount.Text = "$ " + total.ToString("F2");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            txtTotalAmount.Clear();
            txtReciveAmount.Clear();
            txtChange.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = this.dataGridView1.Columns[e.ColumnIndex].Name;
            if (columnName == "btnRemove")
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                txtReciveAmount.Clear();
                txtChange.Clear();
                getTotal();
            }
        }

        private void txtReciveAmount_TextChanged(object sender, EventArgs e)
        {
            double total = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.IsNewRow) continue;
                int qty = int.Parse(dr.Cells["Qty"].Value.ToString());
                double price = double.Parse(dr.Cells["Price"].Value.ToString());
                total += (qty * price);
            }

            double receiveAmount;
            if (double.TryParse(txtReciveAmount.Text, System.Globalization.NumberStyles.Any, null, out receiveAmount))
            {
                double change = receiveAmount - total;
                txtChange.Text = change.ToString("F2");
            }
            else
            {
                txtChange.Clear();
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                clsInvoice invoice = new clsInvoice();
                clsInvoiceDetail invoiceDetails = new clsInvoiceDetail();

                double totalAmount = 0;
                int totalQty = 0;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    int qty = int.Parse(dr.Cells["Qty"].Value.ToString());
                    double price = double.Parse(dr.Cells["Price"].Value.ToString());
                    totalAmount += (qty * price);
                    totalQty += qty;
                }

                invoice.UserId = 1;
                invoice.CustomerId = 1;
                invoice.TotalAmount = totalAmount;
                invoice.TotalQty = totalQty;
                invoice.TotalPaid = double.Parse(txtReciveAmount.Text);
                double findDueAmount = (double.Parse(txtReciveAmount.Text) - totalAmount);
                invoice.DueAmount = findDueAmount;
                invoice.InvoiceStatus = "Completed";
                invoice.IsReturn = false;
                int invoice_id = invoice.order();  // Call the order function

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    invoiceDetails.InvoiceId = invoice_id;
                    invoiceDetails.ProductId = int.Parse(dr.Cells["Id"].Value.ToString());
                    invoiceDetails.Qty = int.Parse(dr.Cells["Qty"].Value.ToString());
                    invoiceDetails.Price = double.Parse(dr.Cells["Price"].Value.ToString());
                    invoiceDetails.Discount = 0;
                    invoiceDetails.orderDetails();
                }

                MessageBox.Show("Order Successfully");
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checkout Error: " + ex.Message);
            }

            txtReciveAmount.Clear();
            txtChange.Clear();
            txtTotalAmount.Text = "0.00";
        }
    }
}
