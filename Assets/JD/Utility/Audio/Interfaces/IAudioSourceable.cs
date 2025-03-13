using System;
using UnityEngine;

namespace JD.Utility.Audio
{
    public interface IAudioSourceable
    {
        public void PlaySound(AudioData audio, Action OnProjectedFinish);
        public void PlaySound(AudioData audio, Vector3 p, Action OnProjectedFinish);
        public void PlaySound(AudioData audio, Transform t, Action OnProjectedFinish);
        public AudioSourceType AudioSourceType { get; }
    }
}