using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JD.Utility.Audio
{
    public class JD_AudioSourceDynamic : MonoBehaviour
    {
        public int channelCount;
        private List<JD_Audio> channels = new List<JD_Audio>();

        private void Awake()
        {
            for (int i = 0; i < channelCount; i++)
            {
                GameObject g = new GameObject();
                g.transform.SetParent(transform);
                g.name = $"AudioChannel ({i + 1})";

                JD_Audio channel = g.AddComponent<JD_Audio>();

                channels.Add(channel);
            }
        }

        public void PlaySound(AudioData audio, Vector3 p, Action OnProjectedFinish) => InitialiseSoundAgent(audio, OnProjectedFinish).transform.position = p;

        public void PlaySound(AudioData audio, Transform t, Action OnProjectedFinish) => InitialiseSoundAgent(audio, OnProjectedFinish).transform.SetParent(t);

        public GameObject InitialiseSoundAgent(AudioData clip, Action OnProjectedFinish)
        {
            JD_Audio channel = new GameObject().AddComponent<JD_Audio>();
            channel.Play(clip, OnProjectedFinish);
            Destroy(channel.gameObject, channel.GetAudioSource().clip.length);
            return channel.gameObject;
        }

        public JD_Audio GetFirstEmptyChannel()
        {
            for (int i = 0; i < channels.Count; i++)
            {
                if (!channels[i].GetAudioSource().isPlaying)
                {
                    return channels[i];
                }
            }
            Debug.LogError($"Static audio overloaded! Channels : {channels.Count}", this);
            return null;
        }
    }
}