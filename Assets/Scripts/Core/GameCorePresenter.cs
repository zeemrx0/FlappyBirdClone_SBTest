using System;
using LNE.UI;
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
    public bool IsAIPlayMode { get; private set; } = false;
    public ScoreModel ScoreModel { get; private set; } = new ScoreModel();

    [SerializeField]
    private GameOverCanvas _gameOverCanvas;

    [SerializeField]
    private GameStartCanvas _gameStartCanvas;

    [SerializeField]
    private InfoCanvas _infoCanvas;

    private ZenjectSceneLoader _zenjectSceneLoader;

    [Inject]
    private void Construct(ZenjectSceneLoader zenjectSceneLoader)
    {
      _zenjectSceneLoader = zenjectSceneLoader;
    }

    public void StartGame(bool isAIPlayMode)
    {
      if (IsGameStarted)
      {
        return;
      }

      IsGameStarted = true;
      IsAIPlayMode = isAIPlayMode;
      _gameStartCanvas.Hide();
      _infoCanvas.Show();
      _infoCanvas.SetScore(ScoreModel.Score);
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
      _gameOverCanvas.Show();
      _gameOverCanvas.SetScore(ScoreModel.Score);
      _infoCanvas.Hide();
    }

    public void ToggleIsAIPlayMode()
    {
      IsAIPlayMode = !IsAIPlayMode;
      _infoCanvas.SetExitAIPlayModeButtonState(IsAIPlayMode);
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
      ScoreModel.Score++;
      _infoCanvas.SetScore(ScoreModel.Score);
    }

    public void ShowAIModeMessage()
    {
      _infoCanvas.ShowAIModeMessage(2f);
    }
  }
}
