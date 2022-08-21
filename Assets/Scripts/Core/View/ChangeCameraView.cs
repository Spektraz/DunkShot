
using Core.AudioEntity.ServiceLayer;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class ChangeCameraView : MVC.View.View
    {
        private const int DiamondPosY = 1590;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private Canvas gameCanvas;
        [SerializeField] private Transform mainTransform;
        [SerializeField] private Transform scorePos;
        [SerializeField] private Transform diamondPos;
        private bool state;
        public void Update()
        {
            var localPosition =  gameCamera.transform.localPosition;
            if (state)
            {
                var position = mainTransform.localPosition;
                var transform1 = scorePos.transform;
                var position1 = transform1.localPosition;
                var localPosition1 = diamondPos.transform.localPosition;
                gameCamera.transform.localPosition = new Vector3(localPosition.x,
                    position.y, localPosition.z);
                scorePos.localPosition =  new Vector3(position1.x,
                    position.y, position1.z);
                diamondPos.localPosition =  new Vector3(localPosition1.x,
                    position.y + DiamondPosY, localPosition1.z);
            }
        }

        public void SetCamera(bool state)
        {
            this.state = state;
            gameCanvas.worldCamera = state ? gameCamera : mainCamera;
            mainCamera.enabled = !state;
            gameCamera.enabled = state;
        }
        protected override IController CreateController() => new ChangeCameraController(this);
    }

    public class ChangeCameraController : Controller<ChangeCameraView, AttackBallServiceLayer>
    {
        private EndGameServiceLayer endGameServiceLayer;
        public ChangeCameraController(ChangeCameraView view) : base(view)
        {
            endGameServiceLayer = ServiceFactory.GetService<EndGameServiceLayer>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
            endGameServiceLayer.DtoHandler.AddListener(HandleStopServiceLayer);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            endGameServiceLayer.DtoHandler.RemoveListener(HandleStopServiceLayer);
        }

        private void HandleStopServiceLayer()
        {
            View.SetCamera(false);
        }
        protected override void HandleServiceLayer()
        {
            View.SetCamera(true);
        }
    }
}