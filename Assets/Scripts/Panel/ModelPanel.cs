
using Enum;
using Panel;
using UnityEngine;

namespace Panel
{
    public class ModelPanel : BasePanel
    {
        public new void Deactivate()
        {
            gameObject.SetActive(false);
        }
        public new void Activate(PanelType panelType)
        {
            gameObject.SetActive(true);
        }
    }
}
