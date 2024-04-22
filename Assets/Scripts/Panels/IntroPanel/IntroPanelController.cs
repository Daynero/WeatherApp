using Extensions;
using PanelsNavigationModule;

namespace Panels.IntroPanel
{
    public class IntroPanelController : AbstractPanelController
    {
        private readonly IntroPanelMono _panelMono;
        public override PanelType PanelType => PanelType.Intro;

        public IntroPanelController(IntroPanelMono panelMono) : base(panelMono)
        {
            _panelMono = panelMono;

            Init();
        }

        private void Init()
        {
            _panelMono.StartAppButton.WhenClicked(Close);
        }
    }
}