using System;
using MVC.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.View
{
   public class VibrationView : MVC.View.View
   {
 
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
      private static AndroidJavaObject vibrator;
#endif
      [SerializeField] private Slider vibrationSlider;

      public void AddListener(Action action)
      {
         vibrationSlider.onValueChanged.AddListener(delegate { action(); });
      }
      public void RemoveListener()
      {
         vibrationSlider.onValueChanged.RemoveAllListeners();
      }
      public void VibrationBarSet()
      {
         if (vibrationSlider.value == 1)
         {
            Handheld.Vibrate();
         }

         if (vibrationSlider.value != 0) return;
         if(IsAndroid())
            vibrator.Call("cancel");
      }

      public static bool IsAndroid()
      {
#if UNITY_ANDROID
         return true;
#else 
      return false;
#endif
      }

      protected override IController CreateController() => new VibrationController(this);
   }

   public class VibrationController : Controller<VibrationView>
   {
      public VibrationController(VibrationView view) : base(view)
      {
      }

      public override void AddListeners()
      {
         View.AddListener(View.VibrationBarSet);
      }

      public override void RemoveListeners()
      {
         View.RemoveListener();
      }
   }
}