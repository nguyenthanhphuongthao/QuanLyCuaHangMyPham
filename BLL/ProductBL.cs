using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class ProductBL
    {
        #region Field
        private string description;
        private DateTime expiry;
        private int idCosmeticType;
        private int idImport;
        private int idProducer;
        private int idProduct;
        private int idSupplier;
        private int inventory;
        private float price;
        private string productName;
        private static ProductBL instance;
        #endregion

        #region Constructor
        public ProductBL(int idProduct, int idImport, string productName, float price, DateTime expiry, int idProducer, int idCosmeticType, int idSupplier, int inventory, string description)
        {
            this.IdProduct = idProduct;
            this.IdImport = idImport;
            this.ProductName = productName;
            this.Price = price;
            this.Expiry = expiry;
            this.IdProducer = idProducer;
            this.IdCosmeticType = idCosmeticType;
            this.IdSupplier = idSupplier;
            this.Inventory = inventory;
            this.Description = description;
        }

        public ProductBL() { }
        #endregion

        #region Property
        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        public int IdImport
        {
            get { return idImport; }
            set { idImport = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public DateTime Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        public int IdProducer
        {
            get { return idProducer; }
            set { idProducer = value; }
        }

        public int IdCosmeticType
        {
            get { return idCosmeticType; }
            set { idCosmeticType = value; }
        }

        public int IdSupplier
        {
            get { return idSupplier; }
            set { idSupplier = value; }
        }

        public int Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public static ProductBL Instance
        {
            get { if (instance == null) instance = new ProductBL(); return ProductBL.instance; }
            private set { ProductBL.instance = value; }
        }
        #endregion

        #region Method
        public DataTable GetProductList()
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM View_HangHoa");
        }

        public bool UpdateProduct(int idProduct, float price, string productTypeName)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_CapNhatHangHoa @idHH , @GiaBanMoi , @LoaiMyPham", new object[] { idProduct, price, productTypeName});

            return result > 0;
        }

        public DataTable FindProductCI(int idProduct)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM UFN_TimThongTinHangHoa_PN ( @idHH )", new object[] { idProduct });
        }

        public DataTable FindProductCB(int idProduct)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * from UFN_TimThongTinHangHoa( @idHH )", new object[] { idProduct });
        }

        public DataTable SearchProduct(int idProduct)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.UFN_TimKiemHangHoa ( @idHH )", new object[] { idProduct });
        }

        public bool DeleteProductCancelImport(int idProduct)
        {
            int result = DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_XoaHangHoaKhongDuocNhap @idHH", new object[] { idProduct });

            return result > 0;
        }
        #endregion
    }
}