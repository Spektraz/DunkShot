using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class CreateDiamondView : MVC.View.View
    {
        [SerializeField] private GameObject diamondObject;

        protected override void Start()
        {
            base.Start();
            SetRandom();
        }
        private void SetRandom()
        {
            var random = Random.Range(0, 2);
            diamondObject.SetActive(random == 1);
        }
        protected override IController CreateController() => new CreateDiamondController(this);
    }
    public class CreateDiamondController : Controller<CreateDiamondView>
    {
        public CreateDiamondController(CreateDiamondView view) : base(view)
        {
        }

        public override void AddListeners()
        {
            
        }

        public override void RemoveListeners()
        {
           
        }
    }
}