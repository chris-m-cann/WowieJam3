using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Util
{
    public static class AudioEx
    {

        public static void SetClipDetails(this AudioSource source, AudioClipEx clipEx)
        {
            source.clip = clipEx.clip ?? source.clip;
            source.volume = clipEx.volume;
            source.outputAudioMixerGroup = clipEx.mixer ?? source.outputAudioMixerGroup;
        }
    }

    [Serializable]
    public class AudioClipEx
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume = 1f;
        public AudioMixerGroup mixer;
    }
}