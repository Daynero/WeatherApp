using System;
using Object = UnityEngine.Object;

namespace PanelsNavigationModule
{
    public abstract class AbstractPanelController : IPanelController
    {
        private protected readonly AbstractPanelMono PanelMono;

        protected AbstractPanelController(AbstractPanelMono panelMono)
        {
            PanelMono = panelMono;
        }

        public event Action<PanelType> OnPanelRequestedEvent;
        public event Action<IPanelController> OnPanelDisposedEvent;

        public abstract PanelType PanelType { get; }

        public virtual void Show()
        {
            
        }

        public virtual void Close()
        {
            Object.Destroy(PanelMono.gameObject);
            OnPanelDisposedEvent?.Invoke(this);
        }

        protected void OnPanelRequested(PanelType panelType)
        {
            OnPanelRequestedEvent?.Invoke(panelType);
        }
    }
}