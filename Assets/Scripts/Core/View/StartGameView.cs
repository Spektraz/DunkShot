using Core.ServiceLayer;
using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class StartGameView : MVC.View.View
    {
        [SerializeField] private Canvas timeGameCanvas;
        public void SetCanvas(bool state)
        {
            timeGameCanvas.enabled = state;
        }
        protected override IController CreateController() => new StartGameController(this);
    }
    public class StartGameController : Controller<StartGameView,StartGameServiceLayer>
    {
        public StartGameController(StartGameView view) : base(view)
        {
        }

        protected override void HandleServiceLayer()
        {
           View.SetCanvas(serviceLayer.GetContext());
        }
    }
}