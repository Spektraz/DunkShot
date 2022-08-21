using System;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class BallView : MVC.View.View
    {
        [SerializeField] private Rigidbody2D rigidbody2DBall;
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform ballPos;
        private void SetBall(string tag)
        {
            if (string.CompareOrdinal(tag, "Point")==0)
            {
                rigidbody2DBall.constraints = RigidbodyConstraints2D.FreezePositionY;
                ballPos.localPosition =startPos.localPosition;
            }
            else if (string.CompareOrdinal(tag, "Basket")==0)
            {
                rigidbody2DBall.constraints = RigidbodyConstraints2D.None;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            ServiceFactory.GetService<EndDragServiceLayer>().UpdateDto(false);
            SetBall(col.tag);
        }

        protected override IController CreateController() => new BallController(this);
    }
    public class BallController : Controller<BallView>
    {
        public BallController(BallView view) : base(view)
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