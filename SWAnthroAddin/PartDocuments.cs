using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWErgotronAddin
{
    public class Part
    {
        public string PartName { get; set; }
        public string PartFullPath { get; set; }

        public Part(string d, string p)
        {
            PartName = d;
            PartFullPath = p;
        }
    }

    public class PartList : List<Part>
    {
        public bool Contains(string s, Part p)
        {
            if (p.PartName == s)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
