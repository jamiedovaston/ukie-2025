using System;
using UnityEngine;
using JD.Utility.General;
using System.Collections.Generic;

namespace JD.Utility.Audio
{
    public enum AudioSourceType { SFX, MUSIC, AMBIENCE }

    public static class AudioUtility
    {
        /// <summary>
        /// Static play sound function.
        /// </summary>
        /// <param name="audio"> Audio reference</param>
        /// <param name="playType"> Sound type</param>
        public static void PlaySound(AudioData audio, AudioSourceType playType) => PlaySound(audio, playType, () => { });

        /// <summary>
        /// Static play sound function with projected finish callback.
        /// </summary>
        /// <param name="audio"> Audio reference</param>
        /// <param name="playType"> Sound type</param>
        /// <param name="OnProjectedFinish"> Projected finish callback</param>
        public static void PlaySound(AudioData audio, AudioSourceType playType, Action OnProjectedFinish)
        {
            List<IAudioSourceable> audioSources = InterfaceUtility.FindObjectsWithInterface<IAudioSourceable>();
            audioSources.Find(a => a.AudioSourceType == playType).PlaySound(audio, OnProjectedFinish);
        }

        /// <summary>
        /// Dynamic play sound function with projected finish callback.
        /// </summary>
        /// <param name="audio"> Audio reference</param>
        /// <param name="playType"> Sound type</param>
        /// <param name="p"> Position vector</param>
        /// <param name="OnProjectedFinish"> Projected finish callback</param>
        public static void PlaySound(AudioData audio, AudioSourceType playType, Vector3 p, Action OnProjectedFinish)
        {
            List<IAudioSourceable> audioSources = InterfaceUtility.FindObjectsWithInterface<IAudioSourceable>();
            audioSources.Find(a => a.AudioSourceType == playType).PlaySound(audio, p, OnProjectedFinish);
        }

        /// <summary>
        /// Dynamic play sound function with projected finish callback.
        /// </summary>
        /// <param name="audio"> Audio reference</param>
        /// <param name="playType"> Sound type</param>
        /// <param name="t"> Transform position</param>
        /// <param name="OnProjectedFinish"> Projected finish callback</param>
        public static void PlaySound(AudioData audio, AudioSourceType playType, Transform t, Action OnProjectedFinish)
        {
            List<IAudioSourceable> audioSources = InterfaceUtility.FindObjectsWithInterface<IAudioSourceable>();
            audioSources.Find(a => a.AudioSourceType == playType).PlaySound(audio, t, OnProjectedFinish);
        }
    }
}