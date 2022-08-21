using Core.ServiceLayer;
using Meta.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.View
{
   public class BestScoreView : MVC.View.View
   {
      [SerializeField] private Text bestScoreText;
      public void SetText(int score)
      {
         bestScoreText.text = score.ToString();
      }
      protected override IController CreateController() => new BestScoreController(this);
   }

   public class BestScoreController : Controller<BestScoreView, HowManyScoreServiceLayer>
   {
      private readonly BestScoreSaveServiceLayer bestScoreSaveServiceLayer;
      public BestScoreController(BestScoreView view) : base(view)
      {
         bestScoreSaveServiceLayer = ServiceFactory.GetService<BestScoreSaveServiceLayer>();
      }
      
      protected override void HandleServiceLayer()
      {
         if (bestScoreSaveServiceLayer.GetContext() < serviceLayer.GetContext())
         {
            View.SetText(serviceLayer.GetContext());
            return;
         }
         View.SetText(bestScoreSaveServiceLayer.GetContext());
      }
   }
}