
using Enum;
using Screens;
using SFB;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace Model
{
    public class ButtonImage : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _clearImage;
        [SerializeField] private ModelType _modelType;

        private int _index;
        private string _title = "";
        private string _fileName = "";
        private string _directory = "";
        private string _extension = "";
        private Action<int, string, Texture2D, ButtonImage, bool> _imageLoadEvent;

        public ModelType ModelType { get { return _modelType; } }

        public void ImageSelected()
        {
            _clearImage.gameObject.SetActive(true);
        }

        public void ImageDeselect()
        {
            _clearImage.gameObject.SetActive(false);
        }

        public void OnClearImage()
        {
            ScreenManager.Instance.OnShowLoadingPopup();
            SilentClear();
            _imageLoadEvent?.Invoke(_index, String.Empty, null, this, true);
        }

        public void SilentClear()
        {
            Managers.GameManager.Instance.customImage[(int)_modelType - 1] = false;
            Managers.GameManager.Instance.CustomImage = false;
            ImageDeselect();
        }

        public void OnDescribeOnEvent(int index, Action<int, string, Texture2D, ButtonImage, bool> onDescribe)
        {
            _index = index;
            _imageLoadEvent = onDescribe;
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void UploadFile(string id);

        public void OnPointerDown(PointerEventData eventData) 
        {
            UploadFile(gameObject.name);
        }

        public void OnFileUploaded(string url) 
        {
            Managers.GameManager.Instance.customImage[(int)_modelType - 1] = true;
            StartCoroutine(OutputRoutine(url));
        }
#else

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            var paths = StandaloneFileBrowser.OpenFilePanel(_title, _directory, _extension, false);
            if (paths.Length > 0)
            {
                Managers.GameManager.Instance.customImage[(int)_modelType - 1] = true;
                StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
            }
        }
#endif

        private IEnumerator OutputRoutine(string url)
        {
            ScreenManager.Instance.OnShowLoadingPopup();
            yield return new WaitForEndOfFrame();
            GC.Collect();
            Resources.UnloadUnusedAssets();
            Debug.LogWarningFormat("URL: {0}", url);
            var loader = new WWW(url);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            while (!loader.isDone)
                yield return new WaitForEndOfFrame();
            GetTexture(loader.texture);
            yield return new WaitForEndOfFrame();
            loader = null;
            Resources.UnloadUnusedAssets();
            GC.Collect();
            yield return new WaitForEndOfFrame();
            ScreenManager.Instance.OnHideLoadingPopup();
            ImageSelected();
            _imageLoadEvent?.Invoke(_index, url, _result, this, true);
        }
        Texture2D _result;
        public void GetTexture(Texture2D mainTexture2D)
        {
            if (mainTexture2D != null)
            {
                float maxValue = Mathf.Max(mainTexture2D.width, mainTexture2D.height * 2);
                float _scaleFactor = 600 / maxValue;
                _result = new Texture2D((int)(mainTexture2D.width * _scaleFactor), (int)(mainTexture2D.height * _scaleFactor), TextureFormat.RGBA32, false);
                for (int i = 0; i < _result.height; ++i)
                {
                    for (int j = 0; j < _result.width; ++j)
                    {
                        Color newColor = mainTexture2D.GetPixelBilinear((float)j / (float)_result.width, (float)i / (float)_result.height);
                        _result.SetPixel(j, i, newColor);
                    }
                }
                _result.Apply();
                GC.Collect();
            }
        }
    }
}
