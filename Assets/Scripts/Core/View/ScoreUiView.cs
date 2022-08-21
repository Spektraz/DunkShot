using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class ScoreUiView : MVC.View.View
    {
        [SerializeField] private Text scoreText;

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
        protected override IController CreateController() => new ScoreUiController(this);
    }
    public class ScoreUiController : Controller<ScoreUiView, IsAddScoreServiceLayer>
    {
        private int score;
        public ScoreUiController(ScoreUiView view) : base(view)
        {
        }

        protected override void HandleServiceLayer()
        {
           score += serviceLayer.GetContext();
           View.SetScore(score);
           ServiceFactory.GetService<HowManyScoreServiceLayer>().UpdateDto(score);
        }
    }
}