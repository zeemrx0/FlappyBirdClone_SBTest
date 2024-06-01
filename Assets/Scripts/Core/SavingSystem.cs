using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LNE.Core
{
  public class SavingManager : MonoBehaviour {
    public void SaveHighScore(ScoreModel scoreModel) {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/high-score.save";
      FileStream stream = new FileStream(path, FileMode.Create);
      formatter.Serialize(stream, scoreModel);
      stream.Close();
    }
  }
}
