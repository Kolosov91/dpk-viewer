using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Класс аргументов событий с Т-параметром
    /// </summary>
    /// <typeparam name="T">Тип данных полезной информации (данных) при событии в аргументе</typeparam>
    public class EventArgs<T>:EventArgs
    {
        /// <summary>
        /// полезная информация (данные) при событии 
        /// </summary>
        public T Value { get; protected set; }
        /// <summary>
        /// Констурктор
        /// </summary>
        /// <param name="value">полезная информация (данные) при событии </param>
        public EventArgs(T value)
            : base()
        {
            Value = value;
        }
    }
}
