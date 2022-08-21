using Core.ServiceLayer;
using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class ParticleGoalView  : MVC.View.View
    {
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private AddScoreBasketView addScoreBasketView;
        protected override void Start()
        {
            base.Start();
            SetParticle(false);
        }
        public void SetParticle(bool state)
        {
            if(state)
                particleSystem.Play();
            else 
                particleSystem.Stop();
        }
        protected override IController CreateController() => new ParticleGoalController(this, addScoreBasketView);
    }

    public class ParticleGoalController : Controller<ParticleGoalView, IsAddScoreServiceLayer>
    {
        private AddScoreBasketView addScoreBasketView;
        public ParticleGoalController(ParticleGoalView view, AddScoreBasketView addScoreBasketView) : base(view)
        {
            this.addScoreBasketView = addScoreBasketView;
        }
    
        protected override void HandleServiceLayer()
        {
            if(addScoreBasketView.isGoal)
                View.SetParticle(true);
        }
    }
}