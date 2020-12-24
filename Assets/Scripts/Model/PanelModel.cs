
using System;
using Enum;
using Panel;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    [Serializable]
    public class PanelModel : MonoBehaviour
    {
        [SerializeField] private PanelType _panelType;
        [SerializeField] private Image _imageSelect;
        [SerializeField] private BasePanel _panelGameObject;

        private Action<PanelType> _onClickAction;

        public PanelType PanelType { get { return _panelType; }}

        public void SubscribeOnPanelClick(Action<PanelType> onClickAction)
        {
            _onClickAction = onClickAction;
            DeactivatePanel();
        }

        public IPanel ActivatePanel()
        {
            _imageSelect.enabled = true;
            return (_panelGameObject as IPanel);
        }

        public void DeactivatePanel()
        {
            _imageSelect.enabled = false;
            (_panelGameObject as IPanel).Deactivate();
        }

        public void OnClick()
        {
            _onClickAction.Execute(_panelType);
        }

        public void SelectCustom()
        {
            ModelController.Instance.SelectCustom();
        }

        public void SelectPopular()
        {
            ModelController.Instance.SelectPopular();
            
        }


    }
}