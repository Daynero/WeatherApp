using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

namespace Panels.MainPanel
{
    public class AdvertisingScroll : MonoBehaviour
    {
        [SerializeField] private SimpleScrollSnap scroll;
        [SerializeField] private float scrollInterval = 2f;

        private void Start()
        {
            scroll.OnDragBegin += StopAutoScrolling;
            scroll.OnDragEnd += StartAutoScrolling;
        }

        public void StartAutoScrolling()
        {
            InvokeRepeating(nameof(ScrollToNext), scrollInterval, scrollInterval);
        }
        
        private void ScrollToNext()
        {
            scroll.GoToNextPanel();
        }

        private void StopAutoScrolling()
        {
            CancelInvoke(nameof(ScrollToNext));
        }
    }
}