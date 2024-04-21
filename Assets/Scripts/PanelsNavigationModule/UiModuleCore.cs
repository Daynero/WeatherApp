using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace PanelsNavigationModule
{
    public class UiModuleCore
    {
        public event Action<PanelType> OnPanelOpenedEvent;
        public event Action<PanelType> OnPanelDisposedEvent;
        
        public PanelType CurrentPanel { get; private set; }
        public PanelType PreviousPanel { get; private set; }
        
        private readonly UiModuleMono _uiModuleMono;
        private readonly List<IPanelController> _panelControllerList = new();
        private readonly Dictionary<PanelType, bool> _panelsStateDictionary = new()
        {
            {PanelType.Menu, true},
            {PanelType.Hangar, true},
            {PanelType.Store, true},
            {PanelType.Settings, false},
            {PanelType.Pause, false},
            {PanelType.Splash, true},
            {PanelType.Result, true},
            {PanelType.EndGame, true},
            {PanelType.Game, true},
            {PanelType.CurrencyPopup, false},
        };

        private PanelProvider _panelProvider;

        public UiModuleCore(UiModuleMono uiModuleMono)
        {
            _uiModuleMono = Object.Instantiate(uiModuleMono);
            
            Init();
        }

        private void Init()
        {
            _panelProvider = new PanelProvider(_uiModuleMono.PanelDatabase, _uiModuleMono.ParentTransform);
        }

        public void Show(PanelType panelType)
        {
            foreach (var controller in _panelControllerList)
            {
                if (controller.PanelType == panelType)
                    return;
            }
            
            if (_panelsStateDictionary[panelType])
            {
                CloseAllPanels();
            }
            
            CurrentPanel = panelType;

            IPanelController panelController = _panelProvider.Get(panelType);
            
            if (panelController == null)
                return;
            
            InitPanel(panelController);
            
            OnPanelOpenedEvent?.Invoke(panelType);
        }

        public void Close()
        {
            CurrentPanel = PanelType.Undefined;
            CloseAllPanels();
        }

        private void CloseAllPanels()
        {
            for (int i = _panelControllerList.Count - 1; i >= 0; i--)
            {
                _panelControllerList[i].Close();
            }
        }

        private void InitPanel(IPanelController panelController)
        {
            panelController.OnPanelRequestedEvent += Show;
            panelController.OnPanelDisposedEvent += OnPanelDisposedHandler;
            _panelControllerList.Add(panelController);
        }

        private void OnPanelDisposedHandler(IPanelController panelController)
        {
            OnPanelDisposedEvent?.Invoke(panelController.PanelType);
            _panelControllerList.Remove(panelController);
        }
    }
}