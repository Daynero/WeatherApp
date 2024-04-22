using PanelsNavigationModule;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.InfoPanel
{
    public class InfoPanelMono : AbstractPanelMono
    {
        [field: SerializeField] public Button SocialLinkButton { get; private set; }
        [field: SerializeField] public Button CloseButton { get; private set; }

        public override PanelType PanelType => PanelType.Info;
    }
}