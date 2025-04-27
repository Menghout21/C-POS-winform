using NIT_G2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Frm_login_HW1.Product;

namespace Frm_login_HW1.POS
{
    internal class clsInvoice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public double TotalAmount { get; set; }
        public int TotalQty { get; set; }
        public double TotalPaid { get; set; }
        public double DueAmount { get; set; }
        public string InvoiceStatus { get; set; } // Complete, Pending, Cancel
        public bool IsReturn { get; set; } // 1 | o

        public int order()
        {
            try
            {
                ClsHelper.con.Open();
                // Insert Data To Invoive Table 
                string sql = "INSERT INTO Invoice (UserId,CustomerId,TotalAmount,TotalQty,TotalPaid,DueAmount,InvoiceStatus,IsReturn) OUTPUT INSERTED.ID VALUES (@UserId,@CustomerId,@TotalAmount,@TotalQty,@TotalPaid,@DueAmount,@InvoiceStatus,@IsReturn)";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@TotalQty", TotalQty);
                cmd.Parameters.AddWithValue("@TotalPaid", TotalPaid);
                cmd.Parameters.AddWithValue("@DueAmount", DueAmount);
                cmd.Parameters.AddWithValue("@InvoiceStatus", InvoiceStatus);
                cmd.Parameters.AddWithValue("@IsReturn", IsReturn);
                //cmd.ExecuteNonQuery();
                int modified = (int)cmd.ExecuteScalar();
                if (ClsHelper.con.State == System.Data.ConnectionState.Open)
                {
                    ClsHelper.con.Close();
                }

                return modified;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
