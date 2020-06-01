using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;

namespace MalaSpiritGIS
{
    /// <summary>
    /// 要素类型枚举，点，多段线，多边形，多点
    /// </summary>
    public enum FeatureType { POINT, POLYLINE, POLYGON, MULTIPOINT }

    /// <summary>
    /// double类型点坐标
    /// </summary>
    public class PointD
    {
        private double x, y;

        #region 构造函数
        /// <summary>
        /// 基于坐标生成点
        /// </summary>
        /// <param name="_x">横坐标</param>
        /// <param name="_y">纵坐标</param>
        public PointD(double _x, double _y)
        {
            x = _x;
            y = _y;
        }
        #endregion

        public double X { get { return x; } set { x = value; } }

        public double Y { get { return y; } set { y = value; } }

        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (obj.GetType().Equals(this.GetType()) == false) { return false; }
            PointD pt = (PointD)obj;
            return Math.Abs(x - pt.x) < Math.Abs(x + pt.x) * 1e-15 && Math.Abs(y - pt.y) < Math.Abs(y + pt.y) * 1e-15;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }
        public void Move(float _x,float _y)
        {
            x += _x;
            y += _y;
        }
    }

    /// <summary>
    /// 多段线
    /// </summary>
    public class PolylineD
    {
        List<PointD> points;

        #region 构造函数
        /// <summary>
        /// 基于点数组构造多段线
        /// </summary>
        /// <param name="_points"></param>
        public PolylineD(PointD[] _points)
        {
            points = new List<PointD>(_points);
        }
        #endregion

        public int Count { get { return points.Count; } }

        public PointD GetPoint(int index)
        {
            return points[index];
        }
        public PointD[] GetPoints()
        {
            PointD[] res = new PointD[points.Count];
            for(int i = 0;i != points.Count; ++i)
            {
                res[i] = points[i];
            }
            return res;
        }
        public void RemoveLastOne()
        {
            points.RemoveAt(points.Count - 1);
        }
        public void AddPoint(PointD point)
        {
            points.Add(point);
        }

        public bool IsClose()
        {
            return points.Count >= 3 && points[0] == points[points.Count - 1];
        }
        public void Move(float x,float y)
        {
            for (int i = 0; i != points.Count; ++i)
                points[i].Move(x, y);
        }
    }

    /// <summary>
    /// 多边形
    /// </summary>
    public class PolygonD
    {
        List<PolylineD> rings;

        #region 构造函数
        /// <summary>
        /// 基于多环构造多边形
        /// </summary>
        /// <param name="_rings"></param>
        public PolygonD(PolylineD[] _rings)
        {
            rings = new List<PolylineD>(_rings);
        }
        #endregion

        public int Count { get { return rings.Count; } }

        public PolylineD GetRing(int index)
        {
            return rings[index];
        }

        public void AddRing(PolylineD ring)
        {
            rings.Add(ring);
        }
        public void Move(float x,float y)
        {
            for (int i = 0; i != rings.Count; ++i)
                rings[i].Move(x, y);
        }
    }

    /// <summary>
    /// 麻辣精灵要素抽象基类
    /// </summary>
    public abstract class MLFeature
    {
        #region 属性
        protected int id;                     //要素编号
        protected FeatureType featureType;    //要素类型
        protected double[] mbr;               //要素最小外包矩形，xmin，xmax，ymin，ymax
        protected int pointNum;               //要素包含点的数量
        #endregion

        static int count=0;
        public FeatureType FeatureType { get { return featureType; } }
        public MLFeature()
        {
            id = count++;   //id自动+1
            mbr = new double[4];
        }
        public int ID { get { return id; } }

        //用于剪切，复制，粘贴等操作；结果为shp文件中要素记录字段，去掉编号和字段长度，见P8-P9记录格式
        public abstract byte[] ToBytes();


        public virtual void Move(float x, float y) { }

        public double XMin { get { return mbr[0]; } }
        public double XMax { get { return mbr[1]; } }
        public double YMin { get { return mbr[2]; } }
        public double YMax { get { return mbr[3]; } }
    }

    /// <summary>
    /// 麻辣精灵要素类，与图层一对一，包含多个要素
    /// </summary>
    public class MLFeatureClass
    {
        #region 属性
        uint id;                    //要素类编号
        string name;                //要素类名称
        public FeatureType featureType;    //要素类类型，其中的每个要素集合类型均一致
        double[] mbr;               //要素类最小外包矩形
        List<MLFeature> features;       //要素数组
        DataTable attributeData;    //要素属性表
        #endregion

        
        /// <summary>
        /// 界面新建图层时使用
        /// </summary>
        /// <param name="fp">要素处理器对象</param>
        /// <param name="_name">图层名称</param>
        /// <param name="_type">图层类型</param>
        public MLFeatureClass(MLFeatureProcessor fp, string _name, FeatureType _type)
        {
            id = fp.NextFeaClassId;//自动根据当前数据库状态生成一个合理的id，所有id的最大值+1
            name = _name;
            featureType = _type;
            features = new List<MLFeature>();
            attributeData = new DataTable();
            attributeData.Columns.Add("ID", typeof(uint));
            attributeData.Columns.Add("Type", typeof(FeatureType));
        }

        /// <summary>
        /// 从数据库读取要素类时使用
        /// </summary>
        /// <param name="_id">数据库中记录id</param>
        /// <param name="_name">数据库中记录名称</param>
        /// <param name="_type"></param>
        /// <param name="_mbr"></param>
        public MLFeatureClass(uint _id, string _name, FeatureType _type, double[] _mbr)
        {
            id = _id;
            name = _name;
            featureType = _type;
            mbr = new double[4];
            Array.Copy(_mbr, mbr, 4);
            features = new List<MLFeature>();
            attributeData = new DataTable();
        }

        public void AddAttributeField(string fieldName, Type fieldType)
        {
            attributeData.Columns.Add(fieldName, fieldType);
        }


        public string GetFieldName(int index)
        {
            return attributeData.Columns[index].ColumnName;
        }

        public Type GetFieldType(int index)
        {
            return attributeData.Columns[index].DataType;
        }

        public object GetAttributeCell(int rowIndex, int colIndex)
        {
            return attributeData.Rows[rowIndex][colIndex];
        }

        /// <summary>
        /// 向要素类中添加要素
        /// </summary>
        /// <param name="curFea">要素</param>
        /// <param name="values">要素对应的属性信息</param>
        public void AddFeaure(MLFeature curFea, object[] values=null)
        {
            //更新要素列表
            features.Add(curFea);
            //更新mbr
            if (mbr != null)//features已经存在
            {
                mbr[0] = Math.Min(mbr[0], curFea.XMin);
                mbr[1] = Math.Max(mbr[1], curFea.XMax);
                mbr[2] = Math.Min(mbr[2], curFea.YMin);
                mbr[3] = Math.Max(mbr[3], curFea.YMax);
            }
            else//add第一个feature时
            {
                mbr = new double[4];
                mbr[0] = curFea.XMin;
                mbr[1] = curFea.XMax;
                mbr[2] = curFea.YMin;
                mbr[3] = curFea.YMax;
            }

            //更新属性表
            attributeData.BeginLoadData();
            if (values!=null)
            {
                object[] curValues = new object[values.Length - 1];
                curValues[0] = values[0];
                curValues[1] = featureType;
                Array.Copy(values, 3, curValues, 2, values.Length - 3);
                attributeData.LoadDataRow(curValues, true);
            }
            else
            {
                object[] curValues = new object[attributeData.Columns.Count];
                curValues[0] = (uint)attributeData.Rows.Count;
                curValues[1] = featureType;
                attributeData.LoadDataRow(curValues, true);
            }
            
            attributeData.EndLoadData();
        }
        public void RemoveFeaure(int index)
        {
            //更新要素列表
            features.RemoveAt(index);
            if(features.Count != 0)
            {
                mbr[0] = features[0].XMin;
                mbr[1] = features[0].XMax;
                mbr[2] = features[0].YMin;
                mbr[3] = features[0].YMax;
                //更新mbr
                for (int i = 1; i != features.Count; ++i)//add第一个feature时
                {
                    mbr[0] = Math.Min(mbr[0], features[i].XMin);
                    mbr[1] = Math.Max(mbr[1], features[i].XMax);
                    mbr[2] = Math.Min(mbr[2], features[i].YMin);
                    mbr[3] = Math.Max(mbr[3], features[i].YMax);
                }
            }
            else
            {
                mbr = null;
            }
            attributeData.Rows.RemoveAt(index);
        }
        public void RemoveFeaure(MLFeature feature)
        {
            //更新要素列表
            for(int i = 0;i != features.Count; ++i)
            {
                if (feature.ID == features[i].ID)
                {
                    RemoveFeaure(i);
                    return;
                }
            }
        }

        public MLFeature GetFeature(int index)
        {
            return features[index];
        }

        public void EditName(string _name)
        {
            name = _name;
        }

        public double XMin { get { return mbr[0]; } }
        public double XMax { get { return mbr[1]; } }
        public double YMin { get { return mbr[2]; } }
        public double YMax { get { return mbr[3]; } }
        public uint ID { get { return id; } }
        public string Name { get { return name; } }
        public FeatureType Type { get { return featureType; } }
        public int Count { get { return features.Count; } }
        public int FieldCount { get { return attributeData.Columns.Count; } }
        public DataTable AttributeData { get { return attributeData; } }
    }

    /// <summary>
    /// 麻辣精灵点要素
    /// </summary>
    public class MLPoint : MLFeature
    {
        PointD point;

        public PointD Point { get { return point; } }

        public MLPoint(double x, double y) : base()
        {
            point = new PointD(x, y);
            featureType = FeatureType.POINT;
            mbr[0] = mbr[1] = x;
            mbr[2] = mbr[3] = y;
            pointNum = 1;

        }

        public MLPoint(PointD _p) : base()
        {
            point = new PointD(_p.X, _p.Y);
            featureType = FeatureType.POINT;
            mbr[0] = mbr[1] = point.X;
            mbr[2] = mbr[3] = point.Y;
            pointNum = 1;
        }

        /// <summary>
        /// 从Shp文件字段中初始化要素
        /// </summary>
        /// <param name="biReader">二进制文件读取对象</param>
        public MLPoint(BinaryReader biReader) : base()
        {
            //调用时读取器位于字段开头
            biReader.BaseStream.Seek(12, SeekOrigin.Current);
            double x, y;
            x = biReader.ReadDouble();
            y = biReader.ReadDouble();
            point = new PointD(x, y);
            featureType = FeatureType.POINT;
            mbr[0] = mbr[1] = point.X;
            mbr[2] = mbr[3] = point.Y;
            pointNum = 1;
        }

        public MLPoint(byte[] feaContent) : base()
        {
            using(MemoryStream ms=new MemoryStream(feaContent))
            {
                using(BinaryReader br=new BinaryReader(ms))
                {
                    br.BaseStream.Seek(4, SeekOrigin.Current);
                    double x, y;
                    x = br.ReadDouble();
                    y = br.ReadDouble();
                    point = new PointD(x, y);
                    featureType = FeatureType.POINT;
                    mbr[0] = mbr[1] = point.X;
                    mbr[2] = mbr[3] = point.Y;
                    pointNum = 1;
                }
            }
        }

        public override byte[] ToBytes()
        {
            byte[] rslt;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(1);
                    bw.Write(point.X);
                    bw.Write(point.Y);
                }
                rslt = ms.ToArray();
            }
            return rslt;
        }

        public override void Move(float x,float y)
        {
            point.Move(x, y);
        }
    }

    /// <summary>
    /// 麻辣精灵多段线要素
    /// </summary>
    public class MLPolyline : MLFeature
    {
        PolylineD[] segments;
        public PolylineD[] Segments { get { return segments; } }

        public MLPolyline(PointD[] points)
        {
            PolylineD segment = new PolylineD(points);
            featureType = FeatureType.POLYLINE;
            mbr[0] = mbr[1] = points[0].X;
            mbr[2] = mbr[3] = points[0].Y;
            for (int i = 1; i < points.Length; ++i)
            {
                if (points[i].X < mbr[0]) mbr[0] = points[i].X;
                if (points[i].X > mbr[1]) mbr[1] = points[i].X;
                if (points[i].Y < mbr[2]) mbr[2] = points[i].Y;
                if (points[i].Y > mbr[3]) mbr[3] = points[i].Y;
            }
            pointNum = points.Length;
            segments = new PolylineD[] { segment };
        }

        public MLPolyline(PolylineD segment)
        {
            featureType = FeatureType.POLYLINE;
            mbr[0] = mbr[1] = segment.GetPoint(0).X;
            mbr[2] = mbr[3] = segment.GetPoint(0).Y;
            for (int i = 1; i < segment.Count; ++i)
            {
                if (segment.GetPoint(i).X < mbr[0]) mbr[0] = segment.GetPoint(i).X;
                if (segment.GetPoint(i).X > mbr[1]) mbr[1] = segment.GetPoint(i).X;
                if (segment.GetPoint(i).Y < mbr[2]) mbr[2] = segment.GetPoint(i).Y;
                if (segment.GetPoint(i).Y > mbr[3]) mbr[3] = segment.GetPoint(i).Y;
            }
            pointNum = segment.Count;
            segments = new PolylineD[] { segment };
        }

        /// <summary>
        /// 从shp初始化要素
        /// </summary>
        /// <param name="biReader"></param>
        public MLPolyline(BinaryReader biReader) : base()
        {
            featureType = FeatureType.POLYLINE;
            biReader.BaseStream.Seek(12, SeekOrigin.Current);
            mbr[0] = biReader.ReadDouble();
            mbr[2] = biReader.ReadDouble();
            mbr[1] = biReader.ReadDouble();
            mbr[3] = biReader.ReadDouble();
            int partNum = biReader.ReadInt32();
            pointNum = biReader.ReadInt32();
            segments = new PolylineD[partNum];

            //将part数组后面多加一个元素pointNum，方便计算每个part的长度
            int[] parts = new int[partNum + 1];
            for (int i = 0; i < partNum; ++i)
            {
                parts[i] = biReader.ReadInt32();
            }
            parts[partNum] = pointNum;

            double x, y;
            PointD[] segPoints;
            for (int i = 0; i < partNum; ++i)
            {
                segPoints = new PointD[parts[i + 1] - parts[i]];
                for (int j = 0; j < parts[i + 1] - parts[i]; ++j)
                {
                    x = biReader.ReadDouble();
                    y = biReader.ReadDouble();
                    segPoints[j] = new PointD(x, y);
                }
                segments[i] = new PolylineD(segPoints);
            }
        }

        public MLPolyline(byte[] feaContent) : base()
        {
            using (MemoryStream ms = new MemoryStream(feaContent))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    featureType = FeatureType.POLYLINE;
                    br.BaseStream.Seek(4, SeekOrigin.Current);
                    mbr[0] = br.ReadDouble();
                    mbr[2] = br.ReadDouble();
                    mbr[1] = br.ReadDouble();
                    mbr[3] = br.ReadDouble();
                    int partNum = br.ReadInt32();
                    pointNum = br.ReadInt32();
                    segments = new PolylineD[partNum];

                    //将part数组后面多加一个元素pointNum，方便计算每个part的长度
                    int[] parts = new int[partNum + 1];
                    for (int i = 0; i < partNum; ++i)
                    {
                        parts[i] = br.ReadInt32();
                    }
                    parts[partNum] = pointNum;

                    double x, y;
                    PointD[] segPoints;
                    for (int i = 0; i < partNum; ++i)
                    {
                        segPoints = new PointD[parts[i + 1] - parts[i]];
                        for (int j = 0; j < parts[i + 1] - parts[i]; ++j)
                        {
                            x = br.ReadDouble();
                            y = br.ReadDouble();
                            segPoints[j] = new PointD(x, y);
                        }
                        segments[i] = new PolylineD(segPoints);
                    }
                }
            }
        }
        public override byte[] ToBytes()
        {
            byte[] rslt;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(3);
                    bw.Write(mbr[0]);//xmin
                    bw.Write(mbr[2]);//ymin
                    bw.Write(mbr[1]);//xmax
                    bw.Write(mbr[3]);//ymax
                    bw.Write(segments.Length);
                    bw.Write(pointNum);
                    int curSum = 0;
                    for (int i = 0; i < segments.Length; ++i)
                    {
                        bw.Write(curSum);
                        curSum += segments[i].Count;
                    }
                    for (int i = 0; i < segments.Length; ++i)
                    {
                        for (int j = 0; j < segments[i].Count; ++j)
                        {
                            bw.Write(segments[i].GetPoint(j).X);
                            bw.Write(segments[i].GetPoint(j).Y);
                        }
                    }
                }
                rslt = ms.ToArray();
            }
            return rslt;
        }
        public override void Move(float x, float y)
        {
            for (int i = 0; i != segments.Length; ++i)
                segments[i].Move(x, y);
        }
        public void AddLine(PolylineD pd)
        {
            PolylineD[] ori = segments;
            segments = new PolylineD[ori.Length + 1];
            for (int i = 0; i != ori.Length; ++i)
                segments[i] = ori[i];
            segments[ori.Length] = pd;
        }
    }

    public class MLPolygon : MLFeature
    {
        PolygonD polygon;

        public PolygonD Polygon { get { return polygon; } }

        /// <summary>
        /// 由于多边形创建要求环闭合，需要在调用构造函数前进行检查，故不具备用点数组作为集合的构造函数
        /// </summary>
        /// <param name="ring">单独外环</param>
        public MLPolygon(PolylineD ring)
        {
            featureType = FeatureType.POLYGON;
            mbr[0] = mbr[1] = ring.GetPoint(0).X;
            mbr[2] = mbr[3] = ring.GetPoint(0).Y;
            for (int i = 1; i < ring.Count; ++i)
            {
                if (ring.GetPoint(i).X < mbr[0]) mbr[0] = ring.GetPoint(i).X;
                if (ring.GetPoint(i).X > mbr[1]) mbr[1] = ring.GetPoint(i).X;
                if (ring.GetPoint(i).Y < mbr[2]) mbr[2] = ring.GetPoint(i).Y;
                if (ring.GetPoint(i).Y > mbr[3]) mbr[3] = ring.GetPoint(i).Y;
            }
            pointNum = ring.Count;
            polygon = new PolygonD(new PolylineD[] { ring });
        }

        public MLPolygon(BinaryReader biReader)
        {
            featureType = FeatureType.POLYGON;
            biReader.BaseStream.Seek(12, SeekOrigin.Current);
            mbr[0] = biReader.ReadDouble();
            mbr[2] = biReader.ReadDouble();
            mbr[1] = biReader.ReadDouble();
            mbr[3] = biReader.ReadDouble();
            int partNum = biReader.ReadInt32();
            pointNum = biReader.ReadInt32();
            PolylineD[] rings = new PolylineD[partNum];

            //将part数组后面多加一个元素pointNum，方便计算每个part的长度
            int[] parts = new int[partNum + 1];
            for (int i = 0; i < partNum; ++i)
            {
                parts[i] = biReader.ReadInt32();
            }
            parts[partNum] = pointNum;

            double x, y;
            PointD[] segPoints;
            for (int i = 0; i < partNum; ++i)
            {
                segPoints = new PointD[parts[i + 1] - parts[i]];
                for (int j = 0; j < parts[i + 1] - parts[i]; ++j)
                {
                    x = biReader.ReadDouble();
                    y = biReader.ReadDouble();
                    segPoints[j] = new PointD(x, y);
                }
                rings[i] = new PolylineD(segPoints);
            }

            polygon = new PolygonD(rings);
        }

        public MLPolygon(byte[] feaContent) : base()
        {
            using (MemoryStream ms = new MemoryStream(feaContent))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    featureType = FeatureType.POLYGON;
                    br.BaseStream.Seek(4, SeekOrigin.Current);
                    mbr[0] = br.ReadDouble();
                    mbr[2] = br.ReadDouble();
                    mbr[1] = br.ReadDouble();
                    mbr[3] = br.ReadDouble();
                    int partNum = br.ReadInt32();
                    pointNum = br.ReadInt32();
                    PolylineD[] rings = new PolylineD[partNum];

                    //将part数组后面多加一个元素pointNum，方便计算每个part的长度
                    int[] parts = new int[partNum + 1];
                    for (int i = 0; i < partNum; ++i)
                    {
                        parts[i] = br.ReadInt32();
                    }
                    parts[partNum] = pointNum;

                    double x, y;
                    PointD[] segPoints;
                    for (int i = 0; i < partNum; ++i)
                    {
                        segPoints = new PointD[parts[i + 1] - parts[i]];
                        for (int j = 0; j < parts[i + 1] - parts[i]; ++j)
                        {
                            x = br.ReadDouble();
                            y = br.ReadDouble();
                            segPoints[j] = new PointD(x, y);
                        }
                        rings[i] = new PolylineD(segPoints);
                    }

                    polygon = new PolygonD(rings);
                }
            }
        }

        public override byte[] ToBytes()
        {
            byte[] rslt;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(5);
                    bw.Write(mbr[0]);//xmin
                    bw.Write(mbr[2]);//ymin
                    bw.Write(mbr[1]);//xmax
                    bw.Write(mbr[3]);//ymax
                    bw.Write(polygon.Count);
                    bw.Write(pointNum);
                    int curSum = 0;
                    for (int i = 0; i < polygon.Count; ++i)
                    {
                        bw.Write(curSum);
                        curSum += polygon.GetRing(i).Count;
                    }
                    for (int i = 0; i < polygon.Count; ++i)
                    {
                        for (int j = 0; j < polygon.GetRing(i).Count; ++j)
                        {
                            bw.Write(polygon.GetRing(i).GetPoint(j).X);
                            bw.Write(polygon.GetRing(i).GetPoint(j).Y);
                        }
                    }
                }
                rslt = ms.ToArray();
            }
            return rslt;
        }

        public override void Move(float x, float y)
        {
            polygon.Move(x, y);
        }
        public void AddPolygon(PolylineD pd)
        {
            polygon.AddRing(pd);
        }
    }

    public class MLMultiPoint : MLFeature
    {
        PointD[] points;
        public PointD[] Points { get { return points; } }

        public MLMultiPoint(PointD[] _points)
        {
            featureType = FeatureType.MULTIPOINT;
            points = new PointD[_points.Length];
            Array.Copy(_points, points, _points.Length);
            mbr[0] = mbr[1] = points[0].X;
            mbr[2] = mbr[3] = points[0].Y;
            for (int i = 1; i < points.Length; ++i)
            {
                if (points[i].X < mbr[0]) mbr[0] = points[i].X;
                if (points[i].X > mbr[1]) mbr[1] = points[i].X;
                if (points[i].Y < mbr[2]) mbr[2] = points[i].Y;
                if (points[i].Y > mbr[3]) mbr[3] = points[i].Y;
            }
            pointNum = points.Length;
        }
        public MLMultiPoint(BinaryReader biReader)
        {
            featureType = FeatureType.MULTIPOINT;
            biReader.BaseStream.Seek(12, SeekOrigin.Current);
            mbr[0] = biReader.ReadDouble();
            mbr[2] = biReader.ReadDouble();
            mbr[1] = biReader.ReadDouble();
            mbr[3] = biReader.ReadDouble();
            pointNum = biReader.ReadInt32();
            points = new PointD[pointNum];
            double x, y;
            for (int i = 0; i < pointNum; ++i)
            {
                x = biReader.ReadDouble();
                y = biReader.ReadDouble();
                points[i] = new PointD(x, y);
            }
        }

        public MLMultiPoint(byte[] feaContent) : base()
        {
            using (MemoryStream ms = new MemoryStream(feaContent))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    featureType = FeatureType.MULTIPOINT;
                    br.BaseStream.Seek(4, SeekOrigin.Current);
                    mbr[0] = br.ReadDouble();
                    mbr[2] = br.ReadDouble();
                    mbr[1] = br.ReadDouble();
                    mbr[3] = br.ReadDouble();
                    pointNum = br.ReadInt32();
                    points = new PointD[pointNum];
                    double x, y;
                    for (int i = 0; i < pointNum; ++i)
                    {
                        x = br.ReadDouble();
                        y = br.ReadDouble();
                        points[i] = new PointD(x, y);
                    }
                }
            }
        }

        public override byte[] ToBytes()
        {
            byte[] rslt;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(8);
                    bw.Write(mbr[0]);//xmin
                    bw.Write(mbr[2]);//ymin
                    bw.Write(mbr[1]);//xmax
                    bw.Write(mbr[3]);//ymax
                    bw.Write(pointNum);
                    for (int i = 0; i < points.Length; ++i)
                    {
                        bw.Write(points[i].X);
                        bw.Write(points[i].Y);
                    }
                }
                rslt = ms.ToArray();
            }
            return rslt;
        }

        public override void Move(float x, float y)
        {
            for (int i = 0; i != points.Length; ++i)
                points[i].Move(x, y);
        }
        public void AddPoint(PointD p)
        {
            PointD[] ori = points;
            points = new PointD[ori.Length + 1];
            for (int i = 0; i != ori.Length; ++i)
                points[i] = ori[i];
            points[ori.Length] = p;
        }
    }
}