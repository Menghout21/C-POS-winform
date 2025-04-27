using NIT_G2;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Frm_login_HW1.Dashboard
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            // Load data into the controls
            LoadDashboardData();
            BorderRadiusHelper.ApplyBorderRadius(panel1, 10);
            BorderRadiusHelper.ApplyBorderRadius(panel2, 10);
        }

        private void LoadDashboardData()
        {
            // Get today's sales and total sales
            decimal todaySale = GetTodaySale();
            decimal totalSale = GetTotalSale();

            // Set the values to the textboxes
            txtTodaySale.Text = todaySale.ToString("C");
            txtTotalSale.Text = totalSale.ToString("C");

            // Update the chart with the total sales data
            UpdateChartWithTotalSales();
        }

        // Get the total sales for today
        private decimal GetTodaySale()
        {
            decimal todaySale = 0;
            DateTime today = DateTime.Today;

            // SQL query to get the total sales for today using CteateAt
            string query = "SELECT SUM(TotalAmount) FROM Invoice WHERE CAST(CteateAt AS DATE) = @today";

            using (SqlCommand command = new SqlCommand(query, ClsHelper.con))
            {
                command.Parameters.AddWithValue("@today", today);

                try
                {
                    // Ensure the connection is open before executing the query
                    if (ClsHelper.con.State == ConnectionState.Closed)
                    {
                        ClsHelper.con.Open();
                    }

                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        todaySale = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed in the finally block
                    if (ClsHelper.con.State == ConnectionState.Open)
                    {
                        ClsHelper.con.Close();
                    }
                }
            }

            return todaySale;
        }

        // Get the total sales (sum of all TotalAmount)
        private decimal GetTotalSale()
        {
            decimal totalSale = 0;

            // SQL query to get the total sales using CteateAt
            string query = "SELECT SUM(TotalAmount) FROM Invoice";

            using (SqlCommand command = new SqlCommand(query, ClsHelper.con))
            {
                try
                {
                    // Ensure the connection is open before executing the query
                    if (ClsHelper.con.State == ConnectionState.Closed)
                    {
                        ClsHelper.con.Open();
                    }

                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalSale = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed in the finally block
                    if (ClsHelper.con.State == ConnectionState.Open)
                    {
                        ClsHelper.con.Close();
                    }
                }
            }

            return totalSale;
        }

        // Update the chart with the total sales data
        private void UpdateChartWithTotalSales()
        {
            // Clear any previous data in the chart
            chart1.Series.Clear();

            // SQL query to get total sales by day for chart, using CteateAt instead of CreateAt
            string query = "SELECT CAST(CteateAt AS DATE) AS SaleDate, SUM(TotalAmount) AS DailySales " +
                           "FROM Invoice " +
                           "GROUP BY CAST(CteateAt AS DATE) ORDER BY SaleDate DESC";

            using (SqlCommand command = new SqlCommand(query, ClsHelper.con))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    // Ensure the connection is open before executing the query
                    if (ClsHelper.con.State == ConnectionState.Closed)
                    {
                        ClsHelper.con.Open();
                    }

                    dataAdapter.Fill(dataTable);

                    // Create a new series for the chart
                    Series series = new Series("Sales")
                    {
                        ChartType = SeriesChartType.Column, // Choose your preferred chart type (Column, Line, etc.)
                        BorderWidth = 2
                    };

                    // Add the data points from the data table to the chart series
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime saleDate = Convert.ToDateTime(row["SaleDate"]);
                        decimal dailySales = Convert.ToDecimal(row["DailySales"]);
                        series.Points.AddXY(saleDate.ToShortDateString(), dailySales);
                    }

                    // Add the series to the chart
                    chart1.Series.Add(series);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating chart: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed in the finally block
                    if (ClsHelper.con.State == ConnectionState.Open)
                    {
                        ClsHelper.con.Close();
                    }
                }
            }
        }
    }
}
