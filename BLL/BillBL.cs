using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class BillBL
    {
        #region Field
        private DateTime dateBill;
        private int idBill;
        private string customerName;
        private string employeeName;
        private int quantity;
        private float totalBill;
        private static BillBL instance;
        
        #endregion

        #region Constructor
        public BillBL(int idBill, DateTime dateBill, string employeeName, string customerName, int quantity, float totalBill)
        {
            this.IdBill = idBill;
            this.DateBill = dateBill;
            this.EmployeeName = employeeName;
            this.CustomerName = customerName;
            this.Quantity = quantity;
            this.TotalBill = totalBill;
        }

        public BillBL() { }
        #endregion

        #region Property
        public int IdBill
        {
            get { return idBill; }
            set { idBill = value; }
        }

        public DateTime DateBill
        {
            get { return dateBill; }
            set { dateBill = value; }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public float TotalBill
        {
            get { return totalBill; }
            set { totalBill = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public static BillBL Instance
        {
            get { if (instance == null) instance = new BillBL(); return BillBL.instance; }
            private set { BillBL.instance = value; }
        }

        
        #endregion

        #region Method
        public DataTable GetSalesList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM View_DonHang");
        }

        public DataTable GetBillList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM View_DonHang");
        }

        public DataTable GetNumBillMonth()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_SoDonHangTrongThang");
        }

        public DataTable GetNumWeeklySales()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_DoanhThuBayNgay");
        }

        public DataTable GetNumMonthlySales()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_DoanhThuTheoThang");
        }

        public DataTable GetInfoBill()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM UFN_TimThongTinDonHang()");
        }

        public DataTable GetTotalBill(int idBill)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_TongGiaTriDonHang @idDH", new object[] { idBill});
        }

        public bool InsertBill(DateTime dateBill, int idEmployee)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_TaoDonHang @NgayLenDon , @idNV", new object[] { dateBill, idEmployee });
            return result > 0;
        }

        public bool CheckOut(int idBill, string customerPhone)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_ThanhToan @idDH , @SDT", new object[] { idBill, customerPhone});

            return result > 0;
        }

        public bool DeleteBill(int idBill)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_XoaDonHang @idDH", new object[] { idBill });

            return result > 0;
        }

        public DataTable SearchBill(int idBill)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.UFN_TimKiemDonHang ( @idDH )", new object[] { idBill });
        }

        public DataTable GetTurnoverByDate(DateTime startDate, DateTime endDate)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_DoanhThuTheoNgay @NgayDau , @NgayCuoi", new object[] { startDate, endDate });
        }

        public int GetNumBillListByDate(DateTime startDate, DateTime endDate)
        {
            return (int)DAL.DataProvider.Instance.ExecuteScalar("EXEC USP_SoDonHangTheoNgay @NgayDau , @NgayCuoi", new object[] { startDate, endDate });
        }

        public DataTable GetBillListByDateAndPage(DateTime startDate, DateTime endDate, int pageNum)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("EXEC USP_DanhSachDonHangTheoNgayVaTrang @NgayDau , @NgayCuoi , @page", new object[] { startDate, endDate, pageNum });
        }
        #endregion
    }
}