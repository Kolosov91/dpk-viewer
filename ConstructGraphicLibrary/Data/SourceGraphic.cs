using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.Data
{
    /// <summary>
    /// Универсальный График (список точек) (без информации о положении на экране)
    /// </summary>
    public class SourceGraphic
    {
        public string Name;
        /// <summary>
        /// Список точек построения графика
        /// </summary>
        public List<SourcePoint> Points;
        /// <summary>
        /// Максимальное значение в точке
        /// </summary>
        public int MaxValue;
        /// <summary>
        /// Конструктор
        /// </summary>
        public SourceGraphic() { Points = new List<SourcePoint>(); MaxValue = 0; Name = ""; }
        public override string ToString()
        {
            return Name.Replace('\n', ' ');
        }
    }
}
