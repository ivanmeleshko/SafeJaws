
using Enum;
using Managers;
using Model;
using UnityEngine;

namespace Panel
{
    public class ColorPanel : BasePanel, IPanel
    {
        [SerializeField] private ColorModelButton _colorModelButtons;

        public new void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public new void Activate(PanelType panelType)
        {
            gameObject.SetActive(true);
            ActivateColorButton();
        }

        private void ActivateColorButton()
        {
            if (GameManager.Instance.CurrentColorModelType >= GameManager.Instance.CurrentModelType)
                _colorModelButtons.OnClick();
        }
    }
}