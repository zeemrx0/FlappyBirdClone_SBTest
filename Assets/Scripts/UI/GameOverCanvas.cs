using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.UI
{
  public class GameOverCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _highScoreText;

    [SerializeField]
    private Image _crownImage;

    public void SetActive(bool isActive)
    {
      gameObject.SetActive(isActive);
    }

    public void SetScore(int score)
    {
      _scoreText.text = score.ToString();
    }

    public void SetHighScore(int highScore)
    {
      _highScoreText.text = highScore.ToString();
    }

    public void SetCrownActive(bool isActive)
    {
      _crownImage.gameObject.SetActive(isActive);
    }
  }
}
