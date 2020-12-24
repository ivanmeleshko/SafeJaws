using System;
using UnityEngine;

namespace Popups
{
    [Serializable]
    public class AlertPopupsSerialized
    {
        [SerializeField] private AlertPopupEnum _popupType;
        [SerializeField] private BasePopup _popupGameObject;

        public BasePopup PopupGameObjectPrefab { get { return _popupGameObject; } }
        public AlertPopupEnum PopupType { get { return _popupType; } }
    }
}
