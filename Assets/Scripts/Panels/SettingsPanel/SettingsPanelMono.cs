using PanelsNavigationModule;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.SettingsPanel
{
    public class SettingsPanelMono : AbstractPanelMono
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button showIntroductionButton;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Slider volumeSlider;
        public override PanelType PanelType { get; }
    }
}