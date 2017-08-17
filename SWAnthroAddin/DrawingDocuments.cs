using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWErgotronAddin
{
    public class Drawing
    {
        public string DrawingName { get; set; }
        public string DrawingFullPath { get; set; }

        public Drawing(string d, string p)
        {
            DrawingName = d;
            DrawingFullPath = p;
        }
    }

    public class DrawingList : List<Drawing>
    {
       

    }
}
