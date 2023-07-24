using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangMyPham.DAL
{
    class DataProvider
    {
        private static DataProvider instance;
        private string username;
        private string password;

        private string connectionSTR;

        //private string connectionSTR = "Data Source=.;Initial Catalog=QuanLyCuaHangMyPham;Integrated Security=True";
        private DataProvider() { }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        public void ChangePassword (string newPass)
        {
            connectionSTR = String.Format("Server=.;Database=QuanLyCuaHangMyPham;User id={0};Password={1}", username, password);
            
            try
            {
                SqlConnection.ChangePassword(connectionSTR, newPass);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            connectionSTR = String.Format("Server=.;Database=QuanLyCuaHangMyPham;User id={0};Password={1}", username, password);
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(data);

                    connection.Close();
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            connectionSTR = String.Format("Server=.;Database=QuanLyCuaHangMyPham;User id={0};Password={1}", username, password);
            int data = 0;
            try
            {
                

                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    data = command.ExecuteNonQuery();

                    connection.Close();
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            connectionSTR = String.Format("Server=.;Database=QuanLyCuaHangMyPham;User id={0};Password={1}", username, password);

            object data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    data = command.ExecuteScalar();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return data;
        }   
    }
}
