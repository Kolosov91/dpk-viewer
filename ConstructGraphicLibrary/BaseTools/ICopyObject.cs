using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Интерфейс копирования содержимого экземпляров объекта
    /// </summary>
    /// <typeparam name="T">Тип данных экземпляра объекта</typeparam>
    public interface ICopyObject<T>
    {
        /// <summary>
        /// Копировать содержимое в экземпляр
        /// </summary>
        /// <param name="item">экземпляр, в который будет производиться копирование содержимого</param>
        void CopyTo(T item);
        /// <summary>
        /// Копировать содержимое из экземпляра
        /// </summary>
        /// <param name="item">экземпляр, из которго будет производиться копирование содержимого</param>
        void CopyFrom(T item);
    }
}
