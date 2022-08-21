using BaseService.AudioEntity.Context;
using BaseService.AudioEntity.Model;
using UnityEngine;

namespace BaseService.AudioEntity.View
{
    public abstract class BaseMetaAudioView : BaseAudioView<AudioId>
    {

        
    }
    public abstract class BaseAudioView<T> : BaseAudioView 
    {
        [SerializeField] private T audioId;
        public T Id => audioId;
    }

    public abstract class BaseAudioView : MVC.View.View<AudioContext>
    {
        protected AudioSource audioSource;
        protected AudioSource Source
        {
            get
            {
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                return audioSource;
            }
        }

        private bool IsActive => gameObject.activeInHierarchy;
        public override void SetContext(AudioContext context)
        {
            if(!IsActive) return;
            
            Source.clip = context.Clip;
            Source.volume = context.Volume;
            Source.loop = context.IsLoop;

        }

        public void PlayAudio()
        {
            if(!IsActive) return;

            Source.Play();
        }
        public void StopAudio()
        {
            if(!IsActive) return;
            
            Source.Stop();
        }

        public void PauseAudio()
        {
            if(!IsActive) return;

            Source.Pause();
        }

        public void UnPauseAudio()
        {
            if(!IsActive) return;

            Source.UnPause();
        }

        public void SetMute(bool state)
        {
            if(!IsActive) return;
            audioSource.volume = state ? 0 : 1;
        }
    }
}
