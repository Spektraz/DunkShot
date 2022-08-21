using System;
using MVC.Model;
using UnityEngine;
using Utils;

namespace Meta.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "CustomizeBallModel", menuName = "Model/Meta/CustomizeBallModel")]
    public class CustomizeBallModel : ScriptableObject, IModel
    {
        [SerializeField] private BuyBallsList buyBallsList; 
        public InternalSkyPreset GetByName(BallsName ballsName) => buyBallsList.GetById(ballsName);
    }
    [Serializable]
    public class BuyBallsList : DataList<BuyBallsPreset, InternalSkyPreset, BallsName>{}
    
    [Serializable]
    public class BuyBallsPreset : InternalData<BallsName, InternalSkyPreset>{}

    [Serializable]
    public class InternalSkyPreset
    {        
        [SerializeField] private Sprite ballSprite;
        [SerializeField] private int priceBall;
        public Sprite BallSprite => ballSprite;
        public int PriceBall => priceBall;
    }
    public enum BallsName
    {
        Hobbies = 0,
        Baby = 1,
        Beach = 2,
        Bronze = 3, 
        Club = 4,
        Country = 5, 
        Damage = 6,
        Everywhere = 7, 
        Futsal = 8,
        HighTech = 9,
        Ice = 10,
        Room = 11,
        Unset = 999,
    }
}