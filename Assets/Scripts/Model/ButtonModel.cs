
using System;
using Enum;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    [Serializable]
    public class ButtonModel : MonoBehaviour
    {
        [SerializeField] private Image _selectImage;
        [SerializeField] private ModelType _modelType;
        private Action<ModelType> _onClickModelAction;
        public ModelType ModelType { get { return _modelType; } }

        public void SubscribeOnClick(Action<ModelType> onClick)
        {
            _onClickModelAction = onClick;
            DeActiveteModel();
        }

        public void ActiveteModel()
        {
            _selectImage.enabled = true;
        }

        public void DeActiveteModel()
        {
            _selectImage.enabled = false;
        }

        public void OnClikcButton()
        {
            Managers.GameManager.Instance.selectedModelType = _modelType;
            _onClickModelAction.Execute(_modelType);
        }
    }
}