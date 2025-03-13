using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace JD.Notifications
{ 
    public class JD_Notification : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text contentsText;

        public void Setup(NotificationData data, float duration)
        {
            image.sprite = data.sprite;
            titleText.text = data.title;
            contentsText.text = data.contents;

            Destroy(gameObject, duration);
        }
    }
}


