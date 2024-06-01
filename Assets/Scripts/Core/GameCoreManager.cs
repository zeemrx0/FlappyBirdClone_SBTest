using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Core
{
  public class GameCoreManager : MonoBehaviour
  {
    public bool IsGameOver { get; private set; } = false;
    public bool IsGameStarted { get; private set; } = false;
    public bool IsPlayerDead { get; private set; } = false;

    [SerializeField]
    private Canvas _gameOverCanvas;

    [SerializeField]
    private Canvas _gameStartCanvas;

    private ZenjectSceneLoader _zenjectSceneLoader;

    [Inject]
    private void Construct(ZenjectSceneLoader zenjectSceneLoader)
    {
      _zenjectSceneLoader = zenjectSceneLoader;
    }

    public void StartGame()
    {
      IsGameStarted = true;
      HideGameStartCanvas();
    }

    public void TriggerGameOver()
    {
      IsGameOver = true;
      TriggerPlayerDead();
      ShowGameOverCanvas();
    }

    public void TriggerPlayerDead()
    {
      IsPlayerDead = true;
    }

    public void Restart()
    {
      _zenjectSceneLoader.LoadScene(SceneName.Game);
    }

    public void ShowGameOverCanvas()
    {
      _gameOverCanvas.gameObject.SetActive(true);
    }

    public void HideGameStartCanvas()
    {
      _gameStartCanvas.gameObject.SetActive(false);
    }
  }
}
