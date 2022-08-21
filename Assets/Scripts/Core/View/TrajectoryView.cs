using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class TrajectoryView : MVC.View.View
    {
        [SerializeField] private AddScoreBasketView addScoreBasketView;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Transform shotPoint;
        [SerializeField] private float launchForce;
        [SerializeField] private float spaceBetweenPoints;
        [SerializeField] private int numberOfPoints;
        [SerializeField] private GameObject point;
        private GameObject[] points;
        private Vector2 direction;
        private Vector2 posVector;
        private bool isDraw;
        public void OnEnable()
        {
            canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>(); // refactor
        }
        public void CreatePoints()
        {
            points = new GameObject[numberOfPoints];
            for (var i = 0; i < numberOfPoints; i++)
            {
                points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
                points[i].transform.parent = canvas.transform;
                points[i].transform.localScale = new Vector3(0.5f, 0.5f);
            }
            isDraw = true;
        }

        public void DeletePoints()
        {
            if(points == null) return;
            foreach (var var in points)
            {
                var.SetActive(false);
            }
            isDraw = false;
        }
        void Update()
        {
            if (!isDraw) return;
            Controller.Execute();
            direction = posVector - (Vector2) gameObject.transform.position;
            for (var i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }

        public void SetPos(Vector2 vector2)
        {
            posVector = vector2;
        }
        private Vector2 PointPosition(float pos)
        {
            var position = (Vector2) shotPoint.position + (direction.normalized * launchForce * pos);
            return position;
        }
        protected override IController CreateController() => new TrajectoryController(this, addScoreBasketView);
    
    }

    public class TrajectoryController : Controller<TrajectoryView, IsAddScoreServiceLayer>
    {
        private readonly AddScoreBasketView addScoreBasketView;
        public TrajectoryController(TrajectoryView view, AddScoreBasketView addScoreBasketView) : base(view)
        {
            this.addScoreBasketView = addScoreBasketView;
            if (addScoreBasketView.isGoal)
            {
                View.CreatePoints();
            }
        }
        
        protected override void HandleServiceLayer()
        {
            if (!addScoreBasketView.isGoal) return;
            ServiceFactory.GetService<DeleteLineServiceLayer>().UpdateDto(true);
            View.CreatePoints();
        }

        public override void Execute()
        {
            base.Execute();
            var trajectoryServiceLayer = ServiceFactory.GetService<TrajectoryServiceLayer>();
            var pos = trajectoryServiceLayer.GetContext();
            View.SetPos(pos);
        }
    }
}