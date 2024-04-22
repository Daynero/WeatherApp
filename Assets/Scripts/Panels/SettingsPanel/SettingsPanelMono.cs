using PanelsNavigationModule;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.SettingsPanel
{
    public class SettingsPanelMono : AbstractPanelMono
    {
        [field: SerializeField] public Toggle MusicToggle { get; private set; }
        [field: SerializeField] public Toggle SoundToggle { get; private set; }
        [field: SerializeField] public Slider VolumeSlider { get; private set; }
        [field: SerializeField] public Button ShowIntroductionButton { get; private set; }
        [field: SerializeField] public Button CloseButton { get; private set; }
        
        public override PanelType PanelType => PanelType.Settings;
    }
}