
using Colors;
using Enum;
using Model;
using Screens;
using System.Text;
using Scripts.UITools;
using Tools;

namespace Managers
{
    public class GameManager : Singelton<GameManager>
    {
        public float StartPrice = 49.99f;
        public delegate void OnCostCnageEvent(float price);
        public delegate void OnBlinkEvent();

        public static OnCostCnageEvent OnCostCnage;
        public static OnBlinkEvent OnBuyButtonBlink;

        private ModelType _currentModelType = ModelType.None;
        private ModelType _currentColorModelType = ModelType.None;

        private ScreenManager _screenManager { get { return ScreenManager.Instance; } }

        public ModelType CurrentModelType { get { return _currentModelType; } }
        public ModelType CurrentColorModelType { get { return _currentColorModelType; } }

        public bool _customText = false;
        public bool[] customImage = new bool[3];
        public void SetCustomText(bool isCustomText)
        {
            _customText = isCustomText;
        }

        public ColorsPreset[] _colorSelected = new ColorsPreset[3] { ColorsPreset.RED, ColorsPreset.RED, ColorsPreset.RED };
        public ColorsPreset[] colorSelected { get { return _colorSelected; } set {
               UnityEngine.Debug.LogError("set"+ _colorSelected[0].ToString()+ _colorSelected[1].ToString()+ _colorSelected[2].ToString());
                    _colorSelected = value; } }
        public int sectionSelected = 0;
        public ModelType selectedModelType = ModelType.OneColor;

        public bool CustomImage { get; set; } = false;

        /// <summary>
        /// Game start from here
        /// </summary>
        public void Start()
        {
            _screenManager.OnShowLoadingScreen();
            AlphaTween.OnTweenColorAction?.Invoke();
        }

        public ModelType SelectJawsModel(ModelType modelType)
        {
            _currentModelType = ModelController.Instance.SelectModel(modelType);
            OnCostCnage?.Invoke(GetPrice());
            OnBuyButtonBlink?.Invoke();
            return _currentModelType;
        }

        public void OnSelectImageOrApplyText()
        {
            OnCostCnage?.Invoke(GetPrice());
            OnBuyButtonBlink?.Invoke();
        }

        public void ColorPanelClick(ModelType typeOfColor)
        {
            _currentColorModelType = typeOfColor;
            OnCostCnage?.Invoke(GetPrice());
        }

        public void ChangePrice()
        {
            OnCostCnage?.Invoke(GetPrice());
        }

        public void ChangePrice(float designPrice)
        {
            OnCostCnage?.Invoke(designPrice);
        }

        public void ColorChangeClick(ModelType typeOfColor, ColorObject colorObject)
        {
            ModelController.Instance.UpdateColor(_currentModelType, typeOfColor, colorObject);
        }

        
        private float GetPrice()
        {
            float sum = StartPrice;

            if (_customText)
            {
                sum += 7.5f;
                if (customImage[0] || customImage[2])
                    sum += 7.5f;
            }
            else if (customImage[0] || customImage[1] || customImage[2])
                sum += 7.5f;
            return sum;
        }

        private ImageModel[] Images
        {
            get { return ScreenManager.Instance.ModelColorScreen.Images; }
        }



        public void SetImages(ImageModel[] images)
        {
            _images = images;
        }

        private ImageModel[] _images;
        public string GetModelText()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("Model Type: {0}", _currentModelType.ToString());

            if (_currentModelType == ModelType.OneColor)
                str.AppendFormat("\nMain Color: {0}",
                    ModelController.Instance.GetColor(_currentModelType, ModelType.OneColor));
            else if (_currentModelType == ModelType.TwoColor)
            {
                str.AppendFormat("\nLeft Color: {0}",
                    ModelController.Instance.GetColor(_currentModelType, ModelType.OneColor));
                str.AppendFormat("\nRight Color: {0}",
                    ModelController.Instance.GetColor(_currentModelType, ModelType.TwoColor));
            }
            else if (_currentModelType == ModelType.ThirdColor)
            {
                str.AppendFormat("\nLeft Color: {0}",
                    ModelController.Instance.GetColor(_currentModelType, ModelType.OneColor));
                str.AppendFormat("\nMiddle Color: {0}",
                    ModelController.Instance.GetColor(_currentModelType, ModelType.TwoColor));
                str.AppendFormat("\nRight Color: {0}",
                    ModelController.Instance.GetColor(_currentModelType, ModelType.ThirdColor));
            }

            if (Images[0].MainTexture2D != null)
            {
                str.AppendFormat("\nLeft Image: {0}", Images[0].MainTexture2D.name);
            }
            if (Images[2].MainTexture2D != null)
            {
                str.AppendFormat("\nRight Image: {0}", Images[2].MainTexture2D.name);
            }

            if (Images[1].MainTexture2D != null)
            {
                if (Images[1].ImageType == ImageType.Image || _images[1].ImageType == ImageType.CenterImage)
                    str.AppendFormat("\nMiddle Image:{0}", Images[1].MainTexture2D.name);
                else if (Images[1].ImageType == ImageType.TextImage)
                {
                    str.AppendFormat("\nMiddle Text: {0}", Images[1].ImageText);
                    str.AppendFormat("\nMiddle Font: {0}", Images[1].FontModel.Font);
                    str.AppendFormat("\nMiddle Font Color: {0}", Images[1].FontModel.FontColor.Color);
                }
            }

            return str.ToString();
        }
    }
}