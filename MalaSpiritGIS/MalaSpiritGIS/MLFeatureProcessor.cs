﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

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

        public uint ID { get { return id; } }
        public string Name { get { return name; } }
        public FeatureType Type { get { return featureType; } }
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

        /// <summary>
        /// 从数据库和文件中加载要素类至内存
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <returns></returns>
        public MLFeatureClass LoadFeatureClass(uint id)
        {
            string featureClassName="";
            FeatureType featureClassType=FeatureType.POINT;
            double[] featureClassMbr=new double[4];
            uint featureCount;
            string shpFilePath;
            foreach (MLRecord curRecord in records)
            {
                if (curRecord.ID == id)
                {
                    featureClassName = curRecord.Name;
                    featureClassType = curRecord.Type;
                    break;
                }
            }
            string sql = "select * from header where ID=" + id.ToString();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                featureCount = reader.GetUInt32("Count");
                featureClassMbr[0] = reader.GetDouble("Xmin");
                featureClassMbr[1] = reader.GetDouble("Xmax");
                featureClassMbr[2] = reader.GetDouble("Ymin");
                featureClassMbr[3] = reader.GetDouble("Ymax");
                shpFilePath = reader.GetString("FilePath");
            }
            MLFeatureClass curFeaClass = new MLFeatureClass(id,featureClassName, featureClassType, featureClassMbr);

            FileStream shp = new FileStream(shpFilePath, FileMode.Open, FileAccess.Read);
            using(BinaryReader br=new BinaryReader(shp))
            {
                sql = "select * from " + featureClassName;
                cmd = new MySqlCommand(sql, connection);
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    for (int i = 0; i < featureCount; ++i)
                    {
                        reader.Read();
                        br.BaseStream.Seek((long)reader.GetUInt32("FileBias"), SeekOrigin.Begin);
                        MLFeature curFeature;
                        switch (featureClassType)
                        {
                            case FeatureType.POINT:
                                curFeature = new MLPoint(br);
                                break;
                            case FeatureType.POLYLINE:
                            case FeatureType.POLYGON:
                            case FeatureType.MULTIPOINT:
                            default:
                                curFeature = new MLPoint(br);//
                                break;
                        }
                        curFeaClass.AddFeaure(curFeature);
                    }
                }
            }
            shp.Close();

            return curFeaClass;
        }

        public void SaveFeatureClass()
        {
            //TODO
        }
    }
}
