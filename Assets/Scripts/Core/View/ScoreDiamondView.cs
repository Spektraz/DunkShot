using BaseService.RegistryEntity.Service;
using Core.ServiceLayer;
using Meta.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class ScoreDiamondView : MVC.View.View
    {
        [SerializeField] private Text diamondScore;
        public void SetScore(int score)
        {
            diamondScore.text = score.ToString();
        }
        protected override IController CreateController() => new ScoreDiamondController(this);
    }

    public class ScoreDiamondController : Controller<ScoreDiamondView, DiamondTakeServiceLayer>
    {
        private DiamondAllScoreServiceLayer diamondAllScoreServiceLayer;
        private ChangeBallServiceLayer changeBallServiceLayer;
        public ScoreDiamondController(ScoreDiamondView view) : base(view)
        {
            diamondAllScoreServiceLayer = ServiceFactory.GetService<DiamondAllScoreServiceLayer>();
            changeBallServiceLayer = ServiceFactory.GetService<ChangeBallServiceLayer>();
            View.SetScore(diamondAllScoreServiceLayer.GetContext());
        }

        public override void AddListeners()
        {
            base.AddListeners();
            changeBallServiceLayer.DtoHandler.AddListener(HandleDiamondServiceLayer);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            changeBallServiceLayer.DtoHandler.RemoveListener(HandleDiamondServiceLayer);
        }

        private void HandleDiamondServiceLayer()
        {
            View.SetScore(diamondAllScoreServiceLayer.GetContext());
        }
        protected override void HandleServiceLayer()
        {
            var context =  diamondAllScoreServiceLayer.GetContext() + serviceLayer.GetContext();
            ServiceFactory.GetService<DiamondAllScoreServiceLayer>().UpdateDto(context);
            RegistryService.SaveDiamond(context);
            View.SetScore(diamondAllScoreServiceLayer.GetContext());
        }
    }
}