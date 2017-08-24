using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWErgotronAddin
{
    public static class CheckDocument
    {
        public static bool isSheetmetal(this string name)
        {
            if (name.Length <= 3)
            {
                return false;
            }

            string prefix = name.Substring(0, 3);

            switch (prefix)
            {
                case "225":
                    return true;
                case "333":
                    return true;
                case "334":
                    return true;
                default:
                    return false;
            }
        }

        public static bool isDrawing(this string name)
        {
            if (name.Length <= 3)
            {
                return false;
            }

            string prefix = name.Substring(0, 3);

            switch (prefix)
            {
                case "100":
                    return true;
                case "101":
                    return true;                
                case "105":
                    return true;
                case "110":
                    return true;
                case "115":
                    return true;
                case "125":
                    return true;
                case "154":
                    return true;
                case "200":
                    return true;
                case "205":
                    return true;                
                case "225":
                    return true;
                case "240":
                    return true;
                case "250":
                    return true;
                case "251":
                    return true;
                case "275":
                    return true;
                case "333":
                    return true;
                case "334":
                    return true;
                case "343":
                    return true;
                case "344":
                    return true;
                case "345":
                    return true;
                case "346":
                    return true;
                case "370":
                    return true;
                case "400":
                    return true;
                case "405":
                    return true;
                case "425":
                    return true;
                case "426":
                    return true;
                case "444":
                    return true;
                case "445":
                    return true;
                case "500":
                    return true;
                case "502":
                    return true;
                case "505":
                    return true;
                case "506":
                    return true;
                case "507":
                    return true;
                case "508":
                    return true;
                case "509":
                    return true;
                case "510":
                    return true;
                case "511":
                    return true;
                case "512":
                    return true;
                case "513":
                    return true;
                case "514":
                    return true;
                case "515":
                    return true;
                case "600":
                    return true;
                case "605":
                    return true;
                case "615":
                    return true;
                case "602":
                    return true;
                case "625":
                    return true;
                case "630":
                    return true;
                case "634":
                    return true;
                case "640":
                    return true;
                case "650":
                    return true;
                case "655":
                    return true;
                case "665":
                    return true;
                case "670":
                    return true;
                case "695":
                    return true;
                case "696":
                    return true;
                case "697":
                    return true;
                case "698":
                    return true;
                case "699":
                    return true;
                case "700":
                    return true;
                case "705":
                    return true;
                case "710":
                    return true;
                case "717":
                    return true;
                case "718":
                    return true;
                case "740":
                    return true;
                case "741":
                    return true;
                case "743":
                    return true;
                case "744":
                    return true;
                case "750":
                    return true;
                case "755":
                    return true;
                case "777":
                    return true;
                case "790":
                    return true;
                case "800":
                    return true;
                case "801":
                    return true;
                case "802":
                    return true;
                case "803":
                    return true;
                case "804":
                    return true;
                case "805":
                    return true;
                case "806":
                    return true;
                case "807":
                    return true;
                case "808":
                    return true;
                case "809":
                    return true;
                case "810":
                    return true;
                case "816":
                    return true;
                case "817":
                    return true;
                case "820":
                    return true;
                case "821":
                    return true;
                case "822":
                    return true;
                case "823":
                    return true;
                case "824":
                    return true;
                case "825":
                    return true;
                case "826":
                    return true;
                case "827":
                    return true;
                case "828":
                    return true;
                case "829":
                    return true;
                case "840":
                    return true;
                case "850":
                    return true;
                case "860":
                    return true;
                case "870":
                    return true;
                case "871":
                    return true;
                case "880":
                    return true;
                case "888":
                    return true;
                case "900":
                    return true;
                case "909":
                    return true;
                case "910":
                    return true;
                case "913":
                    return true;
                case "915":
                    return true;
                case "916":
                    return true;       
                default:
                    return false;
            }
        }

        public static bool isProto(this string name)
        {
            Boolean found = false;

            if (name.Length <= 3)
            {
                return false;
            }

            string prefix = name.Substring(0, 2);

            switch (prefix)
            {
                case "M-":
                    found = true;
                    return found;
                case "W-":
                    found = true;
                    return found;
                case "T-":
                    found = true;
                    return found;
            }

            prefix = name.Substring(0, 3);

            switch (prefix)
            {
                case "BT-":
                    found = true;
                    return found;
                case "VT-":
                    found = true;
                    return found;
            }

            return found;
        }
    }
}
