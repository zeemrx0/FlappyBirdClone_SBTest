using System;
using System.Globalization;

namespace LNE.Core
{
  [Serializable]
  public class ScoreModel
  {
    public string Username;
    public int Score;
    public string CreatedAt;

    public ScoreModel()
    {
      Username = "Anonymous Bird";
      Score = 0;
      CreatedAt = DateTime.Now.ToString("o", CultureInfo.InvariantCulture);
    }
  }
}
