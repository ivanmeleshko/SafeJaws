using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WebGLKeyboard
{

    public class DetectInputFocus : MonoBehaviour, IPointerClickHandler, IDeselectHandler
    {
        private KeyboardController controller = null;
        private UnityEngine.UI.InputField nativeInput;

        private InputField _inputField
        {
            get { return GetComponent<InputField>(); }
        }
       
        public void Initialize(KeyboardController _controller)
        {
            controller = _controller;
            nativeInput = gameObject.GetComponent<UnityEngine.UI.InputField>();
        }

        public void OnPointerClick(PointerEventData _data)
        {
            if (nativeInput != null)
            {
                controller.FocusInput(nativeInput);
            }
        }

        public void OnDeselect(BaseEventData data)
        {
            controller.ForceClose();
        }
    }
}