using NIT_G2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Frm_login_HW1.Employee
{
    class clsUser
    {
        public static int Id { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string Role { get; set; }

        public static void Insert()
        {
            try
            {
                ClsHelper.con.Open();
                string sql = $"INSERT INTO [User] (FirstName, LastName, UserName, Password, Role) VALUES (@FirstName, @LastName, @UserName, @Password, @Role)";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ClsHelper.con.Close();
            }
        }

        public static void Update()
        {
            try
            {
                ClsHelper.con.Open();
                string sql = "UPDATE [User] SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Password = @Password, Role = @Role WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ClsHelper.con.Close();
            }
        }

        public static void Delete()
        {
            try
            {
                ClsHelper.con.Open();
                string sql = $"DELETE FROM [User] WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, ClsHelper.con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ClsHelper.con.Close();
            }
        }

    }
}
