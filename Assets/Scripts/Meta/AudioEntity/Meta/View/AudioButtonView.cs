using System;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.AudioEntity.Meta.View
{
    public class AudioButtonView : MonoBehaviour
    { 
        [SerializeField] private Slider vibrationSlider;

        public void Context(bool state)
        {
            vibrationSlider.value = state ? 0 : 1;
        }
        public void AddListener(Action action)
        {
            vibrationSlider.onValueChanged.AddListener(delegate { action(); });
        }
        public void RemoveListener()
        {
            vibrationSlider.onValueChanged.RemoveAllListeners();
        }
    }
}