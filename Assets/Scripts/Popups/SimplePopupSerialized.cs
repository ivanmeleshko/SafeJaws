using System;
using UnityEngine;

namespace Popups
{
    [Serializable]
    public class SimplePopupSerialized
    {
        [SerializeField] private PopupEnum _popupType;
        [SerializeField] private BasePopup _popupGameObject;

        public BasePopup PopupGameObjectPrefab { get { return _popupGameObject; } }
        public PopupEnum PopupType { get { return _popupType; } }
    }
}
