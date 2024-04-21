using PanelsNavigationModule;

namespace Panels.SettingsPanel
{
    public class SettingsPanelController : AbstractPanelController
    {
        private readonly SettingsPanelMono _panelMono;

        public SettingsPanelController(SettingsPanelMono panelMono) : base(panelMono)
        {
            _panelMono = panelMono;
        }

        public override PanelType PanelType { get; }
    }
}