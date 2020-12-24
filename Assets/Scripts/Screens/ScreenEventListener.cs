
using Enum;
using UnityEngine;

namespace Screens
{
    public class ScreenEventListener : MonoBehaviour
    {
        [SerializeField] private Vector3 _position = new Vector3(-35, 0, -100);
        [SerializeField] private Vector3 _positionFinish = new Vector3(-35, 0, -100);
        [SerializeField] private float _timeChange = .5f;

        private void OnEnable()
        {
            ScreenManager.OnScreenChangeEvent += OnScreenChangeEvent;
        }

        private void OnDisable()
        {
            ScreenManager.OnScreenChangeEvent -= OnScreenChangeEvent;
        }

        private void OnScreenChangeEvent(ScreenType screenType)
        {
            if (screenType == ScreenType.ModelScrenSelectSelect)
            {
                if (transform.localPosition != Vector3.zero)
                {
                    LeanTween.moveLocal(gameObject, Vector3.zero, _timeChange);
                }
            }
            else if (screenType == ScreenType.ImageScreenSelect)
            {
                if (transform.localPosition != _position)
                {
                    LeanTween.moveLocal(gameObject, _position, _timeChange);
                }
            }
            else if (screenType == ScreenType.FinishScreen)
            {
                if (transform.localPosition != _positionFinish)
                {
                    LeanTween.moveLocal(gameObject, _positionFinish, _timeChange);
                }
            }
        }
    }
}
