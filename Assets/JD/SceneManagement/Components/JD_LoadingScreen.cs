using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JD.SceneManagement
{
    public class JD_LoadingScreen : MonoBehaviour, ILoadingScreenable
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image bar;

        private void Start() => SetActive(false);

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetTitle(string title) => titleText.text = title;

        public void SetProgress(float progress) => bar.fillAmount = progress;
    }
}

