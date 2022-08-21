using System;
using BaseService.AudioEntity.Context;
using UnityEngine;

namespace BaseService.AudioEntity.Model
{
    [Serializable]
    public struct AudioPreset
    {
        [SerializeField] private bool isLoop;
        [SerializeField] private AudioClip clip; 
        [Range(0.1f, 1f)] [SerializeField] private float volume;

        public bool IsLoop => isLoop;

        public AudioClip Clip => clip;

        public float Volume => volume;


        public AudioContext ToContext() => new AudioContext(isLoop, clip, volume);
    }
}