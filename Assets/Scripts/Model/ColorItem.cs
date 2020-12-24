
using System;
using Colors;
using Enum;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    [Serializable]
    public class ColorItem : MonoBehaviour
    {
        [SerializeField] private Enum.ColorsPreset _colorName;
        [SerializeField] private Image _image;
        [SerializeField] private bool _isMetalic = false;
        [SerializeField] private bool _isTransperent = false;
        [SerializeField] private Color _targetColor;
        [SerializeField] private bool _override = false;
        [SerializeField] private bool _textColor = false;
        private Action<ColorItem> _onClickColor;
        public ColorObject ColorObject
        {
            get { return new ColorObject(_override ? _targetColor : _image.color, GetSmoot()); }
        }

        public ColorsPreset ColorName
        {
            get { return _colorName; }
        }

        public void SubscribeOnColorChange(Action<ColorItem> onColorSelect)
        {
            _onClickColor = onColorSelect;
        }

        public void OnClickOnColor()
        {
            if(!_textColor)
                Managers.GameManager.Instance.colorSelected[Managers.GameManager.Instance.sectionSelected] = _colorName;
            _onClickColor.Execute(this);
        }

        private float GetSmoot()
        {
            if (_isMetalic)
                return 1f;
            if (_isTransperent)
                return 0.6f;
            return 0;
        }
    }
}
