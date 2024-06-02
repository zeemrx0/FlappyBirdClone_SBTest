using System;

namespace LNE.Core
{
  [Serializable]
  public class ScoreModel
  {
    public int Score { get; set; }

    public ScoreModel()
    {
      Score = 0;
    }
  }
}
