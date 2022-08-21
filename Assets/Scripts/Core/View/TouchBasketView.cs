using Core.AudioEntity.ServiceLayer;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Core.View
{
   public class TouchBasketView : MVC.View.View, IDragHandler, IEndDragHandler
   {
      [SerializeField] private AddScoreBasketView addScoreBasketView;
      [SerializeField] private Rigidbody2D ball;
      [SerializeField] private Transform transformRot;
      [SerializeField] private Image basketSprite;
      [SerializeField] private BasketType basketType;
      private IsAddScoreServiceLayer isAddScoreServiceLayer;
      private bool isSetRotate;
      private bool isInterpolation;
      private bool isShoot;
      public void OnEnable()
      {
         ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();// delete and refactor
      }
      protected override void Start()
      {
         base.Start();
         isAddScoreServiceLayer = ServiceFactory.GetService<IsAddScoreServiceLayer>();
      }
      public void Update()
      {
         if (!isInterpolation) return;
         var basketSpriteTransform = basketSprite.transform;
         var localScale = basketSpriteTransform.localScale;
         var transformLocalScale = localScale;
         var local = new Vector2(transformLocalScale.x, transformLocalScale.y = 1.22f);
         localScale = Vector3.Lerp(localScale, local, 15 * Time.deltaTime);
         basketSpriteTransform.localScale = localScale;
         transformRot.rotation = Quaternion.Lerp(transformRot.rotation, Quaternion.identity, 15 * Time.deltaTime);
      }

      public void OnDrag(PointerEventData eventData)
      {
         if (!addScoreBasketView.isGoal) return;
         if(basketType == BasketType.StartBasket && isAddScoreServiceLayer.GetContext() != 0) return;
         var basketSpriteTransform = basketSprite.transform;
         var transformLocalScaleY = basketSpriteTransform.localScale.y;
         transformLocalScaleY = transformLocalScaleY /1;
         var transformLocal = new Vector2(eventData.pressPosition.x, eventData.pressPosition.y + transformLocalScaleY);
         ServiceFactory.GetService<TrajectoryServiceLayer>().UpdateDto(transformLocal);
         ServiceFactory.GetService<StartGameServiceLayer>().UpdateDto(false);
         isInterpolation = false;
         isShoot = false;
         var transformLocalScale = basketSpriteTransform.localScale;
         if (transformLocalScale.y < 1.05f)
         {
            basketSpriteTransform.localScale = new Vector2(transformLocalScale.x, 1.05f);
            return;
         }

         if (transformLocalScale.y > 2.02)
         {
            basketSpriteTransform.localScale = new Vector2(transformLocalScale.x, 2.02f);
            return;
         }

         basketSpriteTransform.localScale = new Vector2(transformLocalScale.x,
            transformLocalScale.y -= eventData.delta.y * Time.deltaTime / 2);
         if (!isSetRotate)
            transformRot.Rotate(0, 0, eventData.delta.x);
      }
      public void OnEndDrag(PointerEventData eventData)
      {
         if (!addScoreBasketView.isGoal) return;
         var basketSpriteTransform = basketSprite.transform;
         var transformLocalScale = basketSpriteTransform.localScale.y;
         transformLocalScale = transformLocalScale /1;
         isInterpolation = true;
         ServiceFactory.GetService<EndDragServiceLayer>().UpdateDto(true);
         ServiceFactory.GetService<AttackBallServiceLayer>().UpdateDto(true);
         Shoot(eventData.pressPosition * transformLocalScale);
         if(basketType == BasketType.StartBasket) return;
         addScoreBasketView.isGoal = false;
      }

      private void Shoot(Vector3 force)
      {
         if (isShoot)
            return;
         ball.AddForce(new Vector3(-force.x, -force.y, -force.z));
         isShoot = true;
      }
      private void OnTriggerStay2D(Collider2D other)
      {
         if (string.CompareOrdinal(other.tag, "EndRotate") == 0)
            isSetRotate = true;
      }
      private void OnTriggerExit2D(Collider2D other)
      {
         if (string.CompareOrdinal(other.tag, "EndRotate") == 0)
            isSetRotate = false;
      }
      protected override IController CreateController() => new TouchBasketController(this);
   }

   public class TouchBasketController : Controller<TouchBasketView>
   {
      public TouchBasketController(TouchBasketView view ): base(view)
      {
      }

      public override void AddListeners()
      {
      }

      public override void RemoveListeners()
      {
      }
      
   }

   public enum BasketType
   {
      Unset = 0,
      StartBasket = 1, 
      OtherBasket = 2,
   }
}