using UnityEngine;
using JD.Utility.General;
using JD.Utility.Audio;
using System.Collections.Generic;

namespace JD.Notifications
{
    public class JD_NotificationManager : MonoSingleton<JD_NotificationManager>
    {
        [SerializeField] private Transform notificationParent;
        [SerializeField] private JD_Notification notificationPrefab;
        private Queue<JD_Notification> notificationQueue = new Queue<JD_Notification>();
        private int maxNotifications = 5;

        /// <summary>
        /// Initialise notification.
        /// </summary>
        /// <param name="data"> Notification.</param>
        /// <param name="duration"> Notification duration.</param>
        public void Notify(NotificationData data, float duration)
        {
            JD_Notification newNotification = Instantiate(notificationPrefab, notificationParent);

            newNotification.Setup(data, duration);

            notificationQueue.Enqueue(newNotification);

            if (notificationQueue.Count > maxNotifications)
            {
                JD_Notification oldestNotification = notificationQueue.Dequeue();
                if (oldestNotification != null)
                {
                    Destroy(oldestNotification.gameObject);
                }
            }
        
            AudioUtility.PlaySound(AudioData.Get("audio_pop"), AudioSourceType.SFX);
        }

    }

    public class NotificationData
    {
        public Sprite sprite;
        public string title;
        public string contents;
    }
}