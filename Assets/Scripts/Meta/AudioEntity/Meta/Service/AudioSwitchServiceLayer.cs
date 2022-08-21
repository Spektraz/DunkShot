using System.Collections.Generic;
using BaseService.RegistryEntity.Service;
using Meta.AudioEntity.Meta.Model;
using Meta.AudioEntity.Meta.View;
using MVC.Service;

namespace Meta.AudioEntity.Meta.Service
{
    public class AudioSwitchServiceLayer : ServiceLayer<AudioModel, IAudioState, AudioTypeMuteContext>
    {
        public override AudioTypeMuteContext GetContext()
        {
            return new AudioTypeMuteContext(dto);
        }
    }

    public class AudioTypeMuteContext
    {
        public IAudioState AudioState => new AudioStateImpl(AudioStateDictionary);
        private Dictionary<AudioGenreType, bool> AudioStateDictionary { get; set; }

        public AudioTypeMuteContext(IAudioState audioState)
        {
            AudioStateDictionary = new Dictionary<AudioGenreType, bool>
            {
                {AudioGenreType.Audio, audioState.IsAudioActive},
            };
            RegistryService.Save(AudioState);
        }
        

        public bool GetState(AudioGenreType audioGenreType)
        {
            return ContainsKey(audioGenreType) && AudioStateDictionary[audioGenreType];
        }

        private bool ContainsKey(AudioGenreType audioGenreType)
        {
            return AudioStateDictionary.ContainsKey(audioGenreType);
        }

        public void SwitchState()
        {
            AudioStateDictionary[AudioGenreType.Audio] = !AudioStateDictionary[AudioGenreType.Audio];
        }
    }

    public interface IAudioState
    {
        bool IsAudioActive { get; }
    }

    public class AudioStateImpl : IAudioState
    {
        public bool IsAudioActive { get; }
        public AudioStateImpl()
        {
            IsAudioActive = true;
        }
    
        public AudioStateImpl(bool isAudioActive, bool isMusicActive)
        {
            IsAudioActive = isAudioActive;
        }
    
        public AudioStateImpl(Dictionary<AudioGenreType, bool> audioStateDictionary)
        {
            IsAudioActive = audioStateDictionary[AudioGenreType.Audio];
        }
    }
}