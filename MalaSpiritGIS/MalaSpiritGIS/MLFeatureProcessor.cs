using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MalaSpiritGIS
{
    class MLRecord
    {
        uint id;
        string name;
        FeatureType featureType;
        public MLRecord(uint _id,string _name,string _typeStr)
        {
            id = _id;
            name = _name;
            switch (_typeStr)
            {
                case "POINT":
                    featureType = FeatureType.POINT;
                    break;
                case "POLYLINE":
                    featureType = FeatureType.POLYLINE;
                    break;
                case "POLYGON":
                    featureType = FeatureType.POLYGON;
                    break;
                case "MULTIPOINT":
                    featureType = FeatureType.MULTIPOINT;
                    break;
            }
        }
    }
    /// <summary>
    /// 在内存和数据库之间处理要素类
    /// </summary>
    class MLFeatureProcessor
    {
        string server="localhost";
        string database="mala spirit gis db";
        string userId="root";
        string password="";
        string charset = "utf8";
        string port = "3306";

        MySqlConnection connection;

        List<MLRecord> records;

        public MLFeatureProcessor()
        {
            string connstr = "server=" + server + ";database=" + database +
                ";uid=" + userId + ";charset=" + charset+";port="+port;
            connection = new MySqlConnection(connstr);
            connection.Open();
            records = new List<MLRecord>();
            string sql = "select * from header";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            using(MySqlDataReader reader= cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    uint id = reader.GetUInt32(0);
                    string featureTypeStr = reader.GetString(1);
                    string name = reader.GetString(2);
                    MLRecord curRecord = new MLRecord(id, name, featureTypeStr);
                    records.Add(curRecord);
                }
            }
        }


    }
}
