using System;
using System.Collections.Generic;
using Panels.SettingsPanel;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PanelsNavigationModule
{
    public class PanelProvider
    {
        private readonly PanelDatabase _panelDatabase;
        private readonly Transform _parentCanvas;

        private readonly Dictionary<PanelType, Func<AbstractPanelMono, IPanelController>> _panelControllerDictionary = new()
        {
            [PanelType.Settings] = mono => new SettingsPanelController(mono as SettingsPanelMono),
        };

        public PanelProvider(PanelDatabase panelDatabase, Transform parentCanvas)
        {
            _panelDatabase = panelDatabase;
            _parentCanvas = parentCanvas;
        }

        public IPanelController Get(PanelType panelTypeType)
        {
            AbstractPanelMono prefab = _panelDatabase[panelTypeType];
            if (prefab == null)
                return null;

            AbstractPanelMono mono = Object.Instantiate(prefab, _parentCanvas);
            IPanelController panelController = _panelControllerDictionary[panelTypeType]?.Invoke(mono);
            return panelController;
        }
    }
}