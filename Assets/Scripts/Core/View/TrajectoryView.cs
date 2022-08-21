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
        public Transform shotPoint;
        public float launchForce;

        public GameObject point;
        private GameObject[] points;
        public int numberOfPoints;
        public float spaceBetweenPoints;
        private Vector2 direction;
        private bool isDraw;
        public void OnEnable()
        {
            canvas = GameObject.FindGameObjectWithTag("GameController").GetComponent<Canvas>();
        }
        public void CreatePoints()
        {
            points = new GameObject[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
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
            var trajectoryServiceLayer = ServiceFactory.GetService<TrajectoryServiceLayer>();
            var pos = trajectoryServiceLayer.GetContext();
            direction = pos - (Vector2) gameObject.transform.position;
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
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
        private AddScoreBasketView addScoreBasketView;
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
            Debug.Log("Create");
            View.CreatePoints();
        }
    }
}