using System;
using System.Collections.Generic;
using BaseService.AudioEntity.Model;
using MVC.Model;
using UnityEngine;
using Utils;

namespace Meta.AudioEntity.Meta.Model
{
    [CreateAssetMenu(fileName = "AudioModel", menuName = "Model/Meta/AudioModel")]
    public class AudioModel :ScriptableObject,  IAudioModel<AudioId>
    {
        [SerializeField] private AudioDataList audioDataList;

        public AudioPreset GetById(AudioId id) => audioDataList.GetById(id);
        
        public List<AudioPreset> GetAudio()
        {
            List<AudioPreset> audioPresets = new List<AudioPreset>();
            foreach (var variable in   audioDataList.Values)
            {
                audioPresets.Add(variable);
            }
            return audioPresets;
        }

        public AudioPreset GetById(int id)
        {
            AudioId result = (AudioId) id;
            return GetById((AudioId) id);
        }
    }

    [Serializable]
    public class AudioDataList: DataList<InternalAudioData, AudioPreset, AudioId>{}
    
    [Serializable]
    public class InternalAudioData : InternalData<AudioId, AudioPreset> {}
    public interface IAudioModel<T>:IModel 
    {
        AudioPreset GetById(T id);
    }
}
