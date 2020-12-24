
using System;
using Enum;

namespace Panel
{
    public interface IPanel
    {
        void Activate(PanelType panelType);
        void Deactivate();
    }
}
