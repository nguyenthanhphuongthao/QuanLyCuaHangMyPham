using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyCuaHangMyPham
{
    public class PositionBL
    {
        private int idPosition;
        private string positionName;
        private static PositionBL instance;

        public PositionBL() { }


        public PositionBL(int idPosition, string positionName)
        {
            this.IdPosition = idPosition;
            this.PositionName = positionName;
        }

        public PositionBL(DataRow row)
        {
            this.IdPosition = (int)row["idChucVu"];
            this.PositionName = row["TenChucVu"].ToString();
        }

        public int IdPosition
        {
            get { return idPosition; }
            set { idPosition = value; }
        }

        public string PositionName
        {
            get { return positionName; }
            set { positionName = value; }
        }

        public static PositionBL Instance
        {
            get { if (instance == null) instance = new PositionBL(); return PositionBL.instance; }
            private set { PositionBL.instance = value; }
        }

        public List<PositionBL> GetListPosition()
        {
            List<PositionBL> list = new List<PositionBL>();

            string query = "SELECT * FROM ChucVu";

            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                PositionBL position = new PositionBL(item);
                list.Add(position);
            }

            return list;
        }
    }
}