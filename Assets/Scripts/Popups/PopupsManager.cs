using System;
using System.Collections.Generic;
using Utility;
using Tools;
using UnityEngine;

namespace Popups
{
    public class PopupsManager : MonoBehaviour
    {
        [SerializeField] private Canvas _alertCanvasPopup;
        [SerializeField] private Canvas _simpleCanvasPopup;
        [SerializeField] private List<AlertPopupsSerialized> _serializedAlertPopupLists = new List<AlertPopupsSerialized>();
        [SerializeField] private List<SimplePopupSerialized> _serializedSimplePopupLists = new List<SimplePopupSerialized>();

        private Dictionary<AlertPopupEnum, AlertPopupsSerialized> _alertPopupsSerializeds = new Dictionary<AlertPopupEnum, AlertPopupsSerialized>();
        private Dictionary<PopupEnum, SimplePopupSerialized> _simplePopupsSerializeds = new Dictionary<PopupEnum, SimplePopupSerialized>();

        private List<BasePopup> _simplePopupList = new List<BasePopup>();
        private List<BasePopup> _alertPopupList = new List<BasePopup>();
       
        #region Instance

        private static PopupsManager _instance;

        public static PopupsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("PopupsManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<PopupsManager>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        #endregion

        #region Property

        public Dictionary<AlertPopupEnum, AlertPopupsSerialized> AlertPopupsSerializeds
        {
            get { return _alertPopupsSerializeds; }
            set { _alertPopupsSerializeds = value; }
        }

        public Dictionary<PopupEnum, SimplePopupSerialized> SimplePopupsSerializeds
        {
            get { return _simplePopupsSerializeds; }
        }

        #endregion

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            foreach (var item in _serializedAlertPopupLists)
                _alertPopupsSerializeds.Add(item.PopupType, item);

            foreach (var item in _serializedSimplePopupLists)
                _simplePopupsSerializeds.Add(item.PopupType, item);
            ShowSimpleCanvas();
            ShowAlertCanvas();
        }


        #region  ShowSimplePopup

        public BasePopup ShowSimplePopup(PopupEnum typeOfPopup, Action<BasePopup> onCloseAction )
        {
            if (SimplePopupsSerializeds.ContainsKey(typeOfPopup))
            {
                return OpenSimplePopup(SimplePopupsSerializeds[typeOfPopup].PopupGameObjectPrefab, onCloseAction);
            }
            else
            {
                throw new Exception(string.Format("[PopupManager] ShowSimplepopup not found popup {0}",typeOfPopup.ToString()));
            }
        }

        private BasePopup OpenSimplePopup(BasePopup popupPrefab, Action<BasePopup> onClosePopup)
        {
            var popup = OpenPopup(popupPrefab, _simpleCanvasPopup, delegate(BasePopup popupTarget)
            {
                onClosePopup.Execute(popupTarget);
                _simplePopupList.Remove(popupTarget);
                UpdateCanvas();
            });
            _simplePopupList.Add(popup);
            UpdateCanvas();
            return popup;
        }

        #endregion

        #region  ShowAlertPopups

        public BasePopup ShowAlertPopup(AlertPopupEnum typeOfPopup, Action<BasePopup> onCloseAction )
        {
            if (AlertPopupsSerializeds.ContainsKey(typeOfPopup))
            {
                return OpenAlertPopup(AlertPopupsSerializeds[typeOfPopup].PopupGameObjectPrefab, onCloseAction);
            }
            else
            {
                throw new Exception(string.Format("[PopupManager] ShowSimplepopup not found popup {0}", typeOfPopup.ToString()));
            }
        }

        private BasePopup OpenAlertPopup(BasePopup popupPrefab, Action<BasePopup> onClosePopup)
        {
            var popup = OpenPopup(popupPrefab, _alertCanvasPopup, delegate(BasePopup alertPopup)
            {
                onClosePopup.Execute(alertPopup);
                _alertPopupList.Remove(alertPopup);
                UpdateCanvas();
            });
            _alertPopupList.Add(popup);
            UpdateCanvas();
            return popup;
        }

        #endregion

        private BasePopup OpenPopup(BasePopup popupPrefab, Canvas parentCanvas, Action<BasePopup> onClosePopup)
        {
            BasePopup popup = Utility.Tools.InstantiateChild<BasePopup>(parentCanvas, popupPrefab);
            popup.InitPopup(onClosePopup);
            return popup;
        }

        private void ShowAlertCanvas(bool show = false)
        {
            _alertCanvasPopup.gameObject.SetActive(show);
        }

        private void ShowSimpleCanvas(bool show = false)
        {
            _simpleCanvasPopup.gameObject.SetActive(show);
        }

        private void UpdateCanvas()
        {
            ShowSimpleCanvas(_simplePopupList.Count > 0);
            ShowAlertCanvas(_alertPopupList.Count > 0);
        }
    }
}