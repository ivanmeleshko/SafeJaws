using System;

using UnityEngine;

namespace Colors
{
    [Serializable]
    public class ColorObject
    {
        public Color Color;
        public float MetalicSmoothess = 0f;

        public ColorObject(Color color)
        {
            Color = color;
            MetalicSmoothess = 0f;
        }

        public ColorObject(Color color, float metalic)
        {
            Color = color;
            MetalicSmoothess = metalic;
        }

        public override string ToString()
        {
            if (MetalicSmoothess > 0.5f)
                return string.Format("{0}", Color.ToString("F5"));
            else
                return string.Format("{0}", Color.ToString("F5"));
        }
    }
}
