using System;

namespace PanelsNavigationModule
{
    public interface IPanelController
    {
        public event Action<PanelType> OnPanelRequestedEvent;
        public event Action<IPanelController> OnPanelDisposedEvent;

        public PanelType PanelType { get; }
        public void Show();
        public void Close();
    }
}