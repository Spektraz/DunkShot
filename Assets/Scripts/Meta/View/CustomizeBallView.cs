using System;
using BaseService.RegistryEntity.Service;
using Core.ServiceLayer;
using Meta.Model;
using Meta.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.View
{
    public class CustomizeBallView : MVC.View.View
    {
        [SerializeField] private Canvas priceCanvas;
        [SerializeField] private Button buyButton;
        [SerializeField] private Image ballImage;
        [SerializeField] private Text priceText;
        [SerializeField] private BallsName ballsName;
        public BallsName BallName()
        {
            return ballsName;
        }
        public void AddListener(Action action)
        {
            buyButton.onClick.AddListener(action.Invoke);
        }
        public void RemoveListener()
        {
            buyButton.onClick.RemoveAllListeners();
        }

        public void SetSprite(Sprite sprite)
        {
            ballImage.sprite = sprite;
        }

        public void SetText(int price)
        {
            priceText.text = price.ToString();
        }

        public void SetCanvas(bool state)
        {
            priceCanvas.enabled = state;
        }
        protected override IController CreateController() => new CustomizeBallController(this);

    }

    public class CustomizeBallController : Controller<CustomizeBallView, DiamondAllScoreServiceLayer>
    {
        private readonly CustomizeServiceLayer customizeServiceLayer;
        private int newScore;
        public CustomizeBallController(CustomizeBallView view) : base(view)
        {
            customizeServiceLayer = ServiceFactory.GetService<CustomizeServiceLayer>();
        }

        public override void AddListeners() 
        {
            View.AddListener(ClickButton);
            View.SetText(customizeServiceLayer.GetBallPriceModel(View.BallName()));
            View.SetSprite(customizeServiceLayer.GetBallSpriteModel(View.BallName()));
        }

        public override void RemoveListeners()
        {
            View.RemoveListener();
        }

        protected override void HandleServiceLayer()
        {
        }

        private void ClickButton()
        {
            if (serviceLayer.GetContext() <
                customizeServiceLayer.GetBallPriceModel(View.BallName())) return;
            newScore = serviceLayer.GetContext() -
                       customizeServiceLayer.GetBallPriceModel(View.BallName());
            serviceLayer.UpdateDto(newScore);
            RegistryService.SaveDiamond(newScore);
            View.SetCanvas(false);
            ServiceFactory.GetService<ChangeBallServiceLayer>().UpdateDto(View.BallName());
        }
    }
}