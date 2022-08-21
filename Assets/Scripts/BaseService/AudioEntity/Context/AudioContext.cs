using Meta.AudioEntity.Meta.View;
using UnityEngine;

namespace BaseService.AudioEntity.Context
{
    public class AudioContext
    {
        public bool IsLoop { get; }
        public AudioClip Clip { get; }

        public float Volume { get; }
        public AudioGenreType AudioGenreType { get; }

        public AudioContext(bool isLoop, AudioClip clip, float volume)
        {
            IsLoop = isLoop;
            Clip = clip;
            Volume = volume;
            AudioGenreType = AudioGenreType.Audio;
        }
    }
}