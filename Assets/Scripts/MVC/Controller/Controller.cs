using BaseService.AudioEntity.Context;
using BaseService.AudioEntity.Model;
using BaseService.AudioEntity.View;
using BaseService.ModelEntity;
using Meta.AudioEntity.Meta.Model;
using Meta.AudioEntity.Meta.Service;
using MVC.Factory;
using MVC.Service;

namespace MVC.Controller
{
    public abstract class Controller<T> : IController where T : View.View
    {

        protected T View { get; }

        public Controller(T view)
        {
            this.View = view;
        }

        public abstract void AddListeners();
        public abstract void RemoveListeners();

        public virtual void Execute()
        {
        }
    }

    public abstract class Controller<T, Layer> : Controller<T> where T : View.View where Layer : BaseServiceLayer
    {
        protected readonly Layer serviceLayer;

        protected Controller(T view) : base(view)
        {
            serviceLayer = ServiceFactory.GetService<Layer>();
        }

        public override void AddListeners()
        {
            serviceLayer.DtoHandler.AddListener(HandleServiceLayer);
        }

        public override void RemoveListeners()
        {
            RemoveServiceLayerListener();
        }

        protected abstract void HandleServiceLayer();

        protected void RemoveServiceLayerListener() => serviceLayer.DtoHandler.RemoveListener(HandleServiceLayer);
    }
    public interface IController
    {
        void AddListeners();
        void RemoveListeners();
        void Execute(); 
    }
        public abstract class BaseAudioController<T, M, ID, SERVICE> : BaseAudioController<T, M,ID>
        where T : BaseAudioView<ID> where M : IAudioModel<ID> where SERVICE : BaseServiceLayer
    {
        protected readonly SERVICE customService;
        protected BaseAudioController(T view) : base(view)
        {
            customService = ServiceFactory.GetService<SERVICE>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
            customService.DtoHandler.AddListener(HandleCustomServiceLayer);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            customService.DtoHandler.RemoveListener(HandleCustomServiceLayer);
        }

        protected abstract void HandleCustomServiceLayer();
        protected virtual void RemoveCustomServiceLayerListener() => customService.DtoHandler.RemoveListener(HandleCustomServiceLayer);

    }
    

    public abstract class BaseAudioController<T, M, ID> : Controller<T, AudioSwitchServiceLayer>
        where T : BaseAudioView<ID> where M : IAudioModel<ID>
    {
        protected AudioContext audioContext;
        protected M Model { get; }
        protected virtual bool PlayOnAwake { get; }

        protected BaseAudioController(T view) : base(view)
        {
            Model = ModelService.GetModel<M>();
        }

        protected sealed override void HandleServiceLayer()
        {
            var context = serviceLayer.GetContext();
            View.SetMute(!context.GetState(audioContext.AudioGenreType));
        }

        public virtual void Play() => View.PlayAudio();

        public virtual void Stop() => View.StopAudio();
        public virtual void Pause() => View.PauseAudio();
        public virtual void UnPause() => View.UnPauseAudio();


        public override void Execute()
        {
            base.Execute();
            Play();
        }
    }

    public abstract class AudioMetaController<T, M> : BaseAudioController<T, M, AudioId>
        where M : IAudioModel<AudioId> where T : BaseAudioView<AudioId>
    {
        protected AudioMetaController(T view) : base(view)
        {
            audioContext = Model.GetById(View.Id).ToContext();
            View.SetContext(audioContext);
            if (PlayOnAwake)
            {
                Play();
            }
        }
    }

    public abstract class AudioMetaController<T, M, Layer> : BaseAudioController<T, M, AudioId, Layer>
        where M : IAudioModel<AudioId> where T : BaseAudioView<AudioId> where Layer : BaseServiceLayer
    {

        protected AudioMetaController(T view) : base(view)
        {
            audioContext = Model.GetById(View.Id).ToContext();
            View.SetContext(audioContext);
            if (PlayOnAwake)
            {
                Play();
            }
        }

    }

}