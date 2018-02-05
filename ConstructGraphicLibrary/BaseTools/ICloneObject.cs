using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Интерфейс клонирования
    /// </summary>
    /// <typeparam name="T">Тип клонированного объекта</typeparam>
    public interface ICloneObject<T>
    {
        /// <summary>
        /// Клонирование объекта
        /// </summary>
        /// <returns>Новый экземпляр объекта</returns>
        T Clone();
    }
}
