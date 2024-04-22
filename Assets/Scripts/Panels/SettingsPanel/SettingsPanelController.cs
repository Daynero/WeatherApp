using Extensions;
using PanelsNavigationModule;
using SaveSystem;

namespace Panels.SettingsPanel
{
    public class SettingsPanelController : AbstractPanelController
    {
        private readonly SettingsPanelMono _panelMono;
        private readonly ISaveDataService _saveDataService;

        public override PanelType PanelType => PanelType.Settings;

        public SettingsPanelController(SettingsPanelMono panelMono) : base(panelMono)
        {
            _panelMono = panelMono;
            _saveDataService = new SaveDataService();

            Init();
        }

        private void Init()
        {
           _panelMono.CloseButton.WhenClicked(ClosePanel);
           _panelMono.ShowIntroductionButton.WhenClicked(ShowInfoPanel);

           LoadData();
        }

        private void ShowInfoPanel()
        {
            OnPanelRequested(PanelType.Info);
            ClosePanel();
        }

        private void LoadData()
        {
            if (!_saveDataService.Load(out SettingsData settingsData, FileNameConstants.SettingsData))
                return;

            _panelMono.MusicToggle.isOn = settingsData.IsMusicOn;
            _panelMono.SoundToggle.isOn = settingsData.IsSoundOn;
            _panelMono.VolumeSlider.value = settingsData.Volume;
        }

        private void SaveData()
        {
            SettingsData settingsData = new()
            {
                IsMusicOn = _panelMono.MusicToggle.isOn,
                IsSoundOn = _panelMono.SoundToggle.isOn,
                Volume = _panelMono.VolumeSlider.value
            };

            _saveDataService.Save(settingsData, FileNameConstants.SettingsData);
        }

        private void ClosePanel()
        {
            SaveData();
            Close();
        }
    }
}