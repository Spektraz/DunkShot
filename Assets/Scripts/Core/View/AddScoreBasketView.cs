using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class AddScoreBasketView : MVC.View.View
    {
        private Collider2D collider2D;
        public bool isGoal;
        public void OnEnable()
        {
            collider2D = gameObject.GetComponent<Collider2D>();
        }
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Ball") || !collider2D.CompareTag("BasketCollider")) return;
            isGoal = true;
            Controller.Execute();
        }
        protected override IController CreateController() => new AddScoreBasketController(this);
    }
    public class AddScoreBasketController : Controller<AddScoreBasketView>
    {
        private bool isOpen;
        private const int ScoreAdd = 20;
        public AddScoreBasketController(AddScoreBasketView view) : base(view)
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
            if (!isOpen)
            {
                ServiceFactory.GetService<IsAddScoreServiceLayer>().UpdateDto(ScoreAdd);
            }
            isOpen = true;
        }
    }
}