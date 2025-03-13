using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JD.Utility.Audio
{
    public class JD_AudioInjector : MonoBehaviour
    {
        public string audio_ID;
        public AudioSourceType type;

        public void Start()
        {
            AudioUtility.PlaySound(AudioData.Get(audio_ID), type, () => { });
        }
    }
}
