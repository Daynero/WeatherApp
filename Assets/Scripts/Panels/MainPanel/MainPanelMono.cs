using System;
using PanelsNavigationModule;
using UnityEngine;
using UnityEngine.UI;
using Weather;

namespace Panels.MainPanel
{
    public class MainPanelMono : AbstractPanelMono
    {
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }
        [field: SerializeField] public WeatherController WeatherController { get; private set; }
        [field: SerializeField] public AdvertisingScroll Scroll { get; private set; }

        public event Action OnStart;

        public override PanelType PanelType => PanelType.Main;

        private void Start()
        {
            OnStart?.Invoke();
        }
    }
}