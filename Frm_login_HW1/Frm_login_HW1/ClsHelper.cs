using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace NIT_G2
{
    internal class ClsHelper
    {
        private static ClsHelper _instance;
        private static readonly object _lock = new object();

        public static Form activeForm = null;

        // 🔵 Connection string (auto get machine name)
        private static readonly string conString = $"Data Source={Environment.MachineName}\\SQLEXPRESS;Database=ComputerShop_db;Integrated Security=True";
        public static SqlConnection con = new SqlConnection(conString);

        // 🔵 Private constructor (singleton pattern)
        private ClsHelper()
        {
            // Can initialize things here if needed
        }

        // 🔵 Public instance access
        public static ClsHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ClsHelper();
                    return _instance;
                }
            }
        }

        // 🔵 Execute query without return
        public void ExecuteQueries(string query)
        {
            try
            {
                Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Close();
            }
        }

        // 🔵 Execute query that returns SqlDataReader
        public SqlDataReader DataReader(string query)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // 🔵 Execute query that returns DataTable
        public DataTable getTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                Open();
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                Close();
            }
            return dt;
        }

        // 🔵 Bind data to ComboBox
        public void BoundComboBox(ComboBox cbo, string query)
        {
            try
            {
                Open();
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cbo.DataSource = dt;
                        cbo.ValueMember = dt.Columns[0].ColumnName;   // First column as value
                        cbo.DisplayMember = dt.Columns[1].ColumnName; // Second column as display
                        cbo.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ComboBox Binding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Close();
            }
        }

        // 🔵 Set font & styling for all controls
        public void SetAllControlDefault(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl.Controls.Count > 0)
                    SetAllControlDefault(ctrl.Controls);

                if (!(ctrl is Label))
                    ctrl.Font = new Font("Roboto", ctrl.Font.Size + 2);

                if (ctrl is DataGridView dgv)
                {
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.BackgroundColor = SystemColors.Control;
                    dgv.DefaultCellStyle.BackColor = SystemColors.Control;
                    dgv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ActiveCaption;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ReadOnly = true;
                }
            }
        }

        // 🔵 Create a blurred background when showing modal form
        public void setBlurBackground(Form modelForm)
        {
            Form background = new Form();
            background.StartPosition = FormStartPosition.Manual;
            background.FormBorderStyle = FormBorderStyle.None;
            background.Opacity = 0.3d;
            background.BackColor = Color.Black;
            background.Size = Form.ActiveForm.Size;
            background.Location = Form.ActiveForm.Location;
            background.ShowInTaskbar = false;
            background.Show();

            modelForm.StartPosition = FormStartPosition.CenterParent;
            modelForm.ShowDialog(background);

            background.Dispose();
        }

        // 🔵 Image selector (return Image object)
        public static Image ImageSelect()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return new Bitmap(ofd.FileName);
                }
            }
            return null;
        }

        // 🔵 Simple Yes/No message box
        public static void Message()
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Do something if Yes
            }
            else
            {
                // Do something if No
            }
        }

        // 🔵 Open a child form inside a Panel
        public void openChildForm(Panel panelContainer, Form childForm)
        {
            if (activeForm != null)
            {
                if (activeForm.GetType() == childForm.GetType())
                    return;

                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(childForm);
            panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // 🔵 Exit program
        public static void exitProgram()
        {
            Application.Exit();
        }

        // 🔵 Open connection
        public static void Open()
        {
            if (con == null)
                con = new SqlConnection(conString);

            if (con.State != ConnectionState.Open)
                con.Open();
        }

        // 🔵 Close connection
        public static void Close()
        {
            if (con != null && con.State == ConnectionState.Open)
                con.Close();
        }
    }
}
