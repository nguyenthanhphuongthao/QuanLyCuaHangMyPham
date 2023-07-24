using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class DetailImportBL
    {
        #region Field
        private int idDetailImport;
        private int idImport;
        private int idProduct;
        private int quantity;
        private string unit;
        private float unitPrice;
        private static DetailImportBL instance;
        #endregion

        #region Constructor
        public DetailImportBL(int idDetailImport, int idImport, int idProduct, int quantity, float unitPrice, string unit)
        {
            this.IdDetailImport = idDetailImport;
            this.IdImport = idImport;
            this.IdProduct = idProduct;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.Unit = unit;
        }

        public DetailImportBL() { }
        #endregion

        #region Property
        public int IdDetailImport
        {
            get { return idDetailImport; }
            set { idDetailImport = value; }
        }

        public int IdImport
        {
            get { return idImport; }
            set { idImport = value; }
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

        public float UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public static DetailImportBL Instance
        {
            get { if (instance == null) instance = new DetailImportBL(); return DetailImportBL.instance; }
            private set { DetailImportBL.instance = value; }
        }
        #endregion

        #region Method
        public DataTable GetMIDetailImportList(int idImport)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_ChiTietPhieuNhap @idPN", new object[] { idImport});
        }

        public DataTable GetCIDetailImportList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_ChiTietPhieuNhapMoi");
        }

        public bool InsertDetailImport(int idImport, int idProduct, string productName, int idProducer, string productTypeName, int quantity, float price)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_ThemChiTietPhieuNhap @idPN , @idHH , @TenHH , @idNSX , @TenLoai , @SoLuong , @Gia", new object[] { idImport, idProduct, productName, idProducer, productTypeName, quantity, price });

            return result > 0;
        }

        public bool UpdateDetailImport(int idImport, int idProduct, int quantity, float price)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_CapNhatChiTietPhieuNhap @idPN , @idHH , @SoLuong , @Gia", new object[] { idImport, idProduct, quantity, price });

            return result > 0;
        }

        public bool DeleteDetailImport(int idImport, int idProduct)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_XoaChiTietPhieuNhap @idPN , @idHH", new object[] { idImport, idProduct});

            return result > 0;
        }
        #endregion
    }
}