using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class WarningPopup : BasePopup
    {
        [SerializeField] private Text _warningText;

        public void SetText(string text)
        {
            _warningText.text = text;
        }

        public void OnClosePopup()
        {
            base.ClosePopup();
        }
    }
}
