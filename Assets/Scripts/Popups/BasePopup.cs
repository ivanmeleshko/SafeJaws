using System;
using UnityEngine;

namespace Popups
{
    public class BasePopup : MonoBehaviour
    {
        private Action<BasePopup> _onClosePopup;

        public virtual void OpenPopup()
        {
            gameObject.SetActive(true);
        }

        public virtual void InitPopup(Action<BasePopup> onClosePopup)
        {
            _onClosePopup = onClosePopup;
        }

        public virtual void ClosePopup()
        {
            gameObject.SetActive(false);
        }
    }
}
