using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.Data;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Интерфейс общих данных (для обмена между компонентами)
    /// Создаётся в визуальном компоненте-контейнере (UserControl)
    /// </summary>
    public interface ICommonData
    {
        /// <summary>
        /// Свойство Общих данных для наследников BaseVisualComponent 
        /// </summary>
        CommonData CommonData { get; set; }
    }
}
