using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.InfoPanel
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Button socialLinkButton;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            socialLinkButton.onClick.AddListener(OpenSocialPage);
            closeButton.onClick.AddListener(ClosePanel);
        }

        private void ClosePanel()
        {
            
        }

        private void OpenSocialPage()
        {
            Application.OpenURL(SocialLinks.LinkedInURL);
        }
    }
}