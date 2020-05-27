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

        public void AddPoint(PointD point)
        {
            points.Add(point);
        }

        public bool IsClose()
        {
            return points.Count >= 3 && points[0] == points[points.Count - 1];
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
    }

    /// <summary>
    /// 麻辣精灵要素抽象基类
    /// </summary>
    public abstract class MLFeature
    {
        #region 属性
        //protected int id;                     //要素编号
        protected FeatureType featureType;    //要素类型
        protected double[] mbr;               //要素最小外包矩形，xmin，xmax，ymin，ymax
        protected int pointNum;               //要素包含点的数量
        protected PointF deviation;           //要素在屏幕绘制的偏移值
        #endregion

        //static int count=0;

        public MLFeature()
        {
            //id = ++count;   //id自动+1
            mbr = new double[4];
            deviation = new PointF();
        }


        //用于剪切，复制，粘贴等操作；结果为shp文件中要素记录字段，去掉编号和字段长度，见P8-P9记录格式
        public abstract byte[] ToBytes();


        public void Move(float x, float y)
        {
            deviation.X += x;
            deviation.Y += y;
        }

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
        FeatureType featureType;    //要素类类型，其中的每个要素集合类型均一致
        double[] mbr;               //要素类最小外包矩形
        List<MLFeature> features;       //要素数组
        DataTable attributeData;    //要素属性表
        PointF deviation;           //要素类整体偏移
        #endregion

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

        //public void AddAttributeRow(object[] values)
        //{
        //    attributeData.BeginLoadData();
        //    object[] curValues = new object[values.Length - 1];
        //    curValues[0] = values[0];
        //    curValues[1] = featureType;
        //    Array.Copy(values, 3, curValues, 2, values.Length - 3);
        //    attributeData.LoadDataRow(curValues, true);
        //    attributeData.EndLoadData();
        //}

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
        public void AddFeaure(MLFeature curFea, object[] values)
        {
            features.Add(curFea);
            attributeData.BeginLoadData();
            object[] curValues = new object[values.Length - 1];
            curValues[0] = values[0];
            curValues[1] = featureType;
            Array.Copy(values, 3, curValues, 2, values.Length - 3);
            attributeData.LoadDataRow(curValues, true);
            attributeData.EndLoadData();
        }

        public MLFeature GetFeature(int index)
        {
            return features[index];
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
    }

    /// <summary>
    /// 麻辣精灵点要素
    /// </summary>
    public class MLPoint : MLFeature
    {
        PointD point;

        public MLPoint(PointD _p) : base()//TODO: 应该是函数体内进行sql查询，直接获取数据进行构造
        {
            point = new PointD(_p.X, _p.Y);
            featureType = FeatureType.POINT;
            mbr[0] = mbr[1] = point.X;
            mbr[2] = mbr[3] = point.Y;
            pointNum = 1;
            //TODO: FillDataTable
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

        public override byte[] ToBytes()
        {
            byte[] rslt;
            using(MemoryStream ms=new MemoryStream())
            {
                using(BinaryWriter bw=new BinaryWriter(ms))
                {
                    bw.Write(1);
                    bw.Write(point.X);
                    bw.Write(point.Y);
                }
                rslt = ms.ToArray();
            }
            return rslt;
        }
    }

    /// <summary>
    /// 麻辣精灵多段线要素
    /// </summary>
    public class MLPolyline : MLFeature
    {
        PolylineD[] segments;

        /// <summary>
        /// 从shp初始化要素
        /// </summary>
        /// <param name="biReader"></param>
        public MLPolyline(BinaryReader biReader) :base()
        {
            biReader.BaseStream.Seek(12, SeekOrigin.Current);
            mbr[0] = biReader.ReadDouble();
            mbr[2] = biReader.ReadDouble();
            mbr[1] = biReader.ReadDouble();
            mbr[3] = biReader.ReadDouble();
            int partNum = biReader.ReadInt32();
            pointNum = biReader.ReadInt32();
            segments = new PolylineD[partNum];

            //将part数组后面多加一个元素pointNum，方便计算每个part的长度
            int[] parts = new int[partNum+1];
            for(int i = 0; i < partNum; ++i)
            {
                parts[i] = biReader.ReadInt32();
            }
            parts[partNum] = pointNum;

            double x, y;
            PointD[] segPoints;
            for (int i = 0; i < partNum; ++i)
            {
                segPoints = new PointD[parts[i + 1] - parts[i]];
                for(int j = 0; j < parts[i + 1] - parts[i]; ++j)
                {
                    x = biReader.ReadDouble();
                    y = biReader.ReadDouble();
                    segPoints[j] = new PointD(x, y);
                }
                segments[i] = new PolylineD(segPoints);
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
                    for(int i = 0; i < segments.Length; ++i)
                    {
                        bw.Write(curSum);
                        curSum += segments[i].Count;
                    }
                    for(int i = 0; i < segments.Length; ++i)
                    {
                        for(int j = 0; j < segments[i].Count; ++j)
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
    }

    public class MLPolygon : MLFeature
    {
        PolygonD[] polygons;

        public MLPolygon(BinaryReader biReader)
        {
            throw new NotImplementedException();
        }

        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

    public class MLMultiPoint : MLFeature
    {
        PointD[] points;

        public MLMultiPoint(BinaryReader biReader)
        {
            throw new NotImplementedException();
        }
        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}