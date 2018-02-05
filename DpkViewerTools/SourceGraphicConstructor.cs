using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.Data;
using DPK;

namespace DpkViewer.Tools
{
    public static class SourceGraphicConstructor
    {
        public static SourceGraphic Construct(DpkDataBuf dpkLogFile, int address, int maxValue)
        {
            SourceGraphic srcGraphic = new SourceGraphic();
            srcGraphic.Name = string.Format("Адрес {0}", address);
            srcGraphic.MaxValue = maxValue;
            int countWords = dpkLogFile.Count;
            for (int i = 0; i < countWords; i++)
            {
                if (dpkLogFile[i].ADR == address)
                    srcGraphic.Points.Add(new SourcePoint(dpkLogFile[i].Time, dpkLogFile[i].DATA));
            }
            return srcGraphic;
        }
    }
}
