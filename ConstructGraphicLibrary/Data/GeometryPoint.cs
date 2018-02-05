using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ConstructGraphicLibrary.BaseTools;

namespace ConstructGraphicLibrary.Data
{
    /// <summary>
    /// Класс для построения графиков.
    /// Добавляет геометрическую составляющую [x,y] к SourcePoint
    /// </summary>
    public class GeometryPoint : ICloneObject<GeometryPoint>, ICopyObject<GeometryPoint>
    {
        /// <summary>
        /// позиция на экране
        /// </summary>
        Point position;
        /// <summary>
        /// Позиция на экране
        /// </summary>
        public Point Position { get { return position; } set { position = value; } }
        /// <summary>
        /// Координата Х позиции на экране
        /// </summary>
        public double X { get { return position.X; } set { position.X = value; } }
        /// <summary>
        /// Координата У позиции на экране
        /// </summary>
        public double Y { get { return position.Y; } set { position.Y = value; } }
        /// <summary>
        /// Ссылка на исходную точку
        /// </summary>
        public SourcePoint SourcePoint { get; set; }
        /// <summary>
        /// Коструктор
        /// </summary>
        public GeometryPoint() { position = new Point(); SourcePoint = null; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="posPt">Позиция на экране</param>
        /// <param name="srcPoint">Ссылка на исходную точку графика</param>
        public GeometryPoint(Point posPt, SourcePoint srcPoint) { this.Position = posPt; this.SourcePoint = srcPoint; }
        /// <summary>
        /// Создание полной копии объекта
        /// </summary>
        /// <returns>копия объекта</returns>
        public GeometryPoint Clone()
        {
            GeometryPoint item = new GeometryPoint();
            item.Position = this.Position;
            item.SourcePoint = this.SourcePoint;
            return item;
        }
        /// <summary>
        /// Копировать содержимое объекта в item
        /// </summary>
        /// <param name="item">объект, в который будет произведено копирование</param>
        public void CopyTo(GeometryPoint item)
        {
            item.Position = this.Position;
            item.SourcePoint = this.SourcePoint;
        }
        /// <summary>
        /// Копирование содержимого объекта из item
        /// </summary>
        /// <param name="item">объект, из которого будет произведено копирование</param>
        public void CopyFrom(GeometryPoint item)
        {
            this.Position = item.Position;
            this.SourcePoint = item.SourcePoint;
        }
    }
}
