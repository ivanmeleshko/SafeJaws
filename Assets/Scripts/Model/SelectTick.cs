using System;
using Colors;
using Enum;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    [Serializable]
    public class SelectTick
    {
        [SerializeField] private Image _imageTick;
        [SerializeField] private ModelType _modelType;
        [SerializeField] private ColorPickObject _colorPicker;
        private bool _executeDefault = true;

        public void ActivateTick(ModelType modelType, Action<ModelType, ColorObject> onClikColorAction)
        {
            _imageTick.enabled = modelType == _modelType;

            if (_modelType <= modelType)
            {
                _colorPicker.Active(_modelType);
                UpdateColorText(modelType);
                _colorPicker.SetOnValueChangeCallback(onClikColorAction);
                if (_executeDefault)
                {
                    _colorPicker.OnColorUpdateDefault();
                    _executeDefault = false;
                }
            }
            else
                _colorPicker.Deactive();
        }

        private void UpdateColorText(ModelType modelType)
        {
            if (modelType == ModelType.OneColor)
            {
                if (_modelType == ModelType.OneColor)
                    _colorPicker.SetText("Select jaws color");
            }
            else if (modelType == ModelType.TwoColor)
            {
                if (_modelType == ModelType.OneColor)
                    _colorPicker.SetText("Select right side color");
                if (_modelType == ModelType.TwoColor)
                    _colorPicker.SetText("Select left side color");
            }
            else if (modelType == ModelType.ThirdColor)
            {
                if (_modelType == ModelType.OneColor)
                    _colorPicker.SetText("Select right side color");
                if (_modelType == ModelType.TwoColor)
                    _colorPicker.SetText("Select midlle side color");
                if (_modelType == ModelType.ThirdColor)
                    _colorPicker.SetText("Select left side color");
            }
        }
    }
}
