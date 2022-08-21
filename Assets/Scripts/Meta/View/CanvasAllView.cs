using System;
using System.Collections.Generic;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using Utils;

namespace Meta.View
{
  public class CanvasAllView : MVC.View.View
  {
    [SerializeField] private List<CanvasButtons> canvasButtonList;
    private readonly Dictionary<CanvasType, CanvasButtons> canvasButtonsDictionary =
      new Dictionary<CanvasType, CanvasButtons>();

    private bool state;
    private Dictionary<CanvasType, CanvasButtons> CanvasButtonsDictionary
    {
      get
      {
        if(canvasButtonsDictionary.Count == 0)
        {
          canvasButtonList.ForEach(x => canvasButtonsDictionary.Add(x.Id, x));
        }
        return canvasButtonsDictionary;
      }
    }
    public void AddListener(CanvasType role, Action action)
    {
      if (!CanvasButtonsDictionary.ContainsKey(role))
      {
        Debug.LogError($"{role} is not found");
        return;
      }
      CanvasButtonsDictionary[role].AddListener(action);
    }
    public void RemoveAllListeners()
    {
      canvasButtonList.ForEach(x => x.RemoveListeners());
    }

    public void BlockCanvas()
    {
      state = true;
    }
    public void SetCanvas(CanvasType canvasType)
    {
      if (state)
      {
        if (canvasType == CanvasType.MainMenuCanvas)
        {
          canvasType = CanvasType.EndGameCanvas;
        }
      }
      foreach (var var in canvasButtonList)
      {
        var.SetCanvas(false);
      }
      
      canvasButtonsDictionary[canvasType].SetCanvas(true);
      
      if (canvasType == CanvasType.MainMenuCanvas)
        canvasButtonsDictionary[CanvasType.GameCanvas].SetCanvas(true);
      if (canvasType == CanvasType.ScoreCanvas)
        canvasButtonsDictionary[CanvasType.GameCanvas].SetCanvas(true);
    }
    protected override IController CreateController() => new CanvasAllController(this);

  }
  [Serializable]
  public class CanvasButtons : ButtonsInputView<CanvasType>
  {
    [SerializeField] private Canvas canvas;
    public void SetCanvas(bool state)
    {
      canvas.enabled = state;
    }
  }
  public class CanvasAllController : Controller<CanvasAllView, StartGameServiceLayer>
  {
    private readonly EndGameServiceLayer endGameServiceLayer;
    public CanvasAllController(CanvasAllView view) : base(view)
    {
      endGameServiceLayer = ServiceFactory.GetService<EndGameServiceLayer>();
    }

    public override void AddListeners()
    {
      base.AddListeners();
      View.AddListener(CanvasType.MainMenuCanvas, SetMainMenuCanvas);
      View.AddListener(CanvasType.SettingsCanvas, SetSettingsCanvas);
      View.AddListener(CanvasType.GameCanvas, SetGameCanvas);
      View.AddListener(CanvasType.CustomizeCanvas, SetCustomizeCanvas);
      endGameServiceLayer.DtoHandler.AddListener(HandleEndServiceLayer);
    }

    public override void RemoveListeners()
    {
      base.RemoveListeners();
       View.RemoveAllListeners();
    }

    protected override void HandleServiceLayer()
    {
      View.SetCanvas(CanvasType.ScoreCanvas);
    }

    private void HandleEndServiceLayer()
    {
      View.SetCanvas(CanvasType.EndGameCanvas);
      View.BlockCanvas();
    }
    private void SetMainMenuCanvas()
    {
      View.SetCanvas(CanvasType.MainMenuCanvas);
    }

    private void SetGameCanvas()
    {
      View.SetCanvas(CanvasType.GameCanvas);
    }

    private void SetSettingsCanvas()
    {
      View.SetCanvas(CanvasType.SettingsCanvas);
    }
    private void SetCustomizeCanvas()
    {
      View.SetCanvas(CanvasType.CustomizeCanvas);
    }
  }

  public enum CanvasType
  {
    Unset = 0,
    MainMenuCanvas = 1,
    GameCanvas = 2,
    SettingsCanvas = 3,
    EndGameCanvas = 4, 
    CustomizeCanvas = 5,
    ScoreCanvas = 6,
  }
}