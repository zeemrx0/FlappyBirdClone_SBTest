using System.Threading.Tasks;
using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.UI
{
  public class LeaderboardCanvas : MonoBehaviour
  {
    [SerializeField]
    private Transform _leaderboard;

    [SerializeField]
    private LeaderboardItem _leaderboardItemPrefab;

    private NetworkSavingManager _networkSavingManager;

    [Inject]
    private void Construct(NetworkSavingManager networkSavingManager)
    {
      _networkSavingManager = networkSavingManager;
    }

    public void SetActive(bool isActive)
    {
      gameObject.SetActive(isActive);
      if (isActive)
      {
        ShowLeaderboard();
      }
    }

    public async void ShowLeaderboard()
    {
      var scoreList = await _networkSavingManager.GetScoreListAsync();

      foreach (Transform child in _leaderboard)
      {
        Destroy(child.gameObject);
      }

      for (int i = 0; i < 10; i++)
      {
        var leaderboardItem = Instantiate(_leaderboardItemPrefab, _leaderboard);
        if (i < scoreList.Count)
        {
          leaderboardItem.SetActive(true);
          leaderboardItem.SetRank(i + 1);
          leaderboardItem.SetUsername(
            scoreList[i].Username[..Mathf.Min(12, scoreList[i].Username.Length)]
              + (scoreList[i].Username.Length > 12 ? "..." : "")
          );
          leaderboardItem.SetScore(scoreList[i].Score);
        }
        else
        {
          leaderboardItem.SetRank(i + 1);
          leaderboardItem.SetUsername("-");
          leaderboardItem.SetScore(0);
        }
      }
    }
  }
}
