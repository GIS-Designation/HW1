using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Net;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace MalaSpiritGIS
{
    public class MLRecord
    {
        uint id;
        string name;
        FeatureType featureType;
        Label signLabel, nameLabel;
        public MLRecord(uint _id, string _name, string _typeStr)
        {
            id = _id;
            name = _name;
            signLabel = new Label();
            nameLabel = new Label();
            switch (_typeStr)
            {
                case "POINT":
                    featureType = FeatureType.POINT;
                    signLabel.Text = "·";
                    break;
                case "POLYLINE":
                    featureType = FeatureType.POLYLINE;
                    signLabel.Text = "—";
                    break;
                case "POLYGON":
                    featureType = FeatureType.POLYGON;
                    signLabel.Text = "■";
                    break;
                case "MULTIPOINT":
                    featureType = FeatureType.MULTIPOINT;
                    signLabel.Text = "·";
                    break;
            }
            nameLabel.Text = name;
        }

        public uint ID { get { return id; } }
        public string Name { get { return name; } }
        public FeatureType Type { get { return featureType; } }
        public Label SignLabel { get { return signLabel; } }
        public Label NameLabel { get { return nameLabel; } }
    }
    /// <summary>
    /// 在内存和数据库之间处理要素类
    /// </summary>
    public class MLFeatureProcessor
    {
        string server = "182.92.161.171";
        string database = "mala_spirit_gis_db";
        string userId = "root";
        string password = "Jzm19260817";
        string charset = "utf8";
        string port = "3306";
        public string defaultShpPath = @"..\\..\\..\\..\\HW1SHP\\";

        MySqlConnection connection;

        List<MLRecord> records;

        uint nextFeaClassId;

        public MLFeatureProcessor()
        {
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

            using (FileStream shp = new FileStream(shpFilePath, FileMode.Open, FileAccess.Read))
            {
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
                                    curFeature = new MLPolyline(br);
                                    break;
                                case FeatureType.POLYGON:
                                    curFeature = new MLPolygon(br);
                                    break;
                                case FeatureType.MULTIPOINT:
                                    curFeature = new MLMultiPoint(br);
                                    break;
                                default:
                                    curFeature = new MLPoint(br);//
                                    break;
                            }
                            curFeaClass.AddFeaure(curFeature, curRow);
                        }
                    }
                }
            }

            return curFeaClass;
        }

        public MLFeatureClass LoadFeatureClassFromShapefile(string filePath)
        {
            string shpName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
            shpName = shpName.Substring(0, shpName.IndexOf(".shp"));

            MLFeatureClass curFeaClass;
            int[] feaBias;
            int[] feaLength;
            using (FileStream shx = new FileStream(filePath.Substring(0, filePath.IndexOf(".shp")) + ".shx", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(shx))
                {
                    br.BaseStream.Seek(100, SeekOrigin.Begin);
                    long count = (br.BaseStream.Length - 100) / 8;
                    feaBias = new int[count];
                    feaLength = new int[count];
                    byte[] temp;
                    for (long i = 0; i < count; ++i)
                    {
                        temp = br.ReadBytes(8).Reverse().ToArray();
                        feaBias[i] = 2*BitConverter.ToInt32(temp,4);
                        feaLength[i] = 2*BitConverter.ToInt32(temp, 0);
                    }
                }
            }
            using (FileStream shp = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(shp))
                {
                    using (FileStream dbf = new FileStream(filePath.Substring(0, filePath.IndexOf(".shp")) + ".dbf", FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br_dbf = new BinaryReader(dbf))
                        {
                            br.BaseStream.Seek(32, SeekOrigin.Begin);
                            int typeInt = br.ReadInt32();
                            FeatureType fcType;
                            switch (typeInt)
                            {
                                case 1:
                                    fcType = FeatureType.POINT;
                                    break;
                                case 3:
                                    fcType = FeatureType.POLYLINE;
                                    break;
                                case 5:
                                    fcType = FeatureType.POLYGON;
                                    break;
                                case 8:
                                    fcType = FeatureType.MULTIPOINT;
                                    break;
                                default:
                                    fcType = FeatureType.POINT;
                                    break;
                            }
                            double[] mbr = new double[4];
                            mbr[0] = br.ReadDouble();
                            mbr[2] = br.ReadDouble();
                            mbr[1] = br.ReadDouble();
                            mbr[3] = br.ReadDouble();
                            curFeaClass = new MLFeatureClass(NextFeaClassId, shpName, fcType, mbr);
                            curFeaClass.AddAttributeField("ID", typeof(uint));
                            curFeaClass.AddAttributeField("Geometry", typeof(FeatureType));
                            br_dbf.BaseStream.Seek(4, SeekOrigin.Begin);
                            int rowCount = br_dbf.ReadInt32();
                            short firstRow = br_dbf.ReadInt16();
                            short rowLength = br_dbf.ReadInt16();
                            int attributeCount = (firstRow - 33) / 32;
                            byte[] fieldLength = new byte[attributeCount];
                            string fieldName;
                            Type[] fieldTypes;
                            fieldTypes = new Type[attributeCount];
                            char ch;
                            for (int i = 0; i < attributeCount; ++i)
                            {
                                br_dbf.BaseStream.Seek(i * 32 + 32, SeekOrigin.Begin);
                                fieldName = Encoding.UTF8.GetString(br_dbf.ReadBytes(11)).Trim('\0');
                                switch (ch=br_dbf.ReadChar())
                                {
                                    case 'I':
                                    case 'L':
                                        fieldTypes[i] = typeof(int);
                                        break;
                                    case 'N':
                                    case 'F':
                                    case 'B':
                                        fieldTypes[i] = typeof(double);
                                        break;
                                    default:
                                        fieldTypes[i] = typeof(string);
                                        break;
                                }
                                br_dbf.BaseStream.Seek(4, SeekOrigin.Current);
                                fieldLength[i] = br_dbf.ReadByte();
                                curFeaClass.AddAttributeField(fieldName, fieldTypes[i]);
                            }
                            br_dbf.BaseStream.Seek(firstRow, SeekOrigin.Begin);
                            for (int i = 0; i < rowCount; ++i)
                            {
                                br.BaseStream.Seek(feaBias[i], SeekOrigin.Begin);
                                MLFeature curFeature;
                                switch (fcType)
                                {
                                    case FeatureType.POINT:
                                        curFeature = new MLPoint(br);
                                        break;
                                    case FeatureType.POLYLINE:
                                        curFeature = new MLPolyline(br);
                                        break;
                                    case FeatureType.POLYGON:
                                        curFeature = new MLPolygon(br);
                                        break;
                                    case FeatureType.MULTIPOINT:
                                        curFeature = new MLMultiPoint(br);
                                        break;
                                    default:
                                        curFeature = new MLPoint(br);//
                                        break;
                                }
                                object[] values = new object[attributeCount + 3];
                                values[0] = (uint)i;
                                string temp;
                                br_dbf.BaseStream.Seek(1, SeekOrigin.Current);
                                for (int j = 0; j < attributeCount; ++j)
                                {
                                    temp = Encoding.GetEncoding("GBK").GetString(br_dbf.ReadBytes(fieldLength[j])).Trim((char)0x20);
                                    if (fieldTypes[j] == typeof(int))
                                    {
                                        if (temp.Equals("")) values[j + 2] = 0;
                                        else values[j + 3] = int.Parse(temp);
                                    }
                                    if (fieldTypes[j] == typeof(double))
                                    {
                                        if (temp.Equals("")) values[j + 2] = 0;
                                        else values[j + 3] = double.Parse(temp);
                                    }
                                    if (fieldTypes[j] == typeof(string))
                                    {
                                        values[j + 3] = temp;
                                    }
                                }
                                curFeaClass.AddFeaure(curFeature, values);
                            }
                        }
                    }

                }
            }
            return curFeaClass;
        }

        public void SaveFeatureClass(MLFeatureClass curFeaClass)
        {
            string shpPath;
            string sql = "select * from header where ID=" + curFeaClass.ID.ToString();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            bool toUpdate;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows)
                {
                    toUpdate = true;
                    shpPath = reader.GetString("FilePath");
                }
                else
                {
                    toUpdate = false;
                    shpPath = defaultShpPath + curFeaClass.ID.ToString() + ".shp";
                }
            }
            if (toUpdate)
            {
                //update
                sql = "update header set Name='" + curFeaClass.Name
                    + "',Count=" + curFeaClass.Count.ToString()
                    + ",Xmin=" + curFeaClass.XMin.ToString() + ",Xmax=" + curFeaClass.XMax.ToString()
                    + ",Ymin=" + curFeaClass.YMin.ToString() + ",Ymax=" + curFeaClass.YMax.ToString()
                    + " where ID=" + curFeaClass.ID.ToString();
                MySqlCommand update = new MySqlCommand(sql, connection);
                update.ExecuteNonQuery();
                sql = "drop table " + curFeaClass.ID.ToString();
                cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                //insert
                sql = "insert into header values(" + curFeaClass.ID.ToString() + ",'"
                    + curFeaClass.Type.ToString("") + "','" + curFeaClass.Name + "',"
                    + curFeaClass.Count.ToString() + ",'" + shpPath + "',"
                    + curFeaClass.XMin.ToString() + "," + curFeaClass.XMax.ToString() + ","
                    + curFeaClass.YMin.ToString() + "," + curFeaClass.YMax.ToString() + ")";
                MySqlCommand insert = new MySqlCommand(sql, connection);
                insert.ExecuteNonQuery();
            }

            sql = "create table `" + curFeaClass.ID.ToString() + "` (ID int,FileBias int,FileLength int";
            for (int i = 2; i < curFeaClass.FieldCount; ++i)
            {
                switch (curFeaClass.GetFieldType(i).Name)
                {
                    case "Int32":
                        sql += "," + curFeaClass.GetFieldName(i) + " int";
                        break;
                    case "Double":
                        sql += "," + curFeaClass.GetFieldName(i) + " double";
                        break;
                    default:
sql += "," + curFeaClass.GetFieldName(i) + " varchar(100)";
                        break;
                }
            }
            sql += ")";
            cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            using (FileStream shp=new FileStream(shpPath,FileMode.Create,FileAccess.Write))
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
                        string temp;
                        for (int j = 2; j < curFeaClass.FieldCount; ++j)
                        {
                            temp = curFeaClass.GetAttributeCell(i, j).ToString();
                            if (curFeaClass.GetFieldType(j)==typeof(string)) temp = "'"+temp+"'";
                            sql += "," + temp;
                        }
                        sql += ")";
                        MySqlCommand insert = new MySqlCommand(sql, connection);
                        insert.ExecuteNonQuery();
                    }
                }
            }
            RefreshRecords();
        }

        public uint NextFeaClassId { get { return ++nextFeaClassId; } }

        public List<MLRecord> Records { get { return records; } }

        public delegate void RecordsChanged();
        public event RecordsChanged RecordsChangedHandle;
        public void RefreshRecords()
        {
            nextFeaClassId = 0;
            string connstr = "server=" + server + ";database=" + database +
                ";uid=" + userId + ";password=" + password + ";charset=" + charset + ";port=" + port;
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
                    if (id > nextFeaClassId)
                    {
                        nextFeaClassId = id;
                    }
                }
            }
            RecordsChangedHandle?.Invoke();

        }

    }

    public class FtpHelper
    {
        //基本设置
        private static string ftppath = @"ftp://" + "brynhild.top" + "/";
        private static string username = "mlshp";
        private static string password = "mlshp";

        //获取FTP上面的文件夹和文件
        public static string[] GetFolderAndFileList(string s)
        {
            string[] getfolderandfilelist;
            FtpWebRequest request;
            StringBuilder sb = new StringBuilder();
            try
            {
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.UseBinary = true;
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    sb.Append(line);
                    sb.Append("\n");
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
                sb.Remove(sb.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return sb.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取FTP上面的文件夹和文件：" + ex.Message);
                getfolderandfilelist = null;
                return getfolderandfilelist;
            }
        }

        //获取FTP上面的文件大小
        public static int GetFileSize(string fileName)
        {
            FtpWebRequest request;
            try
            {
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath + fileName));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                int n = (int)request.GetResponse().ContentLength;
                return n;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取FTP上面的文件大小：" + ex.Message);
                return -1;
            }
        }

        //FTP上传文件
        public static void FileUpLoad(string filePath, string objPath = "")
        {
            try
            {
                string url = ftppath;
                if (objPath != "")
                    url += objPath + "/";
                try
                {
                    FtpWebRequest request = null;
                    try
                    {
                        FileInfo fi = new FileInfo(filePath);
                        using (FileStream fs = fi.OpenRead())
                        {
                            request = (FtpWebRequest)FtpWebRequest.Create(new Uri(url + fi.Name));
                            request.Credentials = new NetworkCredential(username, password);
                            request.KeepAlive = false;
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            request.UseBinary = true;
                            using (Stream stream = request.GetRequestStream())
                            {
                                int bufferLength = 5120;
                                byte[] buffer = new byte[bufferLength];
                                int i;
                                while ((i = fs.Read(buffer, 0, bufferLength)) > 0)
                                {
                                    stream.Write(buffer, 0, i);
                                }
                                MessageBox.Show("FTP上传文件succesful");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("FTP上传文件：" + ex.Message);
                    }
                    finally
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("FTP上传文件：" + ex.Message);
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FTP上传文件：" + ex.Message);
            }
        }

        public static void FileWrite(string filePath, byte[] fileContent)
        {
            try
            {
                string url = ftppath;
                try
                {
                    FtpWebRequest request = null;
                    try
                    {
                        FileInfo fi = new FileInfo(filePath);
                        request = (FtpWebRequest)FtpWebRequest.Create(new Uri(url + fi.Name));
                        request.Credentials = new NetworkCredential(username, password);
                        request.KeepAlive = false;
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.UseBinary = true;
                        using (Stream stream = request.GetRequestStream())
                        {
                            int bufferLength = 5120;
                            byte[] buffer = new byte[bufferLength];
                            int i;
                            using (MemoryStream ms = new MemoryStream(fileContent))
                            {
                                while ((i = ms.Read(buffer, 0, bufferLength)) > 0)
                                {
                                    stream.Write(buffer, 0, i);
                                }
                            }
                            MessageBox.Show("FTP写入文件succesful");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("FTP写入文件：" + ex.Message);
                    }
                    finally
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("FTP写入文件：" + ex.Message);
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FTP写入文件：" + ex.Message);
            }
        }

        //FTP下载文件 
        public static void FileDownLoad(string fileName)
        {
            FtpWebRequest request;
            try
            {
                string downloadPath = @"D:";
                FileStream fs = new FileStream(downloadPath + "\\" + fileName, FileMode.Create);
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath + fileName));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                int bufferLength = 5120;
                int i;
                byte[] buffer = new byte[bufferLength];
                i = stream.Read(buffer, 0, bufferLength);
                while (i > 0)
                {
                    fs.Write(buffer, 0, i);
                    i = stream.Read(buffer, 0, bufferLength);
                }
                stream.Close();
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP下载文件：" + ex.Message);
            }
        }

        //FTP删除文件
        public static void FileDelete(string fileName)
        {
            try
            {
                string uri = ftppath + fileName;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FTP删除文件：" + ex.Message);
            }
        }

        //FTP新建目录，上一级须先存在
        public static void MakeDir(string dirName)
        {
            try
            {
                string uri = ftppath + dirName;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP新建目录：" + ex.Message);
            }
        }

        //FTP删除目录，上一级须先存在
        public static void DelDir(string dirName)
        {
            try
            {
                string uri = ftppath + dirName;
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP删除目录：" + ex.Message);
            }
        }

        public static byte[] GetCloudFile(string fileName)
        {
            FtpWebRequest request;
            MemoryStream ms = new MemoryStream();
            try
            {
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath + fileName));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.KeepAlive = true;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                int bufferLength = 5120;
                int i;
                byte[] buffer = new byte[bufferLength];
                i = stream.Read(buffer, 0, bufferLength);
                while (i > 0)
                {
                    ms.Write(buffer, 0, i);
                    i = stream.Read(buffer, 0, bufferLength);
                }
                stream.Close();
                response.Close();
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FTP获取文件：" + ex.Message);
                return ms.ToArray();
            }
        }
    }
}

