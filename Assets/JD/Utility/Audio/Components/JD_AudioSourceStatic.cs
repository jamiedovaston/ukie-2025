using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JD.Utility.Audio
{
    [RequireComponent(typeof(JD_Audio))]
    public class JD_AudioSourceStatic : MonoBehaviour
    {
        private JD_Audio _audio { get { return GetComponent<JD_Audio>(); } }

        public void PlaySound(AudioData audio, Action OnProjectedFinish) => _audio.PlayOneShot(audio.Clip, OnProjectedFinish);
    }
}