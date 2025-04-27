using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NIT_G2;

namespace Frm_login_HW1.Product
{
    public class ProductRepository
    {
        public void Insert(Product product)
        {
            try
            {
                ClsHelper.Open(); // ✅ Open connection safely

                string sql = "INSERT INTO Product (CategoryId, Name, Description, Qty, Price, Status, Image) " +
                             "VALUES (@CategoryId, @Name, @Description, @Qty, @Price, @Status, @Image)";

                using (SqlCommand cmd = new SqlCommand(sql, ClsHelper.con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryID);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Qty", product.Qty);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Status", product.Status);
                    cmd.Parameters.AddWithValue("@Image", (object)product.Image ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClsHelper.Close();
            }
        }

        public void Update(Product product)
        {
            try
            {
                ClsHelper.Open();

                string sql = "UPDATE Product SET CategoryId=@CategoryId, Name=@Name, Description=@Description, Qty=@Qty, Price=@Price, Status=@Status, Image=@Image WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, ClsHelper.con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryID);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Qty", product.Qty);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Status", product.Status);
                    cmd.Parameters.AddWithValue("@Image", (object)product.Image ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id", product.Id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClsHelper.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                ClsHelper.Open();

                string sql = "DELETE FROM Product WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, ClsHelper.con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClsHelper.Close();
            }
        }

        public DataTable GetAll()
        {
            try
            {
                return ClsHelper.Instance.getTable("SELECT * FROM Product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetAll Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
