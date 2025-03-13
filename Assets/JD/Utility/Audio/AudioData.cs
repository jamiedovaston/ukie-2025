using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace JD.Utility.Audio
{
    [CreateAssetMenu(menuName = "Jamie Dovaston/Audio", fileName = "audio_NAME")]
    public class AudioData : ScriptableObject
    {
        [SerializeField] private string iD;
        public string ID { get { return iD; } }

        [SerializeField] private AudioClip clip;
        public AudioClip Clip { get { return clip; } }

        private static List<AudioData> all = null;
        public static AudioData Get(string _id)
        {
            if(all == null)
                all = Resources.LoadAll<AudioData>("").ToList();

            AudioData audio = all.Find(a => a.ID == _id);

            Debug.Assert(audio != null, $"Couldn't find audio with ID: {_id}");

            return audio;
        }
    }
}