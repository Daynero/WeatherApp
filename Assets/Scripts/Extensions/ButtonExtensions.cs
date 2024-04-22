using UnityEngine.UI;

namespace Extensions
{
    public static class ButtonExtensions
    {
        public static void WhenClicked(this Button button, UnityEngine.Events.UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
}