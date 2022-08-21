using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class BallView : MVC.View.View
    {
        [SerializeField] private Rigidbody2D rigidbody2DBall;
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform ballPos;
        private void SetBall(string ballTag)
        {
            if (string.CompareOrdinal(ballTag, "Point")==0)
            {
                rigidbody2DBall.constraints = RigidbodyConstraints2D.FreezePositionY;
                ballPos.localPosition =startPos.localPosition;
            }
            else if (string.CompareOrdinal(ballTag, "Basket")==0)
            {
                rigidbody2DBall.constraints = RigidbodyConstraints2D.None;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Controller.Execute();
            SetBall(col.tag);
        }

        protected override IController CreateController() => new BallController(this);
    }
    public class BallController : Controller<BallView>
    {
        public BallController(BallView view) : base(view)
        {
        }

        public override void AddListeners()
        {
            
        }

        public override void RemoveListeners()
        {
          
        }

        public override void Execute()
        {
            base.Execute();
            ServiceFactory.GetService<EndDragServiceLayer>().UpdateDto(false);
        }
    }
}