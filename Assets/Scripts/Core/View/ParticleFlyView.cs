using Core.AudioEntity.ServiceLayer;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class ParticleFlyView : MVC.View.View
    {
        [SerializeField] private ParticleSystem particleSystem;

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
        protected override IController CreateController() => new ParticleFlyController(this);
    }

    public class ParticleFlyController : Controller<ParticleFlyView, AttackBallServiceLayer>
    {
         private IsAddScoreServiceLayer isAddScoreServiceLayer;
         private FlyServiceLayer flyServiceLayer;
        public ParticleFlyController(ParticleFlyView view) : base(view)
        {
            isAddScoreServiceLayer = ServiceFactory.GetService<IsAddScoreServiceLayer>();
            flyServiceLayer = ServiceFactory.GetService<FlyServiceLayer>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
            isAddScoreServiceLayer.DtoHandler.AddListener(HandleStopServiceLayer);
            flyServiceLayer.DtoHandler.AddListener(HandleStopServiceLayer);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            isAddScoreServiceLayer.DtoHandler.RemoveListener(HandleStopServiceLayer);
            flyServiceLayer.DtoHandler.RemoveListener(HandleStopServiceLayer);
        }

        private void HandleStopServiceLayer()
        {
            View.SetParticle(false);
        }
        protected override void HandleServiceLayer()
        {
           View.SetParticle(true);
        }
    }
}