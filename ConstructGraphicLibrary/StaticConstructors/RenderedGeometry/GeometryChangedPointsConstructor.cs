using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ConstructGraphicLibrary.Data;
using System.Windows.Media;

namespace ConstructGraphicLibrary.StaticConstructors.RenderedGeometry
{
    /// <summary>
    /// Построитель геометрической информаци (PathGeometry) о изменяющихся точках на основе двумерного графика
    /// (необходимо для рендеринга (вывода на экран))
    /// </summary>
    public static class GeometryChangedPointsConstructor
    {
        /// <summary>
        /// Построение геометрической информации
        /// </summary>
        /// <param name="srcGraphic">исходный двумерный график</param>
        /// <param name="rect">область вывода на экране</param>
        /// <returns>геометрическиая информация</returns>
        public static PathGeometry ConstructGeometry(GeometryGraphic srcGraphic, Rect rect, TimeSpan leftTimeBorder, TimeSpan rightTimeBorder)
        {
            PathGeometry PtColl = new PathGeometry();
            if (srcGraphic.Points.Count == 0) return PtColl;
            GeometryPoint prevPt = srcGraphic.Points[0];
            if ((prevPt.SourcePoint.Time.CompareTo(leftTimeBorder) >= 0) && (prevPt.SourcePoint.Time.CompareTo(rightTimeBorder) <= 0))
                PtColl.AddGeometry(new EllipseGeometry(prevPt.Position, 1, 1));
            foreach (GeometryPoint item in srcGraphic.Points)
            {
                if (prevPt.Y != item.Y)
                {
                    if ((prevPt.SourcePoint.Time.CompareTo(leftTimeBorder) >= 0) && (prevPt.SourcePoint.Time.CompareTo(rightTimeBorder) <= 0))
                        PtColl.AddGeometry(new EllipseGeometry(item.Position, 1, 1));
                    prevPt = item;
                }
            }
            return PtColl;
        }
    }
}
