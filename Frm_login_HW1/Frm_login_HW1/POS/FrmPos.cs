using Frm_login_HW1.Dashboard;
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

namespace Frm_login_HW1.POS
{
    public partial class FrmPos : Form
    {
        public FrmPos()
        {
            InitializeComponent();
        }

        private void FrmPos_Load(object sender, EventArgs e)
        {
            getListCategory();
            getListProduct(0);


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

        // Method to get category list and display as buttons
        // Method to get category list and display as buttons
        public void getListCategory()
        {
            try
            {
                // Clear previous buttons
                pCategory.Controls.Clear();

                DataTable dt = new DataTable();
                string sql = "SELECT Id, Name FROM [Category]";
                dt = ClsHelper.Instance.getTable(sql);

                if (dt != null)
                {
                    Button btnAll = new Button();
                    btnAll.Text = "All";
                    btnAll.Tag = 0;
                    btnAll.Height = 50;
                    btnAll.Width = 100;
                    btnAll.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    btnAll.Margin = new Padding(5);
                    btnAll.Click += btnAll_Click;

                    pCategory.Controls.Add(btnAll);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Button btn = new Button();
                        btn.Text = dr["Name"].ToString().ToUpper();
                        btn.Tag = dr["Id"].ToString();
                        btn.Height = 50;
                        btn.Width = 100;
                        btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                        btn.Margin = new Padding(5);
                        btn.Click += btn_Click;
                        pCategory.Controls.Add(btn);
                    }
                }

                pCategory.Refresh();  // Ensure the panel is refreshed after loading categories
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }


        // Method to get product list based on selected category
        // Method to get product list based on selected category
        public void getListProduct(int CategoryId)
        {
            try
            {
                tableLayoutPanel1.Controls.Clear();  // Clear previous products

                DataTable dt = new DataTable();
                string sql = "SELECT * FROM Product";
                if (CategoryId != 0)
                {
                    sql += " WHERE CategoryId = " + CategoryId;
                }

                Console.WriteLine("Executing SQL: " + sql);  // Debugging SQL query

                dt = ClsHelper.Instance.getTable(sql);

                if (dt != null && dt.Rows.Count == 0)
                {
                    MessageBox.Show("No products found for the selected category.");
                }

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductItemControl pItem = new ProductItemControl();
                        pItem.lbName.Text = dr["Name"].ToString();
                        pItem.lbDescription.Text = dr["Description"].ToString();
                        pItem.lbPrice.Text = "$ " + dr["Price"].ToString();

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

                // After loading products -> update button style
                foreach (Control ctrl in pCategory.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        if (btn.Tag != null && btn.Tag.ToString() == CategoryId.ToString())
                        {
                            // Highlight the selected button
                            btn.BackColor = Color.LightBlue;
                        }
                        else
                        {
                            // Reset others
                            btn.BackColor = SystemColors.Control;
                        }
                    }
                }

                tableLayoutPanel1.Refresh();  // Refresh the table layout panel
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product list: " + ex.Message);
            }
        }


        // Handle category button click
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Categoryid = int.Parse(btn.Tag.ToString());

            Console.WriteLine("CategoryId selected: " + Categoryid);  // Debugging category selection

            getListProduct(Categoryid);
        }

        // Handle "All" category button click
        private void btnAll_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Console.WriteLine("CategoryId selected: 0 (All)");  // Debugging "All" selection

            getListProduct(0);
        }

        // Handle "Back" button click
        private static FrmMain mainFormInstance;
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (mainFormInstance == null || mainFormInstance.IsDisposed)
            {
                mainFormInstance = new FrmMain();
            }

            mainFormInstance.Show();
            ClsHelper.Instance.openChildForm(mainFormInstance.pnlBody, new FrmDashboard());
            this.Hide();
        }

        // Handle product add to cart
        public void btnAdd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataRow dr = (DataRow)btn.Tag;

            double price = double.Parse(dr["Price"].ToString());
            int qty = 1;  // Default quantity is 1

            double amount = price * qty;

            bool isExist = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Id"].Value.ToString() == dr["Id"].ToString())
                {
                    isExist = true;
                    row.Cells["Qty"].Value = int.Parse(row.Cells["Qty"].Value.ToString()) + 1;
                    row.Cells["Amount"].Value = (int.Parse(row.Cells["Qty"].Value.ToString()) * price).ToString();
                    break;
                }
            }

            if (!isExist)
            {
                dataGridView1.Rows.Add(new object[] {
                    "1",  // Row Number (auto-generated if needed)
                    dr["Id"].ToString(),
                    dr["Name"].ToString(),
                    dr["Description"].ToString(),
                    price.ToString(),
                    qty,
                    amount.ToString()
                });
            }

            getTotal();
        }

        // Update total amount in cart
        public void getTotal()
        {
            double total = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.IsNewRow) continue;
                double price = double.Parse(dr.Cells["Price"].Value.ToString());
                int qty = int.Parse(dr.Cells["Qty"].Value.ToString());
                total += (price * qty);
            }
            lbTotal.Text = "$ " + total.ToString("F2");  // Display formatted total
        }

        // Handle remove item button in cart
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

        // Handle checkout button click
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
            lbTotal.Text = "0.00";
        }

        // Update change amount based on received amount
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
    }
}
