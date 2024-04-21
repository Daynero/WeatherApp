using UnityEngine;

namespace PanelsNavigationModule
{
    public abstract class AbstractPanelMono : MonoBehaviour
    {
        public abstract PanelType PanelType { get; }
    }
}