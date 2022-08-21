using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class TakeDiamondView : MVC.View.View
    {
        [SerializeField] private Image diamond;
        [SerializeField] private Collider2D collider2D;

        public void OnEnable()
        {
            collider2D = gameObject.GetComponent<Collider2D>();
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Ball")) return;
            diamond.enabled = false;
            collider2D.enabled = false;
            Controller.Execute();
        }

        protected override IController CreateController() => new TakeDiamondController(this);

    }

    public class TakeDiamondController : Controller<TakeDiamondView,DiamondTakeServiceLayer>
    {
        public TakeDiamondController(TakeDiamondView view) : base(view)
        {
        }
        protected override void HandleServiceLayer()
        {
        }
        public override void Execute()
        {
            base.Execute();
            ServiceFactory.GetService<DiamondTakeServiceLayer>().UpdateDto(1);
        }
    }
}