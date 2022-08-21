using System;
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
