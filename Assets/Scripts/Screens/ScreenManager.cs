
using Enum;
using Popups;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Screens
{
    public class ScreenManager : Singelton<ScreenManager>
    {
        [SerializeField] private BaseScreen _modelColorSelectScreen;
        [SerializeField] private BaseScreen _loadingScreen;
        [SerializeField] private BaseScreen _finishScreen;
        [SerializeField] private BasePopup _loadingPopup;
        [SerializeField] private BasePopup _errorPopup;
        [SerializeField] private AddChartPopup _addChartPopup;
        [SerializeField] private GameObject _bottomButtons;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private GameObject _safeJawsModel;
        [SerializeField] private GameObject _safeJawsPopularModels;
        [SerializeField] private bool _mobilUiUse = false;
        [SerializeField] private RectTransform _bottomRectTRansform;
        [SerializeField] private RectTransform _bottomFinishRectTRansform;

        public delegate void ScreenChangeDelegate(ScreenType screenType);
        public static ScreenChangeDelegate OnScreenChangeEvent;
        
        public ModelColorScreen ModelColorScreen
        {
            get { return _modelColorSelectScreen as ModelColorScreen; }
        }

        public void OnShowLoadingScreen()
        {
            StartCoroutine(WaitAndShowLoading());
        }

        private IEnumerator WaitAndShowLoading()
        {
            _safeJawsModel.SetActive(false);
            _safeJawsPopularModels.SetActive(false);

            if (Application.isMobilePlatform || _mobilUiUse)
            {
                _bottomRectTRansform.anchoredPosition = new Vector2(0, 25);
                _canvasScaler.referenceResolution = new Vector2(1024, 550);
                _bottomFinishRectTRansform.anchoredPosition = new Vector2(0, 29);
            }
            else
            {
                _bottomRectTRansform.anchoredPosition = new Vector2(0, 10);
                _canvasScaler.referenceResolution = new Vector2(1024, 420);
                _bottomFinishRectTRansform.anchoredPosition = new Vector2(0, 0);
            }
            yield return new WaitForEndOfFrame();
            _loadingScreen.Show(OnShowModelColorSelectScreen);
            yield return new WaitForEndOfFrame();
        }

        public void OnShowModelColorSelectScreen()
        {
            _loadingScreen.Hide();
            Debug.LogWarning("OnShowModelColorSelectScreen");
            _modelColorSelectScreen.Show(OnShowImageScreen);
            _safeJawsModel.SetActive(true);
            _safeJawsPopularModels.SetActive(false);
            if (OnScreenChangeEvent != null) OnScreenChangeEvent(ScreenType.ModelScrenSelectSelect);
        }

        public void OnShowTextSelectScreen()
        {
            Debug.LogWarning("OnShowTextSelectScreen");
            _finishScreen.Show(null);
            if (OnScreenChangeEvent != null) OnScreenChangeEvent(ScreenType.FinishScreen);
        }

        public void OnShowImageScreen()
        {
            _modelColorSelectScreen.Hide();
            Debug.LogWarning("OnShowImageScreen");
        }

        public void OnShowLoadingPopup()
        {
            _loadingPopup.OpenPopup();
        }

        public void OnHideLoadingPopup()
        {
            _loadingPopup.ClosePopup();
        }

        public void OnShowErrorPopup(string message)
        {
            Debug.LogWarning("OnShowErrorPopup");
            _errorPopup.OpenPopup();
            (_errorPopup as WarningPopup)?.SetText(message);
        }

        public AddChartPopup OnShowAddChartPopup()
        {
            _bottomButtons.SetActive(false);
            _addChartPopup.Show(null);
            return _addChartPopup;
        }

        public void ShowBottomButtons()
        {
            _bottomButtons.SetActive(true);
        }
    }
}