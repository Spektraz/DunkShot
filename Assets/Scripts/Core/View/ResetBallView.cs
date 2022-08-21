using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class ResetBallView : MVC.View.View
    {
        [SerializeField] private Rigidbody2D rigidbody2DBall;

        public void OnEnable()
        {
            rigidbody2DBall = gameObject.GetComponent<Rigidbody2D>();
        }

        public void SetBall()
        {
            rigidbody2DBall.simulated = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Controller.Execute();
        }

        protected override IController CreateController() => new ResetBallController(this);
    }
    public class ResetBallController : Controller<ResetBallView>
    {
        public ResetBallController(ResetBallView view) : base(view)
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
            View.SetBall();
        }
    }
}
