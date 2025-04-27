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
using System.Data.SqlClient;

namespace Frm_login_HW1.OrderHistory
{
    public partial class FrmOrderHistory: Form
    {
        public FrmOrderHistory()
        {
            InitializeComponent();
        }

        private void FrmOrderHistory_Load(object sender, EventArgs e)
        {
            getData();
        }
        public void getData()
        {
            string sql = "SELECT *FROM OrderHistory";
            dataGridView1.DataSource = ClsHelper.Instance.getTable(sql);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchInvoiceId = txtSearch.Text.Trim();

            string sql = "SELECT * FROM OrderHistory WHERE InvoiceId LIKE @search";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, ClsHelper.con))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchInvoiceId + "%");

                    DataTable dt = new DataTable();
                    ClsHelper.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClsHelper.Close();
            }
        }

    }
}
