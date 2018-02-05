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
    /// Построитель геометрической информации (PathGeometry) о линиях графика на основе двумерного графика
    /// (необходимо для рендеринга (вывода на экран))
    /// </summary>
    public static class GeometryLinesConstructor
    {
        /// <summary>
        /// Построение геометрической информации
        /// </summary>
        /// <param name="srcGraphic">исходный двумерный график</param>
        /// <param name="rect">область вывода на экране</param>
        /// <returns>геометрическая информация</returns>
        public static PathGeometry ConstructGeometry(GeometryGraphic srcGraphic, Rect rect)
        {
            PathGeometry LineColl = new PathGeometry();
            int countPoints = srcGraphic.Points.Count;
            if (countPoints < 2)
                return LineColl;
            for (int i = 0; i < countPoints - 1; i++)
            {
                LineGeometry line = new LineGeometry(srcGraphic.Points[i].Position, new Point(srcGraphic.Points[i + 1].X, srcGraphic.Points[i].Y));
                LineColl.AddGeometry(line);
                if (!srcGraphic.Points[i].Y.Equals(srcGraphic.Points[i + 1].Y))
                {
                    LineGeometry lineV = new LineGeometry(new Point(srcGraphic.Points[i + 1].X, srcGraphic.Points[i].Y), new Point(srcGraphic.Points[i + 1].X, srcGraphic.Points[i + 1].Y));
                    LineColl.AddGeometry(lineV);
                }
            }
            return LineColl;
        }
    }
}
