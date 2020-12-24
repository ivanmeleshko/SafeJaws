using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Popups
{
    public class ConfirmPopup : BasePopup
    {
        [SerializeField] private Text _warningText;
        private Action _onOkClickAction;
        private Action _onCancelClickAction;

        public void SetText(string text, Action onOkClick, Action onCancelClick)
        {
            _onOkClickAction = onOkClick;
            _onCancelClickAction = onCancelClick;
            _warningText.text = text;
        }

        public void OnClickOk()
        {
            _onOkClickAction?.Invoke();
            base.ClosePopup();
        }

        public void OnClosePopup()
        {
            _onCancelClickAction?.Invoke();
            base.ClosePopup();
        }
    }
}

