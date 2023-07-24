using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class ImportBL
    {
        #region Field
        private DateTime dateImport;
        private int idEmployee;
        private int idImport;
        private int idSupplier;
        private float totalBill;
        private static ImportBL instance;
        #endregion

        #region Constructor
        public ImportBL(int idImport, int idEmployee, int idSupplier, DateTime dateImport, float totalBill)
        {
            this.IdImport = idImport;
            this.IdEmployee = idEmployee;
            this.IdSupplier = idSupplier;
            this.DateImport = dateImport;
            this.TotalBill = totalBill;
        }

        public ImportBL() { }
        #endregion

        #region Property
        public int IdImport
        {
            get { return idImport; }
            set { idImport = value; }
        }

        public int IdEmployee
        {
            get { return idEmployee; }
            set { idEmployee = value; }
        }

        public int IdSupplier
        {
            get { return idSupplier; }
            set { idSupplier = value; }
        }

        public DateTime DateImport
        {
            get { return dateImport; }
            set { dateImport = value; }
        }

        public float TotalBill
        {
            get { return totalBill; }
            set { totalBill = value; }
        }

        public static ImportBL Instance
        {
            get { if (instance == null) instance = new ImportBL(); return ImportBL.instance; }
            private set { ImportBL.instance = value; }
        }
        #endregion

        #region Method
        public DataTable GetImportList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM View_PhieuNhap");
        }

        public DataTable GetInfoImport()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM UFN_TimThongTinPhieuNhap()");
        }

        public DataTable GetTotalImport(int idImport)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_TongGiaTriPhieuNhap @idPN", new object[] { idImport });
        }

        public bool InsertImport(int idEmployee, int idSupplier, DateTime dateImport)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_TaoPhieuNhap @idNV , @idNCC , @NgayNhap", new object[] { idEmployee, idSupplier, dateImport });
            return result > 0;
        }

        public bool CheckOut(int idImport)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_NhapHang @idPN", new object[] { idImport });

            return result > 0;
        }

        public bool DeleteImport(int idImport)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_XoaPhieuNhap @idPN", new object[] { idImport });

            return result > 0;
        }

        public DataTable SearchImport(int idImport)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.UFN_TimKiemPhieuNhap ( @idPN )", new object[] { idImport });
        }
        #endregion
    }
}