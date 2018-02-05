using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.Data
{
    /// <summary>
    /// двумерный график (с информацией о положении на экране)
    /// </summary>
    public class GeometryGraphic
    {
        /// <summary>
        /// Список точек двумерного графика
        /// </summary>
        public List<GeometryPoint> Points;
        /// <summary>
        /// Конструктор
        /// </summary>
        public GeometryGraphic() { Points = new List<GeometryPoint>(); }
    }
}
