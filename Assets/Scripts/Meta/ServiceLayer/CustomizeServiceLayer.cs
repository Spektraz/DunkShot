using BaseService.ModelEntity;
using Meta.Model;
using MVC.Service;
using UnityEngine;

namespace Meta.ServiceLayer
{
    public class CustomizeServiceLayer: ServiceLayer<CustomizeBallModel, int, MainMenuCustomizeBallContext>
    {
        public int GetBallPriceModel(BallsName ballsName) =>
            ModelService.GetModel<CustomizeBallModel>().GetByName(ballsName).PriceBall;

        public Sprite GetBallSpriteModel(BallsName ballsName) =>
            ModelService.GetModel<CustomizeBallModel>().GetByName(ballsName).BallSprite;
        public override MainMenuCustomizeBallContext GetContext()
        {
            return new MainMenuCustomizeBallContext
            {
                
            };

        }
    }
    public class MainMenuCustomizeBallContext
    {
    }
}