using System.Collections;
using UnityEngine;

namespace Logic.UI.Curtain
{
    public class LoadingCurtain : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private CanvasGroup curtain;
#pragma warning restore 649

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            
            gameObject.SetActive(false);    
        }
    }
}