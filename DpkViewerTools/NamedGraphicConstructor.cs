using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPK;
using ConstructGraphicLibrary.Data;

namespace DpkViewer.Tools
{
    public static class SourceBitGraphicConstructor
    {
        public static SourceGraphic Construct(DpkDataBuf dpkLogFile, int address, int numBit)
        {
            SourceGraphic namedGraphic = new SourceGraphic();
            namedGraphic.Name = string.Format("Адрес {0} {1}Бит {2} [{3}]", address,'\n', numBit, numBit+9);
            namedGraphic.MaxValue = 1;
            if (numBit >= 26) return namedGraphic;
            int countWords = dpkLogFile.Count;
            for (int i = 0; i < countWords; i++)
            {
                if (dpkLogFile[i].ADR == address)
                    namedGraphic.Points.Add(new SourcePoint(dpkLogFile[i].Time, ((dpkLogFile[i].DATA & (1 << numBit)) > 0) ? 1:0 ));
            }
            return namedGraphic;
        }
    }
}
