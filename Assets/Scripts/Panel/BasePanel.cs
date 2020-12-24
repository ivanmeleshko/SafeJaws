using Enum;
using Panel;
using UnityEngine;

namespace Panel
{
    public class BasePanel : MonoBehaviour, IPanel
    {
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
        public void Activate(PanelType panelType)
        {
            gameObject.SetActive(true);
        }
    }
}
