using System;
using Enum;
using Model;
using Tools;
using UITools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Colors
{
    public class ColorPickObject : MonoBehaviour, IPointerDownHandler
    {

        [SerializeField] private ModelType _modelType;
        [SerializeField] private Text _colorHelpTitle;

        [SerializeField] private ColorItem[] _colorItems;
        [SerializeField] private MarkerFollow _markerFollow;
        [SerializeField] private ScrollRect _colorScrollRect;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private bool _test = false;

        private Color _color = Color.red;
        private Action<ModelType, ColorObject> _onValueChange;
        private Action<ColorItem> _onColorItemAction;

        public void DeActive(ModelType modelType)
        {
            _modelType = ModelType.None;
            gameObject.SetActive(false);
        }

        public void Active(ModelType modelType)
        {
            if ((Application.isMobilePlatform || _test) && _rectTransform != null)
                _rectTransform.sizeDelta = new Vector2(880, 83);
            _modelType = modelType;
            gameObject.SetActive(true);
            _colorItems.ForEach<ColorItem>(p => p.SubscribeOnColorChange(OnColorSelect));
        }

        public void SetPredefineColor(ColorItem colorItem)
        {
            SelectUiItem(colorItem.transform);
        }

        private void OnColorSelect(ColorItem colorItem)
        {
            _onValueChange?.Invoke(_modelType, colorItem.ColorObject);
            _onColorItemAction?.Invoke(colorItem);
            SelectUiItem(colorItem.transform);
            ForceStop();
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }

        public void SetText(string text)
        {
            _colorHelpTitle.text = text;
        }

        public Color Color
        {
            get { return _color; }
        }

        public void SetOnValueChangeCallback(Action<ModelType, ColorObject> onValueChange)
        {
            _onValueChange = onValueChange;
        }

        public void SetOnValueChangeCallback(Action<ColorItem> onColorItemAction)
        {
            _onColorItemAction = onColorItemAction;
        }

        public void OnColorUpdateDefault()
        {
            int randomIndex = GetRandom();
            _onValueChange.Execute(_modelType, _colorItems[randomIndex].ColorObject);
            SelectUiItem(_colorItems[randomIndex].transform);
        }

        private int GetRandom()
        {
            return UnityEngine.Random.Range(0, _colorItems.Length);
        }

        private void SelectUiItem(Transform item)
        {
            _markerFollow.SetFolowObject(item);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ForceStop();
        }

        private void ForceStop()
        {
            _work = false;
        }

        public void OnLeftArrowClick()
        {
            MoveScroll(_colorScrollRect.horizontalNormalizedPosition, Mathf.Clamp01(_colorScrollRect.horizontalNormalizedPosition - 0.35f));
        }

        public void OnRightArrowClick()
        {
            MoveScroll(_colorScrollRect.horizontalNormalizedPosition, Mathf.Clamp01(_colorScrollRect.horizontalNormalizedPosition + 0.35f));
        }

        private void MoveScroll(float from, float to)
        {
            _startFloat = from;
            _endFloat = to;
            _currentLerpTime = 0;
            _work = true;
        }

        private float _startFloat;
        private float _endFloat;
        private float _currentLerpTime;
        private float _lerpTime = 1f;
        private bool _work = false;

        protected void Update()
        {
            if (_work)
            {
                _currentLerpTime += Time.deltaTime;

                if (_currentLerpTime > _lerpTime)
                {
                    _currentLerpTime = _lerpTime;
                    _work = false;
                }

                float perc = _currentLerpTime / _lerpTime;
                perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
                _colorScrollRect.horizontalNormalizedPosition = Mathf.Lerp(_startFloat, _endFloat, perc);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("GetAllColors")]
        private void GetAllColors()
        {
            _colorItems = new ColorItem[0];
            _colorItems = transform.GetComponentsInChildren<ColorItem>();
        }
#endif

    }
}