using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    private InputUsernameCanvas _inputNameCanvas;

    [SerializeField]
    private LeaderboardCanvas _leaderboardCanvas;

    [SerializeField]
    private AudioClip _scoreSound;

    private ZenjectSceneLoader _zenjectSceneLoader;
    private SavingManager _savingSystem;
    private NetworkSavingManager _networkSavingManager;

    private AudioSource _audioSource;
    private ScoreModel _previousScoreModel = new ScoreModel();

    public float StartGameSpeed => _gamePlayData.StartGameSpeed;
    public float BirdOffset => _gamePlayData.BirdOffset;

    [Inject]
    private void Construct(
      ZenjectSceneLoader zenjectSceneLoader,
      SavingManager savingSystem,
      NetworkSavingManager networkSavingManager
    )
    {
      _zenjectSceneLoader = zenjectSceneLoader;
      _savingSystem = savingSystem;
      _networkSavingManager = networkSavingManager;
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

    public async void TriggerGameOver()
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

      if (await CheckIsInTop(10, ScoreModel.Score) == false)
      {
        _gameOverCanvas.SetActive(true);
      }
    }

    public async Task<bool> CheckIsInTop(int top, int currentScore)
    {
      if (currentScore == 0)
      {
        return false;
      }

      var scoreList = await _networkSavingManager.GetScoreListAsync();

      if (scoreList.Count < top)
      {
        for (int i = 0; i < Mathf.Min(top, scoreList.Count); i++)
        {
          if (currentScore > scoreList[i].Score)
          {
            _gameOverCanvas.SetActive(false);
            _inputNameCanvas.SetRankText((i + 1).ToString());
            _inputNameCanvas.SetActive(true);
            return true;
          }
        }

        _gameOverCanvas.SetActive(false);
        _inputNameCanvas.SetRankText((scoreList.Count + 1).ToString());
        _inputNameCanvas.SetActive(true);

        return true;
      }
      return false;
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

    public void ToggleIsAIPlayMode()
    {
      IsAIPlayMode = !IsAIPlayMode;
      _controlCanvas.SetToggleAIPlayModeButtonState(IsAIPlayMode);
      OnChangePlayMode?.Invoke(IsAIPlayMode);
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

    public void SaveScoreToNetwork()
    {
      ScoreModel.Username = _inputNameCanvas.GetUsername();
      _networkSavingManager.AddScore(ScoreModel);
      HideInputNameCanvas();
    }

    public void HideInputNameCanvas()
    {
      _inputNameCanvas.SetActive(false);
      _gameOverCanvas.SetActive(true);
    }

    public void SetLeaderboardCanvasActive(bool isActive)
    {
      _gameOverCanvas.SetActive(!isActive);
      _leaderboardCanvas.SetActive(isActive);
    }
  }
}
