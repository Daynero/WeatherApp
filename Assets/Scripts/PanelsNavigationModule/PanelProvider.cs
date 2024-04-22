using System;
using System.Collections.Generic;
using Panels.InfoPanel;
using Panels.IntroPanel;
using Panels.MainPanel;
using Panels.SettingsPanel;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PanelsNavigationModule
{
    public class PanelProvider
    {
        private readonly PanelDatabase _panelDatabase;

        private readonly Dictionary<PanelType, Func<AbstractPanelMono, IPanelController>> _panelControllerDictionary = new()
        {
            [PanelType.Settings] = mono => new SettingsPanelController(mono as SettingsPanelMono),
            [PanelType.Main] = mono => new MainPanelController(mono as MainPanelMono),
            [PanelType.Info] = mono => new InfoPanelController(mono as InfoPanelMono),
            [PanelType.Intro] = mono => new IntroPanelController(mono as IntroPanelMono),
        };

        public PanelProvider(PanelDatabase panelDatabase)
        {
            _panelDatabase = panelDatabase;
        }

        public IPanelController Get(PanelType panelTypeType)
        {
            AbstractPanelMono prefab = _panelDatabase[panelTypeType];
            if (prefab == null)
                return null;

            AbstractPanelMono mono = Object.Instantiate(prefab);
            IPanelController panelController = _panelControllerDictionary[panelTypeType]?.Invoke(mono);
            return panelController;
        }
    }
}