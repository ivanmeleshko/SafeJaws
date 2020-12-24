using Assets.Scripts.SerializedObject;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public static class CanvasTools
    {
        public static Vector2 ConverToRelative(Vector2 pos, RectTransform canvasRect, CanvasScaler canvasScaler)
        {
            ScreenValue screenValue = GetScreenValue(canvasRect, canvasScaler);
            return new Vector2(pos.x / screenValue.scale - screenValue.offset.x, pos.y / screenValue.scale - screenValue.offset.y);
        }

        private static ScreenValue GetScreenValue(RectTransform canvasRect, CanvasScaler scaler)
        {
            Vector2 offset = new Vector2((canvasRect.rect.width - scaler.referenceResolution.x) / 2f, (canvasRect.rect.height - scaler.referenceResolution.y) / 2f);
            return new ScreenValue(canvasRect.localScale.x, offset);
        }
    }
}
