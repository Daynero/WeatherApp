using UnityEngine;

namespace PanelsNavigationModule
{
    public class UiModuleMono : MonoBehaviour
    {
        [field:SerializeField] public PanelDatabase PanelDatabase { get; private set; }
        [field:SerializeField] public Transform ParentTransform { get; private set; }
    }
}