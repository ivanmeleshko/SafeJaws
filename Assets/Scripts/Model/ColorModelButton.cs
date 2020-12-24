
using System;
using Colors;
using Enum;
using Managers;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    [Serializable]
    public class ColorModelButton : MonoBehaviour
    {
        [SerializeField] private ColorItem _selectedColorItem;
        [SerializeField] private Image _selectImage;
        [SerializeField] private Text _buttonText;
        [SerializeField] private ColorPickObject _colorPickObject;
        [SerializeField] private ModelType _modelType;

        private Action _oncClickAction;

        public ModelType ModelType { get { return _modelType; } }

        public void Subscribe(Action onClick)
        {
            _oncClickAction = onClick;
        }

        public void Activate(ModelType modelType)
        {
            gameObject.SetActive(_modelType <= modelType);

            if (modelType == ModelType.OneColor && _modelType == ModelType.OneColor)
            {
                gameObject.SetActive(false);
                OnClick();
            }
            UpdateButtonText(modelType);
        }

        public void OnClick()
        {
            //Debug.Log("On Click!");
            GameManager.Instance.ColorPanelClick(_modelType);
            _colorPickObject.SetOnValueChangeCallback(OnClickColor);
            _colorPickObject.Active(_modelType);
            //Debug.Log(_modelType.ToString());
            if(_selectedColorItem != null)
                _colorPickObject.SetPredefineColor(_selectedColorItem);
            _oncClickAction.Execute();
            ActiveteModel();
        }

        public void ActiveteModel()
        {
            _selectImage.enabled = true;
        }

        public void DeActiveteModel()
        {
            _selectImage.enabled = false;
        }

        private void OnClickColor(ColorItem colorObject)
        {
            _selectedColorItem = colorObject;
            GameManager.Instance.ColorChangeClick(_modelType, colorObject.ColorObject);
        }

        private void UpdateButtonText(ModelType modelType)
        {
            if (modelType == ModelType.OneColor)
            {
                if (_modelType == ModelType.OneColor)
                    _buttonText.text = ("main");
            }
            else if (modelType == ModelType.TwoColor)
            {
                if (_modelType == ModelType.OneColor)
                    _buttonText.text = ("Left");
                if (_modelType == ModelType.TwoColor)
                    _buttonText.text = ("Right");
            }
            else if (modelType == ModelType.ThirdColor)
            {
                if (_modelType == ModelType.OneColor)
                    _buttonText.text = ("Left");
                if (_modelType == ModelType.TwoColor)
                    _buttonText.text = ("Middle");
                if (_modelType == ModelType.ThirdColor)
                    _buttonText.text = ("Right");
            }
        }
    }
}
