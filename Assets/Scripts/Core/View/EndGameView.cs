using BaseService.RegistryEntity.Service;
using Core.ServiceLayer;
using Meta.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class EndGameView : MVC.View.View
    {
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Ball"))
            {
              Controller.Execute();  
            }
        }

        protected override IController CreateController() => new EndGameController(this);
    }

    public class EndGameController : Controller<EndGameView, IsAddScoreServiceLayer>
    {
        private readonly HowManyScoreServiceLayer howManyScoreServiceLayer;
        private readonly BestScoreSaveServiceLayer bestScoreSaveServiceLayer;
        public EndGameController(EndGameView view) : base(view)
        {
            howManyScoreServiceLayer = ServiceFactory.GetService<HowManyScoreServiceLayer>();
            bestScoreSaveServiceLayer = ServiceFactory.GetService<BestScoreSaveServiceLayer>();
        }

        public override void AddListeners()
        {
        }

        public override void RemoveListeners()
        {
        }

        protected override void HandleServiceLayer()
        {
        }

        public override void Execute()
        {
            base.Execute();
            ServiceFactory.GetService<FlyServiceLayer>().UpdateDto(true);
            if (serviceLayer.GetContext() == 0) return;
            if(howManyScoreServiceLayer.GetContext() > bestScoreSaveServiceLayer.GetContext()) 
                RegistryService.Save(howManyScoreServiceLayer.GetContext());
            ServiceFactory.GetService<EndGameServiceLayer>().UpdateDto(true);
        }
    }
}