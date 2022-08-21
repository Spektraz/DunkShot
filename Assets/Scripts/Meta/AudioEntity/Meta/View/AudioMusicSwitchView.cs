using Meta.AudioEntity.Meta.Service;
using MVC.Controller;
using UnityEngine;

namespace Meta.AudioEntity.Meta.View
{
    public class AudioMusicSwitchView : MVC.View.View
    {
        [SerializeField] private AudioButtonView audioButtonView;

        
 
        protected override IController CreateController() => new AudioSwitchController(this, audioButtonView);
    }
    public class AudioSwitchController : Controller<AudioMusicSwitchView, AudioSwitchServiceLayer>
    {
        private readonly AudioButtonView audioButtonViews;
        private AudioTypeMuteContext context;


        public AudioSwitchController(AudioMusicSwitchView view, AudioButtonView audioButtonViews) : base(view)
        {
            this.audioButtonViews = audioButtonViews;
        }

        public override void AddListeners()
        {
            base.AddListeners();
            audioButtonViews.AddListener(SwitchAudioState);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            audioButtonViews.RemoveListener();
        }
        
        private void SwitchAudioState()
        {
            context.SwitchState();
            serviceLayer.UpdateDto(context.AudioState);
        }

        protected override void HandleServiceLayer()
        {
            context = serviceLayer.GetContext();
            audioButtonViews.Context(context.GetState(AudioGenreType.Audio));
        }
    }
    public enum AudioGenreType
    {
        Audio = 0,
    }
}