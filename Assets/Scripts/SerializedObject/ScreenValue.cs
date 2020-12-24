
using System;
using UnityEngine;

namespace Assets.Scripts.SerializedObject
{
    [Serializable]
    struct ScreenValue
    {
        public float scale;
        public Vector2 offset;

        public ScreenValue(float scale, Vector2 offset)
        {
            this.scale = scale;
            this.offset = offset;
        }
    }
}
