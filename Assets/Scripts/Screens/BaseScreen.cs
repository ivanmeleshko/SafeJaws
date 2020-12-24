
using System;
using Tools;
using UnityEngine;

namespace Screens
{
    public class BaseScreen : MonoBehaviour
    {
        private RectTransform _mainRectTransform;

        private RectTransform _panel
        {
            get
            {
                if(_mainRectTransform == null)
                    _mainRectTransform = gameObject.GetComponent<RectTransform>();
                return _mainRectTransform;
            }
        }
        protected Action onCompleteScreenAction;

      //  private Rect _lastSafeArea = new Rect(0, 0, 0, 0);

        public virtual void Show(Action onComplete)
        {
            onCompleteScreenAction = onComplete;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            ScreenManager.Instance.ShowBottomButtons();
            gameObject.SetActive(false);
        }

        public bool IsScreenOpen
        {
            get { return gameObject.activeSelf; }
        }

        protected void CompleteScreen()
        {
             onCompleteScreenAction.Execute();
        }

        protected void Update()
        {
            //Rect safeArea = GetSaveArea();
            //if (safeArea != _lastSafeArea)
            //    ApplySafeArea(safeArea);
        }

        private void ApplySafeArea(Rect area)
        {
            var anchorMin = area.position;
            var anchorMax = area.position + area.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;

        //    _lastSafeArea = area;
        }

        private Rect GetSaveArea()
        {
            #if UNITY_EDITOR
            if (Mathf.Min(Screen.width, Screen.height) == 1125 && Mathf.Max(Screen.width, Screen.height) == 2436)
            {
                if (Screen.width > Screen.height)
                {
                    return new Rect(132, 63, 2172, 1062);
                }
                else
                {
                    return new Rect(0, 102, 1125, 2202);
                }
            }
            #endif

            return Screen.safeArea;
        }
    }
}