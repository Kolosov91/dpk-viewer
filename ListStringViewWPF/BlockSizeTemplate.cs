using System;
using System.Collections.Generic;
using System.Text;

namespace ListStringViewWPF
{
    /// <summary>
    /// Шаблон размеров компонента блока элемента (строки)
    /// </summary>
    public class BlockSizeTemplate
    {
        /// <summary>
        /// Высота блока
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Список ширин колонок блока
        /// </summary>
        public List<int> ListColumnWidth { get; set; }
        public int GetWidthByIndex(int index)
        {
            int width = 0;
            for (int i = 0; i <= index; i++)
                width += ListColumnWidth[i];
            return width;
        }
        public int FullWidth 
        {
            get 
            {
                int width = 0;
                foreach (int item in ListColumnWidth)
                    width += item;
                return width;
            }
        }
        public BlockSizeTemplate()
        {
            Height = 10;
            ListColumnWidth = new List<int>();
        }
        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="height">высота блока</param>
        /// <param name="listColumnWidth">список ширин колонок блока</param>
        public BlockSizeTemplate(int height, List<int> listColumnWidth):this()
        {
            Height = height;
            ListColumnWidth = listColumnWidth;
        }
    }
}
