using System;
using MVC.Controller;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.View
{
    public class NightModeView : MVC.View.View
    {  
        [SerializeField] private Slider nightModeSlider;
        [SerializeField] private Camera mainCamera;
        public void AddListener(Action action)
        {
            nightModeSlider.onValueChanged.AddListener(delegate { action(); });
        }
        public void RemoveListener()
        {
            nightModeSlider.onValueChanged.RemoveAllListeners();
        }

        public void SetCamera()
        {
            if (nightModeSlider.value == 1)
            {
                mainCamera.backgroundColor = new Color(0.1f, 0.1f, 0.1f);
            }
            if (nightModeSlider.value == 0)
            {
                mainCamera.backgroundColor = Color.gray;
            }
        }

        protected override IController CreateController() => new NightModeController(this);
    }
    public class NightModeController : Controller<NightModeView>
    {
        public NightModeController(NightModeView view) : base(view)
        {
        }

        public override void AddListeners()
        {
            View.AddListener(View.SetCamera);
        }

        public override void RemoveListeners()
        {
          View.RemoveListener();
        }
    }
}
