using NIT_G2;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frm_login_HW1.Order_Status
{
    class clsOrderStatus
    {
        public static int ID { get; set; }
        public static string Name { get; set; }
        public static string Description { get; set; }
        public static bool Status { get; set; }
        public static string Create_at { get; set; }

        public static void Insert()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ClsHelper.con.ConnectionString))
                {
                    con.Open();
                    string sql = "INSERT INTO Order_Status (Name, Description, Status) VALUES (@Name, @Description, @Status)";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Description", Description); // Fix typo below too
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection con = new SqlConnection(ClsHelper.con.ConnectionString))
                {
                    con.Open();
                    string sql = "UPDATE Order_Status SET Name = @Name, Description = @Description, Status = @Status WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(sql, con); // Also changed ClsHelper.con to local `con`
                    cmd.Parameters.AddWithValue("@Id", ID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Description", Description); // ✅ fixed here
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.ExecuteNonQuery();
                }
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
                string sql = $"DELETE FROM Order_Status  WHERE Id =@Id";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@Id", ID);
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
