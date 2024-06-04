using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Core
{
  public class NetworkSavingManager : MonoBehaviour
  {
    private DatabaseReference _databaseReference;

    private void Start()
    {
      _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void AddScore(ScoreModel scoreModel)
    {
      if (scoreModel == null)
      {
        Debug.LogError("Cannot save a null model.");
        return;
      }

      try
      {
        string json = JsonUtility.ToJson(scoreModel);
        string uuid = Guid.NewGuid().ToString();
        await _databaseReference
          .Child($"{NetworkSavingPath.Leaderboard}/{uuid}")
          .SetRawJsonValueAsync(json);

        // Clean up the leaderboard
        await CleanUpLeaderboard();
      }
      catch (Exception e)
      {
        Debug.LogError(e.Message);
      }
    }

    public async Task<List<ScoreModel>> GetScoreListAsync()
    {
      List<ScoreModel> scoreModels = new List<ScoreModel>();

      try
      {
        var snapshot = await _databaseReference
          .Child($"{NetworkSavingPath.Leaderboard}")
          .GetValueAsync();
        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
          string json = childSnapshot.GetRawJsonValue();
          ScoreModel scoreModel = JsonUtility.FromJson<ScoreModel>(json);
          scoreModels.Add(scoreModel);
        }

        // Sort by score then by created date
        scoreModels.Sort(
          (a, b) =>
          {
            if (a.Score == b.Score)
            {
              return DateTime.Compare(
                DateTime.Parse(a.CreatedAt),
                DateTime.Parse(b.CreatedAt)
              );
            }

            return b.Score.CompareTo(a.Score);
          }
        );
      }
      catch (Exception e)
      {
        Debug.LogError("Failed to load data: " + e.Message);
      }

      return scoreModels;
    }

    private async Task CleanUpLeaderboard()
    {
      try
      {
        var scores = await GetScoreListAsync();

        if (scores.Count > 10)
        {
          // Get the top 10 scores
          var topScores = scores.GetRange(0, 10);

          // Clear the existing leaderboard
          await _databaseReference
            .Child($"{NetworkSavingPath.Leaderboard}")
            .RemoveValueAsync();

          // Add the top 10 scores back to the leaderboard
          foreach (var score in topScores)
          {
            string json = JsonUtility.ToJson(score);
            string uuid = Guid.NewGuid().ToString();
            await _databaseReference
              .Child($"{NetworkSavingPath.Leaderboard}/{uuid}")
              .SetRawJsonValueAsync(json);
          }
        }
      }
      catch (Exception e)
      {
        Debug.LogError("Failed to clean up leaderboard: " + e.Message);
      }
    }
  }
}
