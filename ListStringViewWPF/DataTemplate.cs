using System;
using System.Collections.Generic;
using System.Text;

namespace ListStringViewWPF
{
    /// <summary>
    /// Данные для отображения в компоненте ListStringView
    /// </summary>
    public class DataListTemplate
    {
        /// <summary>
        /// Шаблон отображения блока (цвет, фон, шрифт)
        /// </summary>
        public ViewTemplate ViewT { get; set; }
        /// <summary>
        /// Список string-объектов для отображения в колонках
        /// </summary>
        public List<string> ListColumnText { get; set; }
        public DataListTemplate()
        {
            ViewT = new ViewTemplate();
            ListColumnText = new List<string>();
        }
        /// <summary>
        /// конструктор с парметрами
        /// </summary>
        /// <param name="viewT">шаблон отображения блока</param>
        /// <param name="listColumnText">Список string-объектов для отображения в колонках</param>
        public DataListTemplate(ViewTemplate viewT, List<string> listColumnText) : this()
        {
            ViewT = viewT;
            ListColumnText = listColumnText;
        }
    }
}
