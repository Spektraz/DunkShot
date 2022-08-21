using Core.ServiceLayer;
using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class CameraMoveView : MVC.View.View
    {
        [SerializeField] private Transform ballTransform;
        [SerializeField] private Transform backCanvas;
        private bool setCamera;

        public void Update()
        {
            if (!setCamera) return;
            backCanvas.transform.position =
                new Vector3(0, (ballTransform.transform.position.y + 50)  * Time.deltaTime);
                
            Vector3 interpolatedPosition = Vector3.Lerp(backCanvas.transform.position, ballTransform.transform.position, 100);
            backCanvas.transform.position = new Vector3(0, interpolatedPosition.y);
        }

        public void SetCamera(bool state)
        {
            setCamera = state;
        } 
        protected override IController CreateController() => new CameraMoveController(this);
    }

    public class CameraMoveController : Controller<CameraMoveView, EndDragServiceLayer>
    {
        public CameraMoveController(CameraMoveView view) : base(view)
        {
        }
        protected override void HandleServiceLayer()
        {
            View.SetCamera(serviceLayer.GetContext());
        }
    }
}