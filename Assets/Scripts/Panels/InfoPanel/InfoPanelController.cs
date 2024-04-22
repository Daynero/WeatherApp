using Extensions;
using PanelsNavigationModule;
using Tools;
using UnityEngine;

namespace Panels.InfoPanel
{
    public class InfoPanelController : AbstractPanelController
    {
        private readonly InfoPanelMono _panelMono;
        public override PanelType PanelType => PanelType.Info;

        public InfoPanelController(InfoPanelMono panelMono) : base(panelMono)
        {
            _panelMono = panelMono;

            Init();
        }
        
        private void Init()
        {
            _panelMono.SocialLinkButton.WhenClicked(OpenSocialPage);
            _panelMono.CloseButton.WhenClicked(Close);
        }

        private void OpenSocialPage()
        {
            Application.OpenURL(SocialLinks.LinkedInURL);
        }
    }
}