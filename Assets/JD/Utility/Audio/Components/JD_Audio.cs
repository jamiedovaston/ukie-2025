using System;
using System.Collections;
using UnityEngine;


namespace JD.Utility.Audio
{
    public enum JD_AUDIOMODE { Static, Dynamic }

    [RequireComponent(typeof(AudioSource))]
    public class JD_Audio : MonoBehaviour
    {
        public AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

        public void Play(AudioData audio, Action OnProjectedFinish)
        {
            audioSource.clip = audio.Clip;
            audioSource.Play();
            StartCoroutine(C_PlaySoundWait(audio.Clip.length, OnProjectedFinish));
        }

        public void PlayOneShot(AudioClip clip, Action OnProjectedFinish)
        {
            audioSource.PlayOneShot(clip);
            StartCoroutine(C_PlaySoundWait(clip.length, OnProjectedFinish));
        }

        public IEnumerator C_PlaySoundWait(float length, Action OnFinish)
        {
            yield return new WaitForSeconds(length);
            OnFinish?.Invoke();
        }

        public AudioSource GetAudioSource() => audioSource;
    }
}
