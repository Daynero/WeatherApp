using System;
using UnityEngine;

namespace Panels.MainPanel
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private AdvertisingScroll scroll;

        private void Start()
        {
            scroll.StartAutoScrolling();
        }
    }
}