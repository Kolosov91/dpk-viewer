using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ConstructGraphicLibrary.Data;
using System.Windows;

namespace ConstructGraphicLibrary.StaticConstructors.RenderedGeometry
{
    /// <summary>
    /// Построитель геометрической информации (PathGeometry) о точках графика на основе двумерного графика
    /// (необходимо для рендеринга (вывода на экран))
    /// </summary>
    public static class GeometryPointsConstructor
    {
        /// <summary>
        /// Построение геометрической информации
        /// </summary>
        /// <param name="srcGraphic">исходный двумерный график</param>
        /// <param name="rect">область вывода на экране</param>
        /// <returns>геометрическая информация</returns>
        public static PathGeometry ConstructGeometry(GeometryGraphic srcGraphic, Rect rect, TimeSpan leftTimeBorder, TimeSpan rightTimeBorder)
        {
            PathGeometry PtColl = new PathGeometry();
            if (srcGraphic.Points.Count == 0) return PtColl;
            foreach (GeometryPoint item in srcGraphic.Points)
            {
                if ((item.SourcePoint.Time.CompareTo(leftTimeBorder) < 0) || (item.SourcePoint.Time.CompareTo(rightTimeBorder) > 0))
                    continue;
                PtColl.AddGeometry(new EllipseGeometry(item.Position, 1, 1));
            }
            return PtColl;
        }
    }
}
