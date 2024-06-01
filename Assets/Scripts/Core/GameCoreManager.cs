using LNE.UI;
using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;
using Zenject;

namespace LNE.Core
{
  public class GameCoreManager : MonoBehaviour
  {
    public bool IsGameOver { get; private set; } = false;
    public bool IsGameStarted { get; private set; } = false;
    public bool IsPlayerDead { get; private set; } = false;
    public int Points { get; private set; } = 0;

    [SerializeField]
    private GameOverCanvas _gameOverCanvas;

    [SerializeField]
    private Canvas _gameStartCanvas;

    [SerializeField]
    private InfoCanvas _infoCanvas;

    [SerializeField]
    private TextMeshProUGUI _pointsText;

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
      ShowInfoCanvas();
    }

    public void TriggerGameOver()
    {
      IsGameOver = true;
      TriggerPlayerDead();
      ShowGameOverCanvas();
      HideInfoCanvas();
    }

    public void TriggerPlayerDead()
    {
      IsPlayerDead = true;
    }

    public void AddPoint()
    {
      Points++;
      _infoCanvas.SetPoints(Points);
    }

    public void Restart()
    {
      _zenjectSceneLoader.LoadScene(SceneName.Game);
    }

    public void ShowGameOverCanvas()
    {
      _gameOverCanvas.gameObject.SetActive(true);
      _gameOverCanvas.SetPoints(Points);
    }

    public void HideGameStartCanvas()
    {
      _gameStartCanvas.gameObject.SetActive(false);
    }

    public void ShowInfoCanvas()
    {
      _infoCanvas.gameObject.SetActive(true);
      _infoCanvas.SetPoints(Points);
    }

    public void HideInfoCanvas()
    {
      _infoCanvas.gameObject.SetActive(false);
    }
  }
}
