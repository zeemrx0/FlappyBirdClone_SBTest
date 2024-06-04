using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class LeaderboardItem : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _rankText;

    [SerializeField]
    private TMP_Text _usernameText;

    [SerializeField]
    private TMP_Text _scoreText;

    public void SetActive(bool isActive)
    {
      gameObject.SetActive(isActive);
    }

    public void SetRank(int rank)
    {
      _rankText.text = rank.ToString();
    }

    public void SetUsername(string username)
    {
      if (string.IsNullOrEmpty(username))
      {
        _usernameText.text = GameString.AnonymousBird;
        return;
      }

      _usernameText.text = username;
    }

    public void SetScore(int score)
    {
      _scoreText.text = score.ToString();
    }
  }
}
