using UnityEngine;
using JD.Utility.Audio;

namespace JD.Utility.UIEssentials
{
    public class JD_UI_PlaySound : MonoBehaviour
    {
        [SerializeField] private string audio_ID;
        [SerializeField] private AudioSourceType type;

        public void Trigger() => AudioUtility.PlaySound(Audio.AudioData.Get(audio_ID), type);
    }
}