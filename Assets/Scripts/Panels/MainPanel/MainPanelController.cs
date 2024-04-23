using Extensions;
using PanelsNavigationModule;
using UnityEngine;

namespace Panels.MainPanel
{
    public class MainPanelController : AbstractPanelController
    {
        private readonly MainPanelMono _panelMono;

        public override PanelType PanelType => PanelType.Main;

        public MainPanelController(MainPanelMono panelMono) : base(panelMono)
        {
            _panelMono = panelMono;

            Init();
        }

        private void Init()
        {
            _panelMono.SettingsButton.WhenClicked(OpenSettings);
            _panelMono.ExitButton.WhenClicked(CloseApplication);
            _panelMono.ResetButton.WhenClicked(ResetCards);
            _panelMono.Scroll.StartAutoScrolling();
            _panelMono.WeatherController.CreateCards();
            _panelMono.OnStart += OpenInfoPanel;
        }

        private void ResetCards()
        {
            _panelMono.WeatherController.CreateNewCards();
        }

        private void CloseApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OpenInfoPanel()
        {
            OnPanelRequested(PanelType.Info);
        }

        private void OpenSettings()
        {
            OnPanelRequested(PanelType.Settings);
        }
    }
}