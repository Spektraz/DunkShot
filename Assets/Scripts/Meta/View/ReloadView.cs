using System;
using BaseService.RegistryEntity.Service;
using Core.ServiceLayer;
using Meta.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Meta.View
{
    public class ReloadView : MVC.View.View
    {
        [SerializeField] private Button buttonReload;

        public void AddListener(Action action)
        {
            buttonReload.onClick.AddListener(action.Invoke); 
        }

        public void RemoveListener()
        {
            buttonReload.onClick.RemoveAllListeners();
        }
        protected override IController CreateController() => new ReloadController(this);
    }
    public class ReloadController : Controller<ReloadView, HowManyScoreServiceLayer>
    {
        public ReloadController(ReloadView view) : base(view)
        {
        }

        public override void AddListeners()
        {
            base.AddListeners();
            View.AddListener(ReloadScene);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            View.RemoveListener();
        }

        protected override void HandleServiceLayer()
        {
        }

        private void ReloadScene()
        {
            ServiceFactory.ResetServiceLayer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
