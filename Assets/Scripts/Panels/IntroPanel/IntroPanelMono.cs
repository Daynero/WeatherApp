using PanelsNavigationModule;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.IntroPanel
{
    public class IntroPanelMono : AbstractPanelMono
    {
        [field: SerializeField] public Button StartAppButton { get; private set; }
        
        public override PanelType PanelType => PanelType.Intro;
    }
}