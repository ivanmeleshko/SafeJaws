
using System;
using Colors;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    [Serializable]
    public class FontModel
    {
        [SerializeField] private Font _font;
        [SerializeField] private Sprite _fontSprite;
        [SerializeField] private int _fontSize;
        [SerializeField] private ColorObject _fontColor;

        public Font Font { get { return _font; } }
        public int FontSize { get { return _fontSize; } }
        public ColorObject FontColor { get { return _fontColor; } }

        public void SetFontSize(int fontSize)
        {
            _fontSize = fontSize;
        }

        public void SetFontColor(ColorObject color)
        {
            _fontColor = color;
        }

        public string GetName()
        {
            return _font.name;
        }

        public Sprite GetImage()
        {
            return _fontSprite;
        }
    }
}