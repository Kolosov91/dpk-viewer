using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.BaseTools;

namespace ConstructGraphicLibrary.Data
{
    /// <summary>
    /// Универсальная Точка построения графика
    /// </summary>
    public class SourcePoint : ICloneObject<SourcePoint>, ICopyObject<SourcePoint>
    {
        /// <summary>
        /// Время
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public SourcePoint() { Time = TimeSpan.Zero; Value = 0; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="value">Значение</param>
        public SourcePoint(TimeSpan time, int value) { Time = time; Value = value; }
        /// <summary>
        /// Создание полной копии объекта
        /// </summary>
        /// <returns>Полная копия объекта</returns>
        public SourcePoint Clone()
        {
            SourcePoint clonedObject = new SourcePoint(Time, Value);
            return clonedObject;
        }
        /// <summary>
        /// Копирование содержимого объекта в item
        /// </summary>
        /// <param name="item">объект, в который производится копирование</param>
        public void CopyTo(SourcePoint item)
        {
            item.Time = this.Time;
            item.Value = this.Value;
        }
        /// <summary>
        /// Копирование содержимого объекта из item
        /// </summary>
        /// <param name="item">объект, из которого производится копирование</param>
        public void CopyFrom(SourcePoint item)
        {
            this.Time = item.Time;
            this.Value = item.Value;
        }
    }
}
