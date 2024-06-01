using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class GameOverCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void SetScore(int score)
    {
      _scoreText.text = score.ToString();
    }
  }
}
