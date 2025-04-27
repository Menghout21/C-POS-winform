using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Windows.Forms.Control;

namespace NIT_G2
{
    internal class ClsHelper
    {
        public static Form activeForm = null;
        string machineName = Environment.MachineName;
        static string conString = $"data source={Environment.MachineName}\\SQLEXPRESS; database=ComputerShop_db; integrated security=true";
        public static SqlConnection con = new SqlConnection(conString);

        public static void ExecuteQueries(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.ExecuteNonQuery();
        }
        public static SqlDataReader DataReader(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public static DataTable getTable(string query)
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.ToString());
            }
            con.Close();
            return dt;
        }
        public static void BoundComboBox(ComboBox c, string q)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                c.DataSource = dt;
                c.ValueMember = dt.Columns[0].ColumnName;
                c.DisplayMember = dt.Columns[1].ColumnName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void SetAllControlDefault(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {

                if (ctrl.Controls != null)
                {
                    SetAllControlDefault(ctrl.Controls);
                }
                // font family and font size
                ctrl.Font = new Font("Roboto", ctrl.Font.Size + 2);
                // data gridview
                if (ctrl is DataGridView)
                {
                    DataGridView ctrl2 = (DataGridView)ctrl;
                    ctrl2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    ctrl2.BackgroundColor = SystemColors.Control;
                    ctrl2.DefaultCellStyle.BackColor = SystemColors.Control;
                    ctrl2.Anchor = (
                        (System.Windows.Forms.AnchorStyles)
                        (System.Windows.Forms.AnchorStyles.Bottom |
                        System.Windows.Forms.AnchorStyles.Left |
                        System.Windows.Forms.AnchorStyles.Top |
                        System.Windows.Forms.AnchorStyles.Right));
                    ctrl2.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ActiveCaption;
                    ctrl2.EnableHeadersVisualStyles = false;
                    ctrl2.ReadOnly = true;
                }

                
}
        }
        public static void setBlurBackground(Form Model)
        {
            Form background = new Form();
            using (Model)
            {
                background.StartPosition = FormStartPosition.Manual;
                background.FormBorderStyle = FormBorderStyle.None;
                background.Opacity = 0.3d;
                background.BackColor = Color.Black;
                background.Size = Form.ActiveForm.Size;
                background.Location = Form.ActiveForm.Location;
                background.ShowInTaskbar = false;
                background.Show();
                Model.StartPosition = FormStartPosition.CenterParent;
                //Model.FormBorderStyle = FormBorderStyle.None;
                Model.ShowDialog(background);
                background.Dispose();
            }
        }
        public void imageSelect()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                //filePath = ofd.FileName;
                //pictureBox1.Image = new Bitmap(filePath);
            }
        }
        public static void Message()
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
        public static void openChildForm(Panel p, Form childForm)
        {
            if (activeForm != null)
            {
                if (activeForm.GetType() == childForm.GetType())
                {
                    return;
                }
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            p.Controls.Add(childForm);
            p.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public static void exitProgram()
        {
            Application.Exit();
        }

        public static void Open() // open connection C# <> SQL server by conString
        {
            //if (con.State == System.Data.ConnectionState.Closed)
            //{
            con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection opened successfully.");
            //}

        }
        public static void Close()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
                Console.WriteLine("Connection opened successfully.");
            }
        }
    }
}