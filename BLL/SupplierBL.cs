using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class SupplierBL
    {
        private int idSupplier;
        private string supplierName;
        private string phone;
        private string address;
        private string email;
        private static SupplierBL instance;
        public SupplierBL() { }

        public SupplierBL(int idSupplier, string supplierName, string address, string phone, string email)
        {
            this.IdSupplier = idSupplier;
            this.SupplierName = supplierName;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
        }
        public int IdSupplier
        {
            get { return idSupplier; }
            set { idSupplier = value; }
        }

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public static SupplierBL Instance
        {
            get { if (instance == null) instance = new SupplierBL(); return SupplierBL.instance; }
            private set { SupplierBL.instance = value; }
        }

        public string FindSupplier(int idSupplier)
        {
            return (string)DAL.DataProvider.Instance.ExecuteScalar("SELECT dbo.UFN_TimThongTinNhaCungCap ( @idNCC )", new object[] { idSupplier });
        }

        public bool InsertSupplier(int idSupplier, string supplierName, string supplierAddress, string supplierPhone, string supplierEmail)
        {
            string query = string.Format("EXEC USP_ThemNhaCungCap @idNCC , @TenNCC , @DiaChi , @SDT , @Email");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { idSupplier, supplierName, supplierAddress, supplierPhone, supplierEmail });

            return result > 0;
        }
    }
}