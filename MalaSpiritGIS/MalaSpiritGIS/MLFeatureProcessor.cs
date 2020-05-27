using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Collections;

namespace MalaSpiritGIS
{
    public class MLRecord
    {
        uint id;
        string name;
        FeatureType featureType;
        public MLRecord(uint _id, string _name, string _typeStr)
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
    public class MLFeatureProcessor
    {
        string server = "localhost";
        string database = "mala_spirit_gis_db";
        string userId = "root";
        string password = "";
        string charset = "utf8";
        string port = "3306";
        string defaultShpPath = @"C:\Users\Kuuhakuj\Documents\PKU\大三下\GIS设计和应用\HW1SHP\";

        MySqlConnection connection;

        List<MLRecord> records;

        public MLFeatureProcessor()
        {
            string connstr = "server=" + server + ";database=" + database +
                ";uid=" + userId + ";charset=" + charset + ";port=" + port;
            connection = new MySqlConnection(connstr);
            connection.Open();
            records = new List<MLRecord>();
            string sql = "select * from header";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
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
            string featureClassName = "";
            FeatureType featureClassType = FeatureType.POINT;
            double[] featureClassMbr = new double[4];
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
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                featureCount = reader.GetUInt32("Count");
                featureClassMbr[0] = reader.GetDouble("Xmin");
                featureClassMbr[1] = reader.GetDouble("Xmax");
                featureClassMbr[2] = reader.GetDouble("Ymin");
                featureClassMbr[3] = reader.GetDouble("Ymax");
                shpFilePath = reader.GetString("FilePath");
            }
            MLFeatureClass curFeaClass = new MLFeatureClass(id, featureClassName, featureClassType, featureClassMbr);

            FileStream shp = new FileStream(shpFilePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader br = new BinaryReader(shp))
            {
                sql = "select * from `" + id.ToString() + "`";
                cmd = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    curFeaClass.AddAttributeField("ID", typeof(uint));
                    curFeaClass.AddAttributeField("Geometry", typeof(FeatureType));
                    for (int i = 3; i < reader.FieldCount; ++i)
                    {
                        curFeaClass.AddAttributeField(reader.GetName(i), reader.GetFieldType(i));
                    }
                    for (int i = 0; i < featureCount; ++i)
                    {
                        reader.Read();
                        object[] curRow = new object[reader.FieldCount];
                        reader.GetValues(curRow);
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
                        curFeaClass.AddFeaure(curFeature, curRow);
                    }
                }
            }
            shp.Close();

            return curFeaClass;
        }

        public void SaveFeatureClass(MLFeatureClass curFeaClass)
        {
            string shpPath;
            string sql = "select * from header where ID=" + curFeaClass.ID.ToString();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows)
                {
                    //update
                    shpPath = reader.GetString("FilePath");
                    sql = "update header set Name='" + curFeaClass.Name
                        + "',Count=" + curFeaClass.Count.ToString()
                        + ",Xmin=" + curFeaClass.XMin.ToString() + ",Xmax=" + curFeaClass.XMax.ToString()
                        + ",Ymin=" + curFeaClass.YMin.ToString() + ",Ymax=" + curFeaClass.YMax.ToString()
                        + " where ID=" + curFeaClass.ID.ToString();
                    MySqlCommand update = new MySqlCommand(sql, connection);
                    update.ExecuteNonQuery();
                    sql = "drop table " + curFeaClass.ID.ToString();
                    update = new MySqlCommand(sql, connection);
                    update.ExecuteNonQuery();
                }
                else
                {
                    //insert
                    shpPath = defaultShpPath + curFeaClass.ID.ToString() + ".shp";
                    sql = "insert into header values(" + curFeaClass.ID.ToString() + ",'"
                        + curFeaClass.Type.ToString("") + "','" + curFeaClass.Name + "',"
                        + curFeaClass.Count.ToString() + ",'" + shpPath + "',"
                        + curFeaClass.XMin.ToString() + "," + curFeaClass.XMax.ToString() + ","
                        + curFeaClass.YMin.ToString() + "," + curFeaClass.YMax.ToString() + ")";
                    MySqlCommand insert = new MySqlCommand(sql, connection);
                    insert.ExecuteNonQuery();
                }
            }
            sql = "create table `" + curFeaClass.ID.ToString() + "` (ID int,FileBias int,FileLength int";
            for (int i = 2; i < curFeaClass.FieldCount; ++i)
            {
                sql += "," + curFeaClass.GetFieldName(i) + " " + curFeaClass.GetFieldType(i).Name;
            }
            sql += ")";
            cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            using (FileStream shp = new FileStream(shpPath, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(shp))
                {
                    int fileSize = 100;
                    bw.Write(BitConverter.GetBytes(curFeaClass.ID).Reverse().ToArray());
                    bw.Write(new byte[24]);
                    bw.Write(BitConverter.GetBytes(1000).Reverse().ToArray());
                    int[] typeCode = { 1, 3, 5, 8 };
                    bw.Write(typeCode[(int)curFeaClass.Type]);
                    bw.Write(curFeaClass.XMin);
                    bw.Write(curFeaClass.YMin);
                    bw.Write(curFeaClass.XMax);
                    bw.Write(curFeaClass.YMax);
                    bw.Write(new byte[32]);

                    for (int i = 0; i < curFeaClass.Count; ++i)
                    {
                        long bias = (int)bw.BaseStream.Position;
                        bw.Write(BitConverter.GetBytes(i).Reverse().ToArray());
                        byte[] featureBytes = curFeaClass.GetFeature(i).ToBytes();
                        bw.Write(BitConverter.GetBytes(featureBytes.Length).Reverse().ToArray());
                        bw.Write(featureBytes);
                        long length = bw.BaseStream.Position - bias;
                        sql = "insert into `" + curFeaClass.ID.ToString() + "` values("
                            + i.ToString() + "," + bias.ToString() + "," + length.ToString();
                        for (int j = 2; i < curFeaClass.FieldCount; ++i)
                        {
                            sql += "," + curFeaClass.GetAttributeCell(i, j).ToString();
                        }
                        sql += ")";
                        MySqlCommand insert = new MySqlCommand(sql, connection);
                        insert.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
