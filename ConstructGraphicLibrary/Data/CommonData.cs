using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.Data
{
    /// <summary>
    /// Общие данные, необходимые для рендеринга компонентов
    /// </summary>
    public class CommonData
    {
        /// <summary>
        /// Ссылка на исходные данные графика
        /// </summary>
        public List<SourceGraphic> SourceGraphics { get; set; }
        /// <summary>
        /// Активный график (рисуется поверх остальных)
        /// </summary>
        public int IndexActiveGraphic { get; set; }
        /// <summary>
        /// Ссылка на рендеренный набор двумерных графиков
        /// </summary>
        public List<GeometryGraphic> RenderedGraphics { get; protected set; }
        /// <summary>
        /// Коэффициент TimeInPoint
        /// </summary>
        public double TimeInPoint { get; set; }
        /// <summary>
        /// Левая временная граница видимого интервала
        /// </summary>
        public TimeSpan LeftTime { get; set; }
        /// <summary>
        /// Правая временная граница видимого интервала
        /// </summary>
        public TimeSpan RightTime { get; set; }
        /// <summary>
        /// отступ сверху (в пикселях)
        /// </summary>
        public double Width_UpBorder { get; set; }
        /// <summary>
        /// отступ снизу (в пикселях)
        /// </summary>
        public double Width_DownBorder { get; set; }
        /// <summary>
        /// отступ слева (в пикселях)
        /// </summary>
        public double Width_LeftBorder { get; set; }
        /// <summary>
        /// отступ справа (в пикселях)
        /// </summary>
        public double Width_RightBorder { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public CommonData()
        {
            SourceGraphics = new List<SourceGraphic>();
            IndexActiveGraphic = -1;
            RenderedGraphics = new List<GeometryGraphic>();

            TimeInPoint = 0;
            LeftTime = TimeSpan.Zero;
            RightTime = TimeSpan.Zero;

            Width_UpBorder = 0;
            Width_DownBorder = 0;
            Width_LeftBorder = 0;
            Width_RightBorder = 0;
        }
    }
}
