using System.Collections.Generic;
using UnityEngine;

namespace PanelsNavigationModule
{
    [CreateAssetMenu(fileName = "PanelDatabase", menuName = "SO/PanelDatabase", order = 101)]
    public class PanelDatabase : ScriptableObject
    {
#pragma warning disable 649
        [SerializeField] private List<AbstractPanelMono> screenViews = new();
#pragma warning restore 649

        private Dictionary<PanelType, AbstractPanelMono> _panelsDictionary;

        public AbstractPanelMono this[PanelType panelType]
        {
            get
            {
                if (_panelsDictionary == null)
                    Init();
                if (_panelsDictionary == null)
                    return null;
                if (!_panelsDictionary.TryGetValue(panelType, out var abstractPanelMono))
                    return null;
                return abstractPanelMono;
            }
        }

        private void Init()
        {
            _panelsDictionary = new();
            
            foreach (var panelMono in screenViews)
            {
                _panelsDictionary[panelMono.PanelType] = panelMono;
            }
        }
    }
}