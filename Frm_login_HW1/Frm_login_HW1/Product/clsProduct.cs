using NIT_G2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1.Product
{
    class clsProduct
    {
        public static int Id { get; set; }
        public static string Name { get; set; }
        public static string Description { get; set; }
        public static int Qty { get; set; }
        public static double Price { get; set; }
        public static int CategoryID { get; set; }
        public static byte[] Image { get; set; }
        public static bool Status { get; set; }







        public static void Insert()
        {
            try
            {
                ClsHelper.con.Open();
                string sql = "INSERT INTO Product (CategoryId, Name, Description, Qty, Price, Status, Image) " +
                             "VALUES (@CategoryId, @Name, @Description, @Qty, @Price, @Status, @Image)";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Image", Image); // Should be byte[] if binary
                cmd.ExecuteNonQuery();
                ClsHelper.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public static void Update()
        {
            try
            {
                ClsHelper.con.Open();
                string sql = "UPDATE Product SET CategoryId=@CategoryId, Name=@Name, Description=@Description, Qty=@Qty, Price=@Price, Status=@Status, Image=@Image WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Image", Image); // should be byte[] if storing image content
                cmd.Parameters.AddWithValue("@Id", Id); // make sure this is the product's ID
                cmd.ExecuteNonQuery();
                ClsHelper.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public static void Delete()
        {
            try
            {
                ClsHelper.con.Open();
                string sql = $"DELETE FROM Product  WHERE Id =@Id";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
                ClsHelper.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        

    }
}
