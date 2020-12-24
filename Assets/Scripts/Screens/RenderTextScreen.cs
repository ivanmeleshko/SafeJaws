using Model;
using System;
using System.Collections;
using UnityEngine;

namespace Screens
{
    public class RenderTextScreen : MonoBehaviour
    {
        [SerializeField] private UnityEngine.TextMesh _textMesh;
        [SerializeField] private RenderTexture _liveRenderTexture;
        [SerializeField] private Camera _camera;
        [SerializeField] private Renderer _render;
        [SerializeField] private int _indexOfCharEnter = 12;
        private Action<Texture2D> _onCompleteAction;

        public void OnGetTexture(string text, FontModel fontModel, Action<Texture2D> onCompleteAction)
        {
            try
            {
                _textMesh.font = fontModel.Font;
                UpdateFontSizeAndLine(text, fontModel);
                _textMesh.color = fontModel.FontColor.Color;
                _onCompleteAction = onCompleteAction;
                _render.material = fontModel.Font.material;
                _render.material.SetFloat("_Metallic/_Smoothness", fontModel.FontColor.MetalicSmoothess);
                StartCoroutine(GetTexture2D());
            }
            catch
            {
                ScreenManager.Instance.OnHideLoadingPopup();
            }
        }
        Texture2D newTexetTexture2D;
        private IEnumerator GetTexture2D()
        {
            yield return new WaitForEndOfFrame();
            gameObject.SetActive(true);
            if(newTexetTexture2D == null || newTexetTexture2D.width != _liveRenderTexture.width || newTexetTexture2D.height != _liveRenderTexture.height)
                newTexetTexture2D = new Texture2D(_liveRenderTexture.width, _liveRenderTexture.height);
            _camera.targetTexture = _liveRenderTexture;
            _camera.Render();
            yield return new WaitForEndOfFrame();
            RenderTexture.active = _liveRenderTexture;
            newTexetTexture2D.ReadPixels(_camera.pixelRect, 0, 0);
            Rect rectReadPicture = new Rect(0, 0, _liveRenderTexture.width, _liveRenderTexture.height);
            RenderTexture.active = _liveRenderTexture;
            
            newTexetTexture2D.ReadPixels(rectReadPicture, 0, 0);
            newTexetTexture2D.Apply();
            _onCompleteAction(newTexetTexture2D);
        }

        private void UpdateFontSizeAndLine(string text, FontModel fontModel)
        {
            _textMesh.fontSize = fontModel.FontSize;
            switch (text.Length)
            {

                case 1:
                case 2:
                case 3:
                    _textMesh.fontSize = 450;
                    break;
                case 4:
                    _textMesh.fontSize = 360;
                    break;
                case 5:
                    _textMesh.fontSize = 280;
                    break;
                case 6:
                    _textMesh.fontSize = 260;
                    break;
                case 7:
                    _textMesh.fontSize = 220;
                    break;
                case 8:
                    _textMesh.fontSize = 180;
                    break;
                case 9:
                    _textMesh.fontSize = 160;
                    break;
                case 10:
                    _textMesh.fontSize = 140;
                    break;
                case 11:
                    _textMesh.fontSize = 130;
                    break;
                default:
                    _textMesh.fontSize = 120;
                    break;
            }
            if(fontModel.Font.fontNames.Equals("Headhunter"))
            {
                _textMesh.fontSize = 120;
            }

            if (text.Length > _indexOfCharEnter)
            {
                _textMesh.fontSize = fontModel.FontSize - 10;
            }
            UpdateString(ref text);

            _textMesh.text = text;
        }

        private void UpdateString(ref string text)
        {
            if (text.Length > _indexOfCharEnter)
            {
                for(int i = _indexOfCharEnter; i > 0; i--)
                {
                    if(text[i] == ' ')
                    {
                        text = text.Insert(i+1, "\n");
                        return;
                    }
                }
                text = text.Insert(_indexOfCharEnter, "\n");
            }
        }
    }
}
