using System;
using LNE.UI;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Core
{
  public class GamePlayManager : MonoBehaviour
  {
    public event Action<bool> OnChangePlayMode;

    public float GameSpeed { get; private set; }
    public bool IsGameOver { get; private set; } = false;
    public bool IsGameStarted { get; private set; } = false;
    public bool IsPlayerDead { get; private set; } = false;
    public bool IsAIPlayMode { get; private set; } = false;
    public ScoreModel ScoreModel { get; private set; } = new ScoreModel();

    [SerializeField]
    private GamePlayData _gamePlayData;

    [SerializeField]
    private GameOverCanvas _gameOverCanvas;

    [SerializeField]
    private GameStartCanvas _gameStartCanvas;

    [SerializeField]
    private InfoCanvas _infoCanvas;

    [SerializeField]
    private ControlCanvas _controlCanvas;

    [SerializeField]
    private AudioClip _scoreSound;

    private ZenjectSceneLoader _zenjectSceneLoader;
    private SavingManager _savingSystem;

    private AudioSource _audioSource;
    private ScoreModel _previousScoreModel = new ScoreModel();

    [Inject]
    private void Construct(
      ZenjectSceneLoader zenjectSceneLoader,
      SavingManager savingSystem
    )
    {
      _zenjectSceneLoader = zenjectSceneLoader;
      _savingSystem = savingSystem;
    }

    private void Awake()
    {
      GameSpeed = _gamePlayData.InitialGameSpeed;
      _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
      if (
        ScoreModel.Score % _gamePlayData.GameSpeedIncrementInterval == 0
        && ScoreModel.Score > _previousScoreModel.Score
      )
      {
        _previousScoreModel.Score = ScoreModel.Score;
        GameSpeed += _gamePlayData.GameSpeedIncrement;
        _audioSource.PlayOneShot(_scoreSound);
      }
    }

    public void StartGame()
    {
      if (IsGameStarted)
      {
        return;
      }

      IsGameStarted = true;
      _gameStartCanvas.SetActive(false);
      _infoCanvas.SetActive(true);
      _infoCanvas.SetScore(ScoreModel.Score);
      _controlCanvas.SetToggleAIPlayModeButtonActive(true);
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
      ScoreModel highScore = _savingSystem.Load<ScoreModel>(
        SavingPath.HighScore,
        new ScoreModel()
      );
      _gameOverCanvas.SetScore(ScoreModel.Score);
      if (ScoreModel.Score > highScore.Score)
      {
        highScore = ScoreModel;
        _savingSystem.Save(ScoreModel, SavingPath.HighScore);
        _gameOverCanvas.SetCrownActive(true);
      }
      else
      {
        _gameOverCanvas.SetCrownActive(false);
      }
      _infoCanvas.SetActive(false);
      _controlCanvas.SetActive(false);
      _gameOverCanvas.SetHighScore(highScore.Score);
      _gameOverCanvas.SetActive(true);
    }

    public void ToggleIsAIPlayMode()
    {
      IsAIPlayMode = !IsAIPlayMode;
      _controlCanvas.SetToggleAIPlayModeButtonState(IsAIPlayMode);
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

    public void IncreaseScore()
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
