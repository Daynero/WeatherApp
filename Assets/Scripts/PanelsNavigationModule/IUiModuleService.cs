using System;
using Infrastructure.Services;

namespace PanelsNavigationModule
{
    public interface IUiModuleService : IService
    {
        event Action<PanelType> OnPanelOpenedEvent;
        event Action<PanelType> OnPanelDisposedEvent;
        
        PanelType CurrentPanel { get; }
        
        void Show(PanelType panelType);
        void Close();
    }
}