using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.Data;
using System.Windows;
using ConstructGraphicLibrary.BaseTools;

namespace ConstructGraphicLibrary.StaticConstructors
{
    /// <summary>
    /// Построитель графика в двумерном пространстве (x,y) на основе исходных данных (точки графика)
    /// </summary>
    public static class GeometryGraphicConstructor
    {
        static GeometryPoint GetGeometryPointFrom(SourcePoint srcPt, double val, Rect rect, double timeInPoint, TimeSpan leftTime, TimeSpan time)
        {
            Point curPosPoint = new Point(rect.Location.X + ToolFunctions.GetDxByTime(time, timeInPoint, leftTime), val);
            GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcPt);
            return curPoint;
        }
        public static GeometryGraphic ConstructWithApproximation(SourceGraphic srcGraphic, Rect rect, double timeInPoint, TimeSpan leftTime, TimeSpan rightTime, double accuracyApproximationPx, out bool isAppearApproximation)
        {
            GeometryGraphic geometryGraphic = new GeometryGraphic();
            isAppearApproximation = false;
            if (srcGraphic.Points.Count == 0) return geometryGraphic;
            double minV = rect.Location.Y + rect.Height;// -30;
            double maxV = rect.Location.Y;// +10;
            double valInPx = (minV - maxV) / srcGraphic.MaxValue;

            int countPoints = srcGraphic.Points.Count;

            if (srcGraphic.Points.Count < 5)
            {
                for (int i = 0; i < countPoints; i++)
                { geometryGraphic.Points.Add(GetGeometryPointFrom(srcGraphic.Points[i], (minV - srcGraphic.Points[i].Value * valInPx), rect, timeInPoint, leftTime, srcGraphic.Points[i].Time)); }
                    return geometryGraphic;
            }

            GeometryPoint prevPt = new GeometryPoint();

            for (int i = 0; i < countPoints; i++)
                if ((i < (countPoints - 1)) && (srcGraphic.Points[i].Time.CompareTo(leftTime) <= 0) && (srcGraphic.Points[i + 1].Time.CompareTo(leftTime) >= 0))
                {
                    Point curPosPoint = new Point(rect.Location.X + ToolFunctions.GetDxByTime(leftTime, timeInPoint, leftTime), (minV - srcGraphic.Points[i].Value * valInPx));
                    GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcGraphic.Points[i]);
                    geometryGraphic.Points.Add(curPoint);
                    prevPt = curPoint;
                }
                else if ((srcGraphic.Points[i].Time.CompareTo(leftTime) > 0) && (srcGraphic.Points[i].Time.CompareTo(rightTime) < 0))
                {
                    Point curPosPoint = new Point(rect.Location.X + ToolFunctions.GetDxByTime(srcGraphic.Points[i].Time, timeInPoint, leftTime), (minV - srcGraphic.Points[i].Value * valInPx));
                    GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcGraphic.Points[i]);

                    if (i == 1)
                    {
                        geometryGraphic.Points.Add(curPoint);
                        prevPt = curPoint;
                        continue;
                    }
                    if (i == (countPoints - 1 - 1))
                    {
                        geometryGraphic.Points.Add(curPoint);
                        prevPt = curPoint;
                        continue;
                    }

