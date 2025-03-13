using System;
using UnityEngine;

namespace JD.Utility.Audio
{
    public class JD_AudioSource : MonoBehaviour, IAudioSourceable
    {
        [SerializeField] private AudioSourceType audioType;
        public AudioSourceType AudioSourceType { get { return audioType; } }

        private JD_AudioSourceStatic audioSourceStatic { get { return GetComponentInChildren<JD_AudioSourceStatic>(); } }
        private JD_AudioSourceDynamic audioSourceDynamic { get { return GetComponentInChildren<JD_AudioSourceDynamic>(); } }

        public void PlaySound(AudioData audio, Action OnProjectedFinish) => audioSourceStatic.PlaySound(audio, OnProjectedFinish);

        public void PlaySound(AudioData audio, Vector3 p, Action OnProjectedFinish) => audioSourceDynamic.PlaySound(audio, p, OnProjectedFinish);

        public void PlaySound(AudioData audio, Transform t, Action OnProjectedFinish) => audioSourceDynamic.PlaySound(audio, t, OnProjectedFinish);
    }
}