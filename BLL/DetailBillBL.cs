using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class DetailBillBL
    {
        #region Field
        private int idBill;
        private int idDetailBill;
        private int idProduct;
        private int quantity;
        private static DetailBillBL instance;
        #endregion

        #region Constructor
        public DetailBillBL(int idDetailBill, int idBill, int idProduct, int quantity)
        {
            this.IdDetailBill = idDetailBill;
            this.IdBill = idBill;
            this.IdProduct = idProduct;
            this.Quantity = quantity;
        }

        public DetailBillBL() { }
        #endregion

        #region Property
        public int IdDetailBill
        {
            get { return idDetailBill; }
            set { idDetailBill = value; }
        }

        public int IdBill
        {
            get { return idBill; }
            set { idBill = value; }
        }

        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        
        public static DetailBillBL Instance
        {
            get { if (instance == null) instance = new DetailBillBL(); return DetailBillBL.instance; }
            private set { DetailBillBL.instance = value; }
        }
        #endregion

        #region Method
        public DataTable GetMBDetailBillList(int idBill)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_ChiTietDonHang @idDH", new object[] { idBill });
        }

        public DataTable GetCBDetailBillList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_ChiTietDonHangMoi");
        }

        public DataTable InsertDetailBill(int idBill, int idEmployee, DateTime dateBill, int idProduct, float price, int quantity)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_ThemChiTietDonHang @idDH , @NgayLenDon , @idNV , @Gia , @idHH , @SoLuong", new object[] { idBill, dateBill, idEmployee,  price, idProduct, quantity});
        }

        public bool UpdateDetailBill(int idBill, int idProduct, int quantity)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_CapNhatChiTietDonHang @idDH , @idHH , @SoLuong", new object[] { idBill, idProduct, quantity });

            return result > 0;
        }

        public bool InsertDetailBill(int idBill, int idProduct, int quantity)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_ThemChiTietDonHang @idDH , @idHH , @SoLuong", new object[] { idBill, idProduct, quantity });

            return result > 0;
        }
        public bool DeleteDetailBill(int idBill, int idProduct)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_XoaChiTietDonHang @idDH , @idHH", new object[] { idBill, idProduct });

            return result > 0;
        }
        #endregion
    }
}