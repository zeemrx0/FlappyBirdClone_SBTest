using LNE.UI;
using UnityEngine;

namespace LNE.Core
{
  public class GameCoreView : MonoBehaviour
  {
    [SerializeField]
    private GameOverCanvas _gameOverCanvas;

    [SerializeField]
    private Canvas _gameStartCanvas;

    [SerializeField]
    private InfoCanvas _infoCanvas;

    public void ShowGameOverCanvas(int points)
    {
      _gameOverCanvas.gameObject.SetActive(true);
      _gameOverCanvas.SetPoints(points);
    }

    public void HideGameStartCanvas()
    {
      _gameStartCanvas.gameObject.SetActive(false);
    }

    public void ShowInfoCanvas()
    {
      _infoCanvas.gameObject.SetActive(true);
    }

    public void SetPointsInfoCanvas(int points)
    {
      _infoCanvas.SetPoints(points);
    }

    public void HideInfoCanvas()
    {
      _infoCanvas.gameObject.SetActive(false);
    }
  }
}