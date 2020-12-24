using System;
using Enum;
using UnityEngine;

namespace Model
{

    [Serializable]
    public class ImageModel
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _offsetText;
        [SerializeField] private float _scaleFactor = 1f;
        [SerializeField] private int _rotateAngle = 0;
        [SerializeField] private Texture2D _mainTexture2D;
        [SerializeField] private ImageType _imageType;
        [SerializeField] private string _imagePath;
        [SerializeField] private string _imageText;
        [SerializeField] private FontModel _fontModel;
        private Texture2D result;

        public int Index { get; private set; }
        public Texture2D MainTexture2D { get { return _mainTexture2D; } }

        public Vector2 Offset
        {
            get
            {
                if (_imageType == ImageType.Image || _imageType == ImageType.CenterImage)
                    return _offset;
                else if (_imageType == ImageType.TextImage)
                    return _offsetText;
                return Vector2.zero;
            }
        }

        public float ScaleFactor
        {
            get { return _scaleFactor; }
            private set { _scaleFactor = value; }
        }

        public ImageType ImageType { get { return _imageType; }}

        public FontModel FontModel { get { return _fontModel; } }
        public string ImageText { get { return _imageText; } }
        public string ImagePath { get { return _imagePath; } }

        public ImageModel(int index, Texture2D texture, Vector3 offset)
        {
            Index = index;
            _mainTexture2D = texture;
            _offset = offset;
        }

        public void SetImageType(ImageType imageType)
        {
            SetImageType(imageType, String.Empty, null);
        }

        public void SetImageType(ImageType imageType, string text, FontModel fontModel)
        {
            _imageType = imageType;
            _imageText = text;
            _fontModel = fontModel;
        }

        public void UpdateTexture(string path, Texture2D texture)
        {
            _imagePath = path;
            _mainTexture2D = texture;
            if(texture == null)
            {
                result = new Texture2D(5, 5);
                result.Apply();
                GC.Collect();
                Resources.UnloadUnusedAssets();
            }
        }
        
        public Texture2D GetTexture()
        {
            if (MainTexture2D == null)
                return null;
            else
            {
                UpdateScaleOfImages();
                Debug.Log((int)(MainTexture2D.width * _scaleFactor));
                result = new Texture2D((int)(MainTexture2D.width * _scaleFactor), (int)(MainTexture2D.height * _scaleFactor), TextureFormat.RGBA32/* MainTexture2D.format*/, false);
                for (int i = 0; i < result.height; ++i)
                {
                    for (int j = 0; j < result.width; ++j)
                    {
                        Color newColor = MainTexture2D.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                        result.SetPixel(j, i, newColor);
                    }
                }
                result.Apply();
                GC.Collect();
                if (_rotateAngle == 0)
                    return result;
                else
                    return RotateTexture(result, _rotateAngle);
            }
        }

        private void UpdateScaleOfImages()
        {
            if (_imageType == ImageType.Image)
            {
                float maxValue = Mathf.Max(MainTexture2D.width, MainTexture2D.height);
                ScaleFactor = 300 / maxValue;
            }

            else if (_imageType == ImageType.TextImage)
            {
                float maxValue = Mathf.Max(MainTexture2D.width, MainTexture2D.height);
                ScaleFactor = 800 / maxValue;
            }

            else if(_imageType == ImageType.CenterImage)
            {
                float maxValue = Mathf.Max(MainTexture2D.width, MainTexture2D.height * 2);
                ScaleFactor = 600 / maxValue;
            }
        }

        private Texture2D RotateTexture(Texture2D tex, float angle)
        {
            Texture2D rotImage = new Texture2D(tex.width*2, tex.height*2);
            int x, y;
            float x1, y1, x2, y2;

            int w = tex.width;
            int h = tex.height;
            float x0 = rot_x(angle, -w / 2.0f, -h / 2.0f) + w / 2.0f;
            float y0 = rot_y(angle, -w / 2.0f, -h / 2.0f) + h / 2.0f;

            float dx_x = rot_x(angle, 1.0f, 0.0f);
            float dx_y = rot_y(angle, 1.0f, 0.0f);
            float dy_x = rot_x(angle, 0.0f, 1.0f);
            float dy_y = rot_y(angle, 0.0f, 1.0f);


            x1 = x0;
            y1 = y0;

            for (x = 0; x < tex.width; x++)
            {
                x2 = x1;
                y2 = y1;
                for (y = 0; y < tex.height; y++)
                {
                    //rotImage.SetPixel (x1, y1, Color.clear);          

                    x2 += dx_x;//rot_x(angle, x1, y1);
                    y2 += dx_y;//rot_y(angle, x1, y1);
                    rotImage.SetPixel((int)Mathf.Floor(x), (int)Mathf.Floor(y), getPixel(tex, x2, y2));
                }

                x1 += dy_x;
                y1 += dy_y;

            }

            rotImage.Apply();
            return rotImage;
        }

        private Color getPixel(Texture2D tex, float x, float y)
        {
            Color pix;
            int x1 = (int)Mathf.Floor(x);
            int y1 = (int)Mathf.Floor(y);

            if (x1 > tex.width || x1 < 0 ||
               y1 > tex.height || y1 < 0)
            {
                pix = Color.clear;
            }
            else
            {
                pix = tex.GetPixel(x1, y1);
            }

            return pix;
        }

        private float rot_x(float angle, float x, float y)
        {
            float cos = Mathf.Cos(angle / 180.0f * Mathf.PI);
            float sin = Mathf.Sin(angle / 180.0f * Mathf.PI);
            return (x * cos + y * (-sin));
        }

        private float rot_y(float angle, float x, float y)
        {
            float cos = Mathf.Cos(angle / 180.0f * Mathf.PI);
            float sin = Mathf.Sin(angle / 180.0f * Mathf.PI);
            return (x * sin + y * cos);
        }
    }
}
