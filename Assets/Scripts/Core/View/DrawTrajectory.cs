using System;
using System.Collections;
using System.Collections.Generic;
using MVC.Controller;
using MVC.View;
using UnityEngine;

namespace Core.View
{
    public class DrawTrajectory : MVC.View.View
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] [Range(3, 30)] private int lineSegmentCount = 20;
        [SerializeField] [Range(10, 100)] private int showPercentage = 50;
        [SerializeField] private int linePointCount;
        private List<Vector3> linePoints = new List<Vector3>();
        public static DrawTrajectory Instance;

        // protected override void Start()
        // {
        //     base.Start();
        //     linePointCount = (int) (lineSegmentCount * (showPercentage / 100f));
        // }
        private void Awake()
        {
            Instance = this;
        }

        public void UpdateTrajectory(Vector3 forceVector3, Rigidbody2D rigidbody2D, Vector3 startingPoint)
       {
           Vector3 velocity = (forceVector3 / rigidbody2D.mass) * Time.fixedDeltaTime;
           float flightDuration = (2 * velocity.y); /// Physics.gravity.y;
           float stepTime = flightDuration / lineSegmentCount;
           linePoints.Clear();
           startingPoint = new Vector3(startingPoint.x -10, startingPoint.y, 0);
        //   linePoints.Add(startingPoint);
           for (int i = 0; i < linePointCount; i++)
           {
               float stepTimePassed = stepTime * i;
               Vector3 movementVector3 = new Vector3(
                   velocity.x * stepTimePassed,
                   velocity.y * stepTimePassed - 0.5f  * 100 * stepTimePassed);
              // Vector3 NewPointOnLine = -movementVector3 + startingPoint;
               // RaycastHit hit;
               // if (Physics.Raycast(linePoints[i - 1], NewPointOnLine - linePoints[i - 1], out hit,
               //         (NewPointOnLine - linePoints[i - 1]).magnitude))
               // {
               //     linePoints.Add(hit.point);
               //     break;
               // }
               // Debug.DrawLine(linePoints[i-1], NewPointOnLine, Color.magenta, 0.0f, true);
               //     linePoints.Add(NewPointOnLine);
               linePoints.Add(movementVector3 + startingPoint);
           }
           
           lineRenderer.positionCount = linePoints.Count;
           lineRenderer.SetPositions(linePoints.ToArray());
       }

       public void HideLine()
       {
           lineRenderer.positionCount = 0;
       }
        protected override IController CreateController() => new BallShootController(this);
        
    }
    public class BallShootController : Controller<DrawTrajectory>
    {
        public BallShootController(DrawTrajectory view) : base(view)
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