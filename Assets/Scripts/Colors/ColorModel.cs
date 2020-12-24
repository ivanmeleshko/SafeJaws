
using Enum;
using UnityEngine;
using UnityEngine.UI;

namespace Colors
{
    public class ColorModel : MonoBehaviour
    {
        [SerializeField] private ModelType _colorModeSelect;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;


        public void UpdateColorPanel(ModelType currentmodelType)
        {
            if (_colorModeSelect <= currentmodelType)
                ActivatePanel();
            else
                DeactivatePanel();
        }

        public void ActivatePanel()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                RepostionPanel();
            }
        }

        private void DeactivatePanel()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                RepostionPanel();
            }
        }

        private void RepostionPanel()
        {
        }
    }
}