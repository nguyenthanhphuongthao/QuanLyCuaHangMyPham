using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class CustomerBL
    {
        #region Field
        private string customerName;
        private int idCustomer;
        private string phone;
        private static CustomerBL instance;
        #endregion

        #region Constructor
        public CustomerBL(int idCustomer, string customerName, string phone)
        {
            this.IdCustomer = idCustomer;
            this.CustomerName = customerName;
            this.Phone = phone;
        }

        public CustomerBL() { }
        #endregion

        #region Property
        public int IdCustomer
        {
            get { return idCustomer; }
            set { idCustomer = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public static CustomerBL Instance
        {
            get { if (instance == null) instance = new CustomerBL(); return CustomerBL.instance; }
            private set { CustomerBL.instance = value; }
        }
        #endregion

        #region Method
        public DataTable GetCustomerList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM KhachHang");
        }

        public DataTable GetCustomerBillList(int idCustomer)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_DonHangDaMua @idKH", new object[] { idCustomer});
        }

        public string FindCustomer(string customerPhone)
        {
            return (string)DAL.DataProvider.Instance.ExecuteScalar("SELECT dbo.UFN_TimThongTinKhachHang( @SDT )", new object[] { customerPhone });
        }

        public bool InsertCustomer(string customerPhone, string customerName)
        {
            string query = string.Format("EXEC USP_ThemKhachHang @TenKH , @SDT");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { customerName, customerPhone });

            return result > 0;
        }

        public DataTable SearchCustomer(string customerPhone)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM UFN_TimKiemKhachHang ( @SDT )", new object[] { customerPhone });
        }
        #endregion
    }
}