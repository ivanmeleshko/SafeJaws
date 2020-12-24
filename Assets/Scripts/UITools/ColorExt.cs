using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UITools
{
    public static class ColorExt
    {
        public static string GetColorHex(this Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(color.r), ToByte(color.g), ToByte(color.b));
        }

        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
    }
}
