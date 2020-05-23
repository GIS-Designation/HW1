using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace MalaSpiritGIS
{
    /// <summary>
    /// 要素类型枚举，点，多段线，多边形，多点
    /// </summary>
    enum FeatureType { POINT,POLYLINE,POLYGON,MULTIPOINT}

    /// <summary>
    /// double类型点坐标
    /// </summary>
    class PointD
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
    class PolylineD
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
    class PolygonD
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
    abstract class MLFeature
    {
        #region 属性
        protected int id;                     //要素编号
        protected FeatureType featureType;    //要素类型
        protected double[] mbr;               //要素最小外包矩形，xmin，xmax，ymin，ymax
        protected int pointNum;               //要素包含点的数量
        protected DataTable attributeData;    //要素的属性表
        protected PointF deviation;           //要素在屏幕绘制的偏移值
        #endregion

        static int count=0;

        public MLFeature()
        {
            id = ++count;   //id自动+1
            mbr = new double[4];
            deviation = new PointF();
        }

        //用于剪切，复制，粘贴等操作
        public abstract byte[] ToBytes();

        public void Move(float x,float y)
        {
            deviation.X += x;
            deviation.Y += y;
        }
    }

    class MLPoint : MLFeature
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

        public override byte[] ToBytes()
        {
            byte[] rslt=new byte[1];
            return rslt;
        }
    }

}
