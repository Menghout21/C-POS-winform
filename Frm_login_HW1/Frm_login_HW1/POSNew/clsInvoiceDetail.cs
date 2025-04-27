using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NIT_G2;
using System.Windows.Forms;


namespace Frm_login_HW1.POS
{
    class clsInvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }

        public void orderDetails()
        {
            try
            {
                ClsHelper.con.Open();
                // Insert Data To Invoive Details
                string sql = "INSERT INTO InvoiceDetail (InvoiceId,ProductId,Qty,Price,Discount) VALUES (@InvoiceId,@ProductId,@Qty,@Price,@Discount)";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Discount", Discount);
                cmd.ExecuteNonQuery();
                ClsHelper.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
