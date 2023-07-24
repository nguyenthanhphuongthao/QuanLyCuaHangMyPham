using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class ProducerBL
    {
        private int idProducer;
        private string producerName;
        private string nation;
        private static ProducerBL instance;

        public ProducerBL() { }

        public ProducerBL(int idProducer, string producerName, string nation)
        {
            this.IdProducer = idProducer;
            this.ProducerName = producerName;
            this.Nation = nation;
        }
        public int IdProducer
        {
            get { return idProducer; }
            set { idProducer = value; }
        }

        public string ProducerName
        {
            get { return producerName; }
            set { producerName = value; }
        }

        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        public static ProducerBL Instance
        {
            get { if (instance == null) instance = new ProducerBL(); return ProducerBL.instance; }
            private set { ProducerBL.instance = value; }
        }

        public DataTable FindProducer(int idProducer)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.UFN_TimThongTinNhaSanXuat ( @idNSX )", new object[] { idProducer });
        }

        public bool InsertProducer(int idProducer, string producerName, string producerNation)
        {
            string query = string.Format("EXEC USP_ThemNhaSanXuat @idNSX , @TenNSX , @QuocGia");
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { idProducer, producerName, producerNation });

            return result > 0;
        }
    }
}