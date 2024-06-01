using System;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Core
{
  public class GameCorePresenter : MonoBehaviour
  {
    public event Action<bool> OnChangePlayMode;
    public bool IsGameOver { get; private set; } = false;
    public bool IsGameStarted { get; private set; } = false;
    public bool IsPlayerDead { get; private set; } = false;
    public int Points { get; private set; } = 0;
    public bool IsAIPlayMode { get; private set; } = false;

    private ZenjectSceneLoader _zenjectSceneLoader;
    private GameCoreView _view;

    [Inject]
    private void Construct(ZenjectSceneLoader zenjectSceneLoader)
    {
      _zenjectSceneLoader = zenjectSceneLoader;
    }

    private void Awake()
    {
      _view = GetComponent<GameCoreView>();
    }

    public void StartGame(bool isAIPlayMode)
    {
      if (IsGameStarted)
      {
        return;
      }

      IsGameStarted = true;
      IsAIPlayMode = isAIPlayMode;
      HideGameStartCanvas();
      ShowInfoCanvas();
      OnChangePlayMode?.Invoke(IsAIPlayMode);
    }

    public void TriggerGameOver()
    {
      if (IsGameOver)
      {
        return;
      }

      IsGameOver = true;
      TriggerPlayerDead();
      ShowGameOverCanvas();
      HideInfoCanvas();
    }

    public void ToggleIsAIPlayMode()
    {
      IsAIPlayMode = !IsAIPlayMode;
      _view.SetExitAIPlayModeButtonState(IsAIPlayMode);
      OnChangePlayMode?.Invoke(IsAIPlayMode);
    }

    public void TriggerPlayerDead()
    {
      if (IsPlayerDead)
      {
        return;
      }

      IsPlayerDead = true;
    }

    public void Restart()
    {
      _zenjectSceneLoader.LoadScene(SceneName.Game);
    }

    public void AddPoint()
    {
      Points++;
      _view.SetPointsInfoCanvas(Points);
    }

    public void ShowGameOverCanvas()
    {
      _view.ShowGameOverCanvas(Points);
    }

    public void HideGameStartCanvas()
    {
      _view.HideGameStartCanvas();
    }

    public void ShowInfoCanvas()
    {
      _view.ShowInfoCanvas(Points);
      _view.SetPointsInfoCanvas(Points);
    }

    public void HideInfoCanvas()
    {
      _view.HideInfoCanvas();
    }
  }
}
