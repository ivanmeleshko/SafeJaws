

using Colors;
using Enum;
using Managers;
using Model;
using Panel;
using Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.UITools;
using Tools;
using UITools;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{

    public class ModelColorScreen : BaseScreen
    {
        [SerializeField] private ButtonModel[] _buttonModelButtonps;
        [SerializeField] private PanelModel[] _panelModelButtons;
        [SerializeField] private ColorModelButton[] _colorModels;
        [SerializeField] private ButtonImage[] _buttonsImage;
        [SerializeField] private Material _outputMaterial;
        [SerializeField] private Material _outputMaterialMetalic;
        [SerializeField] private ImageModel[] _images;
        [SerializeField] private Dropdown _dropDown;
        [SerializeField] private WebGLInputField _inputField;
        [SerializeField] private WebGLNativeInputField _inputFieldForMobile;
        [SerializeField] private Text _inputTextCounter;
        [SerializeField] private Text _inputMobileTextCounter;
        [SerializeField] private Slider _sliderFontSizeField;
        [SerializeField] private FontModel[] _fontModels;
        [SerializeField] private ColorPickObject _textColorPickObject;
        [SerializeField] private RenderTextScreen _renderTexturel;
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private Text _modelSelectText;
        [SerializeField] private Texture2D _texture2D;
        [SerializeField] private Texture2D _texture2DTextMetalic;

        private Dictionary<PanelType, PanelModel> _panelButtonDictionaryCook;
        private Dictionary<ModelType, ButtonModel> _modelButtonDictionaryCook;
        private Dictionary<ModelType, ColorModelButton> _colorModelDictionaryCook;
        private Dictionary<ModelType, ButtonImage> _imageButtonDictionaryCook;

        private ColorObject _textColor = null;

        private bool _firstApply = false;

        private GameManager _gameManager { get { return GameManager.Instance; } }

        protected Dictionary<ModelType, ButtonModel> ModelButtonDictionary
        {
            get
            {
                if (_modelButtonDictionaryCook == null)
                {
                    _modelButtonDictionaryCook = new Dictionary<ModelType, ButtonModel>();
                    for (int i = 0; i < _buttonModelButtonps.Length; i++)
                    {
                        if (_modelButtonDictionaryCook.ContainsKey(_buttonModelButtonps[i].ModelType))
                            throw new Exception("ButtonModel object already exist");
                        _modelButtonDictionaryCook.Add(_buttonModelButtonps[i].ModelType, _buttonModelButtonps[i]);
                    }
                }

                return _modelButtonDictionaryCook;

            }
        }

        protected Dictionary<PanelType, PanelModel> PanelButtonDictionary
        {
            get
            {
                if (_panelButtonDictionaryCook == null)
                {
                    _panelButtonDictionaryCook = new Dictionary<PanelType, PanelModel>();
                    for (int i = 0; i < _panelModelButtons.Length; i++)
                    {
                        if (_panelButtonDictionaryCook.ContainsKey(_panelModelButtons[i].PanelType))
                            throw new Exception("PanelType object already exist");
                        _panelButtonDictionaryCook.Add(_panelModelButtons[i].PanelType, _panelModelButtons[i]);
                    }
                }

                return _panelButtonDictionaryCook;

            }
        }

        protected Dictionary<ModelType, ColorModelButton> ColorModelDictionary
        {
            get
            {
                if (_colorModelDictionaryCook == null)
                {
                    _colorModelDictionaryCook = new Dictionary<ModelType, ColorModelButton>();
                    for (int i = 0; i < _colorModels.Length; i++)
                    {
                        if (_colorModelDictionaryCook.ContainsKey(_colorModels[i].ModelType))
                            throw new Exception("ColorModel object already exist");
                        _colorModelDictionaryCook.Add(_colorModels[i].ModelType, _colorModels[i]);
                    }
                }

                return _colorModelDictionaryCook;

            }
        }

        protected Dictionary<ModelType, ButtonImage> ImageButtonDictionary
        {
            get
            {
                if (_imageButtonDictionaryCook == null)
                {
                    _imageButtonDictionaryCook = new Dictionary<ModelType, ButtonImage>();
                    for (int i = 0; i < _buttonsImage.Length; i++)
                    {
                        if (_imageButtonDictionaryCook.ContainsKey(_buttonsImage[i].ModelType))
                            throw new Exception("ButtonImage object already exist");
                        _imageButtonDictionaryCook.Add(_buttonsImage[i].ModelType, _buttonsImage[i]);
                    }
                }

                return _imageButtonDictionaryCook;

            }
        }

        public ImageModel[] Images { get { return _images; } }

        private bool IsMobile()
        {
            //return false;
           //return true;
            return Application.isMobilePlatform;    
        }

        public override void Show(Action onComplete)
        {
            base.Show(onComplete);
            SubscribeEvents();
          //  _inputFieldMobile.gameObject.SetActive(IsMobile());
            _inputFieldForMobile.gameObject.SetActive(IsMobile());
            _inputField.gameObject.SetActive(!IsMobile());
        }

        private void SubscribeEvents()
        {
            _panelModelButtons.ForEach<PanelModel>(p => p.SubscribeOnPanelClick(PanelSelectClick));
            _buttonModelButtonps.ForEach<ButtonModel>(p => p.SubscribeOnClick(ActivateModel));
            _colorModels.ForEach<ColorModelButton>(p => p.Subscribe(OnClickColorModel));
            InstantiateDefaultValue();
            _texture2D = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
            _texture2DTextMetalic = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
            for (int i = 0; i < _buttonsImage.Length; i++)
                _buttonsImage[i].OnDescribeOnEvent(i, OnImageLoaded);
            SetDropDownOptions();

            if (IsMobile())
            {
                //_inputFieldMobile.onValueChanged.AddListener(OnInputValueChange);
                _inputFieldForMobile.onValueChanged.AddListener(OnInputValueChange);
            }
            else
                _inputField.onValueChanged.AddListener(OnInputValueChange);
            _textColorPickObject.SetOnValueChangeCallback(OnValueChange);
        }

        private void InstantiateDefaultValue()
        {
            ActivateModel(ModelType.OneColor, false);
        }

        private void OnClickColorModel()
        {
            _colorModels.ForEach<ColorModelButton>(p => p.DeActiveteModel());
        }

        private void OnClickImage()
        {
            _colorModels.ForEach<ColorModelButton>(p => p.DeActiveteModel());
        }

        private bool _firstOpenImagePanel = false;
        private void PanelSelectClick(PanelType panelType)
        {
            _panelModelButtons.ForEach<PanelModel>(p => p.DeactivatePanel());
            if (PanelButtonDictionary.ContainsKey(panelType))
            {
                IPanel panel = PanelButtonDictionary[panelType].ActivatePanel();
                panel.Activate(panelType);
                OpenPanel(panelType);
            }
            else
                throw new Exception("PanelButtonDictionary have not panelType" + panelType.ToString());
        }

        private void OpenPanel(PanelType panelType)
        {
            if (_firstOpenImagePanel)
            {
                _errorSelectImage.text = string.Empty;
            }
            else
            {
                if (panelType == PanelType.ImageSelect)
                {
                     //ShowErrorMessage("We will try to remove image backgrounds;\nsome detailed images will have a border around them");
                    _errorSelectImage.text = "We will try to remove image backgrounds;\nsome detailed images will have a border around them";
                    _firstOpenImagePanel = true;
                }
                else
                {
                    _errorSelectImage.text = string.Empty;
                }
            }
        }


        [SerializeField] private Text _errorSelectImage;


        private void ActivateModel(ModelType modelType)
        {
            ActivateModel(modelType, true);
        }

        private void ActivateModel(ModelType modelType, bool activateButton)
        {
            if (ModelButtonDictionary.ContainsKey(modelType))
            {
                _buttonModelButtonps.ForEach<ButtonModel>(p => p.DeActiveteModel());
                ModelButtonDictionary[modelType].ActiveteModel();
                _colorModels.ForEach<ColorModelButton>(p => p.Activate(modelType));
                _gameManager.SelectJawsModel(modelType);
                if (modelType == ModelType.OneColor) AlphaTween.OnTweenColorAction?.Invoke();
                if (activateButton) _buyButton.gameObject.SetActive(true);

                if (modelType == ModelType.OneColor)
                    _modelSelectText.text = string.Empty;
                else if (modelType == ModelType.TwoColor)
                    _modelSelectText.text = "Tap the section of the mouthguard to change colour";
                else if (modelType == ModelType.ThirdColor)
                    _modelSelectText.text = "Tap the section of the mouthguard to change colour";
            }
            else
                throw new Exception("ModelButtonDictionary have not modelType" + modelType.ToString());
        }

        private void SelectRandomColor()
        {
            _textColorPickObject.OnColorUpdateDefault();
        }

        private void OnEnable()
        {
            OnInputValueChange(string.Empty);
        }
       
        public static bool Clear = false;


       // private string test = "";
        //private void OnGUI()
        //{
        //    if (GUI.Button(new Rect(0, 200, 100, 100), test))
        //    {
        //        OnInputValueChange(null);
        //        string text = test;
        //        if (string.IsNullOrEmpty(text))
        //        {
        //            _textColorPickObject.Deactive();
        //        }
        //        else
        //        {
        //            _textColorPickObject.Active(ModelType.None);
        //            if (_textColor == null)
        //                StartCoroutine(SelectDefaultColor());
        //        }

        //        _inputTextCounter.text = text.Length + "/24";
        //        _inputMobileTextCounter.text = text.Length + "/24";
        //    }
        //}

        public void OnInputValueChange(string arg0)
        {
            string text = IsMobile() ? _inputFieldForMobile.text : _inputField.text;
          //  test = text;
            _inputTextCounter.text = text.Length + "/24";
            _inputMobileTextCounter.text = text.Length + "/24";

            if (string.IsNullOrEmpty(text))
            {
                _textColorPickObject.Deactive();
            }
            else
            {
                _textColorPickObject.Active(ModelType.None);
                if (_textColor == null)
                    StartCoroutine(SelectDefaultColor());
            }
        }
      
        private IEnumerator SelectDefaultColor()
        {
            yield return new WaitForEndOfFrame();
            _textColorPickObject.OnColorUpdateDefault();
        }

        private void OnValueChange(ModelType modelType, ColorObject colorObject)
        {
            _textColor = colorObject;
            if (_firstApply)
                OnClickSetText();
        }

        public void OnInputValueChange() { }

        [SerializeField] private WarningPopup _warningPopup;
        private void ShowErrorMessage(string message)
        {
            _warningPopup.OpenPopup();
            _warningPopup.SetText(message);
        }
         [SerializeField] private ConfirmPopup _confirmPopup;
        private void ShowSimplePopup(string message, Action onOkClick, Action onCancelClick)
        {
            _confirmPopup.OpenPopup();
            _confirmPopup.SetText(message, onOkClick, onCancelClick);
        }

        public void OnClickSetText()
        {
            if ((_images[1].ImageType == ImageType.Image || _images[1].ImageType == ImageType.CenterImage) && _images[1].MainTexture2D != null)
            {
                ShowSimplePopup("Are you sure you want to replace the image/text?",
                    delegate
                    {
                        _buttonsImage[1].ImageDeselect();
                        ApplyTextToModel();
                    }, null);
            }
            else
            {
                ApplyTextToModel();
            }
        }

        private void ApplyTextToModel()
        {
            string text = IsMobile() ? _inputFieldForMobile.text : _inputField.text;
          //  string text = IsMobile() ? _inputFieldMobile.text : _inputField.text;
            if (text.Length > 24)
            {
                ShowErrorMessage("Error. Max length of text 24 characters.");
            }
            else
            {
                _gameManager.SetCustomText(!string.IsNullOrEmpty(text));
                _firstApply = true;
                GenerateTextureByText(text, _fontModels[_dropDown.value]);
                _gameManager.OnSelectImageOrApplyText();
                _buyButton.gameObject.SetActive(true);
            }
        }

        public void OnClearText()
        {
            if (IsMobile())
            {
                //_inputFieldMobile.text = string.Empty;
                _inputFieldForMobile.text = string.Empty;
            }
            else
                _inputField.text = string.Empty;
            OnClickSetText();
            _gameManager.OnSelectImageOrApplyText();
        }

        public void OnClickAddChartButtonClick()
        {
            Debug.Log("Buy button clicked");
            ScreenManager.Instance.OnShowAddChartPopup();
        }

        private void SetDropDownOptions()
        {
            _dropDown.options = new List<Dropdown.OptionData>();
            for (int i = 0; i < _fontModels.Length; i++)
            {
                var t = new Dropdown.OptionData();
                t.text = _fontModels[i].GetName();

                //image for text
                t.image = _fontModels[i].GetImage();
                _dropDown.options.Add(t);
                //_dropDown.ite
              //  _dropDown.options[i]. itemText.font = _fontModels[i].Font;
            }

        }

        [SerializeField] private Text _textFontDropDown;
        public void SelectFont(int selectedFont)
        {
            if (IsMobile())
            {
              //  _inputFieldMobile.textComponent.font = _fontModels[_dropDown.value].Font;
                _inputFieldForMobile.textComponent.font = _fontModels[_dropDown.value].Font;
            }
            else
                _inputField.textComponent.font = _fontModels[_dropDown.value].Font;
            _textFontDropDown.font = _fontModels[_dropDown.value].Font;
        }

        private void GenerateTextureByText(string text, FontModel fontModel)
        {
            ScreenManager.Instance.OnShowLoadingPopup();
            fontModel.SetFontSize((int)_sliderFontSizeField.value);
            fontModel.SetFontColor(_textColor);
            _renderTexturel.OnGetTexture(text, fontModel, delegate (Texture2D d)
            {
                ScreenManager.Instance.OnHideLoadingPopup();
                _images[1].SetImageType(ImageType.TextImage, text, fontModel);
                _images[1].UpdateTexture(string.Empty, d);
                CombineAllImages();
            });
        }

        [SerializeField] private ColorItem _defaultColorItem;
        public void ResetImages()
        {
            for (int i = 0; i < _buttonsImage.Length; i++)
                _buttonsImage[i].SilentClear();

            GameManager.Instance.colorSelected[0] = _defaultColorItem.ColorName;
            GameManager.Instance.colorSelected[1] = _defaultColorItem.ColorName;
            GameManager.Instance.colorSelected[2] = _defaultColorItem.ColorName;
            ModelController.Instance.UpdateColor(ModelType.ThirdColor, ModelType.OneColor,_defaultColorItem.ColorObject);
            ModelController.Instance.UpdateColor(ModelType.ThirdColor, ModelType.TwoColor, _defaultColorItem.ColorObject);
            ModelController.Instance.UpdateColor(ModelType.ThirdColor, ModelType.ThirdColor, _defaultColorItem.ColorObject);

            if (IsMobile())
            {
               // _inputFieldMobile.text = string.Empty;
                _inputFieldForMobile.text = string.Empty;
            }
            else
                _inputField.text = string.Empty;

            _images[0].SetImageType(ImageType.Image);
            _images[0].UpdateTexture(string.Empty, null);
            _images[1].SetImageType(ImageType.Image);
            _images[1].UpdateTexture(string.Empty, null);
            _images[2].SetImageType(ImageType.Image);
            _images[2].UpdateTexture(string.Empty, null);
            _buyButton.gameObject.SetActive(true);
            _gameManager.SetCustomText(false);
            _gameManager.OnSelectImageOrApplyText();
            _buyButton.gameObject.SetActive(true);
            CombineAllImages();
        }

        public void OnImageLoaded(int index, string path, Texture2D imageModel, ButtonImage buttonImage, bool combine = true)
        {
            if (index == 1 && _images[1].ImageType == ImageType.TextImage && !string.IsNullOrEmpty(_images[1].ImageText))
            {
             //   ShowErrorMessage("Error. Image can’t be over text");
                _errorSelectImage.text = "If you want to customize this area,\nplease remove the current text/image";
                buttonImage.ImageDeselect();
            }
            else
            {
                _gameManager.CustomImage = true;
                if (index == 1)
                    _images[index].SetImageType(ImageType.CenterImage);
                else
                    _images[index].SetImageType(ImageType.Image);
                _images[index].UpdateTexture(path, imageModel);
                if(combine)
                    CombineAllImages();
                _gameManager.SetImages(_images);
                _gameManager.OnSelectImageOrApplyText();
                _errorSelectImage.text = string.Empty;
            }
        }

        private void CombineAllImages()
        {
            ScreenManager.Instance.OnShowLoadingPopup();
            StartCoroutine(CombineAll());
        }

        public IEnumerator CombineAll()
        {
            yield return new WaitForEndOfFrame();
            yield return GetClearTexture(delegate { StartCoroutine(CombineTexturesAll(0)); });
        }

        public IEnumerator CombineTexturesAll(int index)
        {
            if (index >= _images.Length)
            {
                ScreenManager.Instance.OnHideLoadingPopup();
                _outputMaterial.mainTexture = _texture2D;
                _outputMaterialMetalic.mainTexture = _texture2DTextMetalic;
                GameManager.Instance.ChangePrice();
                yield break; 
            }
            else
            {
                if (_images[index] != null &&
                    ((_images[index].ImageType == ImageType.TextImage && _textColor.MetalicSmoothess < 0.6f) ||
                     _images[index].ImageType == ImageType.Image || _images[index].ImageType == ImageType.CenterImage))
                {
                    yield return CombineTextures(_texture2D, _images[index].GetTexture(), _images[index].Offset);
                }
                else if (_images[index] != null && _images[index].ImageType == ImageType.TextImage)
                    yield return CombineTextures(_texture2DTextMetalic, _images[index].GetTexture(), _images[index].Offset);
                StartCoroutine(CombineTexturesAll(++index));
            }
        }

        private IEnumerator GetClearTexture(Action actionComplete)
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
            for (int y = 0; y < _texture2D.height; y++)
            {
                for (int x = 0; x < _texture2D.width; x++)
                {
                    _texture2D.SetPixel(x, y, new Color(0, 0, 0, 0));
                }
            }
            _texture2D.Apply();
            for (int y = 0; y < _texture2DTextMetalic.height; y++)
            {
                for (int x = 0; x < _texture2DTextMetalic.width; x++)
                {
                    _texture2DTextMetalic.SetPixel(x, y, new Color(0, 0, 0, 0));
                }
            }
            _texture2DTextMetalic.Apply();
            yield return new WaitForEndOfFrame();
            actionComplete?.Invoke();
        }

        private IEnumerator CombineTextures(Texture2D currentTexture, Texture2D image, Vector3 offset)
        {
            if (image == null)
                yield break;
            else
            {
                currentTexture.SetPixels((int)offset.x + (300 - image.width) / 2, (int)offset.y + (300 - image.height) / 2, image.width, image.height, image.GetPixels());
                currentTexture.Apply();
            }
        }

    }

}