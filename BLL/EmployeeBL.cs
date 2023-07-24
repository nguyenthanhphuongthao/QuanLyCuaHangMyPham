using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class EmployeeBL
    {
        #region Field
        private DateTime birthEmployee;
        private string employeeName;
        private int idEmployee;
        private string phone;
        private int position;
        private string status;
        private static EmployeeBL instance;
        #endregion

        #region Constructor
        public EmployeeBL(int idEmployee, string employeeName, DateTime birthEmployee, string phone, int position, string status)
        {
            this.IdEmployee = idEmployee;
            this.EmployeeName = employeeName;
            this.BirthEmployee = birthEmployee;
            this.Phone = phone;
            this.Position = position;
            this.Status = status;
        }

        public EmployeeBL() { }
        #endregion

        #region Property
        public int IdEmployee
        {
            get { return idEmployee; }
            set { idEmployee = value; }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        public DateTime BirthEmployee
        {
            get { return birthEmployee; }
            set { birthEmployee = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public static EmployeeBL Instance
        {
            get { if (instance == null) instance = new EmployeeBL(); return EmployeeBL.instance; }
            private set { EmployeeBL.instance = value; }
        }
        #endregion

        #region Method
        public DataTable GetEmployeeList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM View_NhanVien");
        }

        public bool InsertEmployee(string employeeName, DateTime birthEmployee, string employeePhone, string position)
        {
            string query = string.Format("EXEC USP_ThemNhanVien @TenNhanVien , @NgaySinh , @SDT , @ChucVu");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { employeeName, birthEmployee, employeePhone, position });

            return result > 0;
        }

        public bool UpdateEmployee(int idEmployee, string employeeName, DateTime birthEmployee, string employeePhone, string position)
        {
            string query = string.Format("EXEC USP_CapNhatNhanVien @idNV ,  @TenNV , @NgaySinh , @SDT , @ChucVu");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { idEmployee, employeeName, birthEmployee, employeePhone, position });

            return result > 0;
        }

        public bool DeleteEmployee(int idEmployee)
        {
            string query = string.Format("EXEC USP_XoaNhanVien @idNV");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { idEmployee});

            return result > 0;
        }

        public string FindEmployee(int idEmployee)
        {
            return (string)DAL.DataProvider.Instance.ExecuteScalar("SELECT dbo.UFN_TimThongTinNhanVien( @idNV )", new object[] { idEmployee });
        }

        public DataTable SearchEmployee(int idEmployee)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM UFN_TimKiemNhanVien ( @idNV )", new object[] { idEmployee });
        }
        #endregion
    }
}