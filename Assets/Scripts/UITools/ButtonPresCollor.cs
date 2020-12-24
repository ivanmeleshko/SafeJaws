
using UnityEngine;
using UnityEngine.UI;

namespace UIUtility
{
    public class ButtonPresCollor : MonoBehaviour
    {
        [SerializeField] private Button _targetButton;
        [SerializeField] private Image[] _multipleGrafix;
        [SerializeField] private Text[] _multipleText;

        private void FixedUpdate()
        {
            if (_targetButton != null)
            {
                foreach (var image in _multipleGrafix)
                    image.color = _targetButton.targetGraphic.color;

                foreach (var text in _multipleText)
                    text.color = _targetButton.targetGraphic.color;
            }
        }
    }
}
