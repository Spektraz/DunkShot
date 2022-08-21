using Meta.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.View
{
    public class ChangeBallView : MVC.View.View
    {
        [SerializeField] private Image imageBall;

        public void SetBall(Sprite sprite)
        {
            imageBall.sprite = sprite;
        }

        protected override IController CreateController() => new ChangeBallController(this);
    }
    public class ChangeBallController : Controller<ChangeBallView, ChangeBallServiceLayer>
    {
        private readonly CustomizeServiceLayer customizeServiceLayer;
        public ChangeBallController(ChangeBallView view) : base(view)
        {
            customizeServiceLayer = ServiceFactory.GetService<CustomizeServiceLayer>();
        }
        protected override void HandleServiceLayer()
        {
            View.SetBall(customizeServiceLayer.GetBallSpriteModel(serviceLayer.GetContext()));
        }
    }
}
