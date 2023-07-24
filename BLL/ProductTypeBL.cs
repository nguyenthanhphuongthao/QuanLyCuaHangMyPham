using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class ProductTypeBL
    {
        private int idProductType;
        private string productTypeName;
        private static ProductTypeBL instance;

        public ProductTypeBL() { }

        public ProductTypeBL(int idProductType, string productTypeName)
        {
            this.IdProductType = idProductType;
            this.ProductTypeName = productTypeName;
        }

        public ProductTypeBL(DataRow row)
        {
            this.IdProductType = (int)row["idLoaiMyPham"];
            this.ProductTypeName = row["TenLoai"].ToString();
        }

        public int IdProductType
        {
            get { return idProductType; }
            set { idProductType = value; }
        }

        public string ProductTypeName
        {
            get { return productTypeName; }
            set { productTypeName = value; }
        }

        public static ProductTypeBL Instance
        {
            get { if (instance == null) instance = new ProductTypeBL(); return ProductTypeBL.instance; }
            private set { ProductTypeBL.instance = value; }
        }

        public List<ProductTypeBL> GetListProductType()
        {
            List<ProductTypeBL> list = new List<ProductTypeBL>();

            string query = "SELECT * FROM LoaiMyPham";

            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ProductTypeBL productType = new ProductTypeBL(item);
                list.Add(productType);
            }

            return list;
        }
    }
}