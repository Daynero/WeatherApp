using System;

namespace PanelsNavigationModule
{
    public interface IUiModuleService
    {
        event Action<PanelType> OnPanelOpenedEvent;
        event Action<PanelType> OnPanelDisposedEvent;
        
        PanelType CurrentPanel { get; }
        
        void Show(PanelType panelType);
        void Close();
    }
}