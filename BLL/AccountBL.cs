using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class AccountBL
    {
        #region Field
        private string password;
        private int idPosition;
        private string username;
        private static AccountBL instance;
        #endregion

        #region Constructor
        public AccountBL(string username, string password, int idPosition)
        {
            this.Username = username;
            this.Password = password;
            this.idPosition = idPosition;
        }

        public AccountBL(DataRow row)
        {
            this.Username = row["TenDangNhap"].ToString();
            this.Password = row["MatKhau"].ToString();
            this.IdPosition = (int)row["idChucVu"];
        }

        public AccountBL() { }
        #endregion

        #region Property
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int IdPosition
        {
            get { return idPosition; }
            set { idPosition = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public static AccountBL Instance
        {
            get { if (instance == null) instance = new AccountBL(); return AccountBL.instance; }
            private set { AccountBL.instance = value; }
        }
        #endregion

        #region Method
        string hashPass(string passWord)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            return hasPass;
        }

        public bool Login(string username, string password)
        {
            string hasPass = hashPass(password);
            DAL.DataProvider.Instance.Username = username;
            DAL.DataProvider.Instance.Password = hasPass;

            string query = "USP_DangNhap @TenDangNhap , @MatKhau";

            DataTable result = DAL.DataProvider.Instance.ExecuteQuery(query, new object[] { username, hasPass });

            return result.Rows.Count > 0;
        }

        public AccountBL GetAccountByUsername(string username)
        {
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM TaiKhoan WHERE TenDangNhap = '" + username + "'");

            foreach (DataRow item in data.Rows)
            {
                return new AccountBL(item);
            }
            return null;
        }

        public DataTable GetAccountList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM View_TaiKhoan");
        }


        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            oldPassword = hashPass(oldPassword);
            newPassword = hashPass(newPassword);
            string query = string.Format("EXEC USP_DoiMatKhau @TenDangNhap , @MatKhauCu , @MatKhauMoi");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { username, oldPassword, newPassword});
            

            if (result > 0)
            {
                DAL.DataProvider.Instance.ChangePassword(newPassword);
                DAL.DataProvider.Instance.Password = newPassword;
            }
            
            return result > 0;
        }

        public bool ResetPassword(string username)
        {
            string query = string.Format("EXEC USP_DatLaiMatKhau @TenDangNhap");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { username });
            if (result > 0)
            {
                string defaultPass = hashPass("000");
                DAL.DataProvider.Instance.ChangePassword(defaultPass);
                DAL.DataProvider.Instance.Password = defaultPass;
            }

            return result > 0;
        }
        #endregion
    }
}