                    if ((curPoint.X - prevPt.X) < accuracyApproximationPx)
                    {
                        isAppearApproximation = true;
                        continue;
                    }
                    else
                    {
                        geometryGraphic.Points.Add(curPoint);
                        prevPt = curPoint;
                    }
                }
                else if (srcGraphic.Points[i].Time.CompareTo(rightTime) >= 0)
                {
                    Point curPosPoint = new Point(rect.Location.X + +ToolFunctions.GetDxByTime(rightTime, timeInPoint, leftTime), (minV - srcGraphic.Points[i].Value * valInPx));
                    GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcGraphic.Points[i]);
                    geometryGraphic.Points.Add(curPoint);
                    return geometryGraphic;
                }

            return geometryGraphic;
        }
        /*-------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Построить двумерный график
        /// </summary>
        /// <param name="srcGraphic">Исходные данные</param>
        /// <param name="rect">область отображения на экране</param>
        /// <param name="timeInPoint">коэффициент</param>
        /// <param name="leftTime">левая временная граница</param>
        /// <param name="rightTime">правая временная граница</param>
        /// <returns>двумерный график</returns>
        public static GeometryGraphic Construct(SourceGraphic srcGraphic, Rect rect, double timeInPoint, TimeSpan leftTime, TimeSpan rightTime)
        {
            GeometryGraphic geometryGraphic = new GeometryGraphic();
            if (srcGraphic.Points.Count == 0) return geometryGraphic;
            double minV = rect.Location.Y + rect.Height;// -30;
            double maxV = rect.Location.Y;// +10;
            double valInPx = (minV - maxV) / srcGraphic.MaxValue;
            int countPoints = srcGraphic.Points.Count;
            for (int i = 0; i < countPoints; i++)
                if ((i < (countPoints - 1)) && (srcGraphic.Points[i].Time.CompareTo(leftTime) <= 0) && (srcGraphic.Points[i + 1].Time.CompareTo(leftTime) >= 0))
                {
                    Point curPosPoint = new Point(rect.Location.X + ToolFunctions.GetDxByTime(leftTime, timeInPoint, leftTime), (minV - srcGraphic.Points[i].Value * valInPx));
                    GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcGraphic.Points[i]);
                    geometryGraphic.Points.Add(curPoint);
                }
                else if ((srcGraphic.Points[i].Time.CompareTo(leftTime) > 0) && (srcGraphic.Points[i].Time.CompareTo(rightTime) < 0))
                {
                    Point curPosPoint = new Point(rect.Location.X + ToolFunctions.GetDxByTime(srcGraphic.Points[i].Time, timeInPoint, leftTime), (minV - srcGraphic.Points[i].Value * valInPx));
                    GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcGraphic.Points[i]);
                    geometryGraphic.Points.Add(curPoint);
                }
                else if (srcGraphic.Points[i].Time.CompareTo(rightTime) >= 0)
                {
                    Point curPosPoint = new Point(rect.Location.X + +ToolFunctions.GetDxByTime(rightTime, timeInPoint, leftTime), (minV - srcGraphic.Points[i].Value * valInPx));
                    GeometryPoint curPoint = new GeometryPoint(curPosPoint, srcGraphic.Points[i]);
                    geometryGraphic.Points.Add(curPoint);
                    return geometryGraphic;
                }
            return geometryGraphic;
        }
        /// <summary>
        /// Аппроксимация двумерного графика 
        /// (необходимо, когда точки расположены очень близко друг к другу, т.е. человеческий глаз не отличит (5 пикселей))
        /// (это даёт увеличение производительности и сбережение памяти)
        /// </summary>
        /// <param name="geometryGraphic">исходный двумерный график</param>
        /// <param name="isAppearApproximation">флаг-признак наличия аппроксимации</param>
        /// /// <param name="accuracyApproximationPx">Коэффициент применения аппроксимации в px</param>
        /// <returns>аппроксимированный двумерный график</returns>
        public static GeometryGraphic Approximation(GeometryGraphic geometryGraphic, ref bool isAppearApproximation, double accuracyApproximationPx)
        {
            isAppearApproximation = false;
            GeometryGraphic resultGeometryGraphic = new GeometryGraphic();
            if (geometryGraphic.Points.Count < 30)
                return geometryGraphic;
            resultGeometryGraphic.Points.Add(geometryGraphic.Points[0]);
            resultGeometryGraphic.Points.Add(geometryGraphic.Points[1]);
            GeometryPoint prevPt = geometryGraphic.Points[1];
            int countPoints = geometryGraphic.Points.Count;
            for (int i = 2; i < countPoints - 1; i++)
            {
                if ((geometryGraphic.Points[i].X - prevPt.X) < accuracyApproximationPx)
                {
                    isAppearApproximation = true;
                    continue;
                }
                else
                {
                    resultGeometryGraphic.Points.Add(geometryGraphic.Points[i]);
                    prevPt = geometryGraphic.Points[i];
                }
            }
            resultGeometryGraphic.Points.Add(geometryGraphic.Points[countPoints - 1]);
            return resultGeometryGraphic;
        }
    }
}
