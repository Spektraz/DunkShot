using System.Collections.Generic;
using Core.ServiceLayer;
using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class CreateBasketView : MVC.View.View
    {
        [SerializeField] private GameObject basketObject;
        [SerializeField] private Canvas canvas;
        [SerializeField] private List<GameObject> basketList;
        private List<TrajectoryView> trajectoryView = new List<TrajectoryView>();
        protected override void Start()
        {
            base.Start();
            var spawnPosition = new Vector3();
            for (var i = 0; i < 10; i++)
            {
                spawnPosition.x = Random.Range(-1.7f, 1.7f);
                spawnPosition.y = Random.Range(spawnPosition.y + 1, spawnPosition.y + 2);
                var basketInst = Instantiate(basketObject, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
                basketInst.transform.parent = canvas.transform;
                basketInst.transform.localScale = Vector3.one;
                basketList.Add(basketInst);
                trajectoryView.Add(basketList[i].GetComponent<TrajectoryView>());
            }
        }
        protected override IController CreateController() => new CreateBasketController(this, trajectoryView);
    }
    public class CreateBasketController : Controller<CreateBasketView, DeleteLineServiceLayer>
    {
        private List<TrajectoryView> trajectoryView;
        public CreateBasketController(CreateBasketView view, List<TrajectoryView> trajectoryView) : base(view)
        {
            this.trajectoryView = trajectoryView;
        }
        

        protected override void HandleServiceLayer()
        {
            foreach (var var in trajectoryView)
            {
                var.DeletePoints();
            }
        }
    }
}
