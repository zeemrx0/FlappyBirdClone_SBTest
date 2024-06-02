using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LNE.Core
{
  public class SavingManager : MonoBehaviour
  {
    public void Save<T>(T model, string filePath)
    {
      if (model == null)
      {
        Debug.LogError("Cannot save a null model.");
        return;
      }

      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + filePath;
      FileStream stream = new FileStream(path, FileMode.Create);

      try
      {
        formatter.Serialize(stream, model);
      }
      catch (Exception e)
      {
        Debug.LogError("Failed to serialize model: " + e.Message);
      }
      finally
      {
        stream.Close();
      }
    }

    public T Load<T>(string filePath, T defaultModel)
    {
      string path = Application.persistentDataPath + filePath;
      if (File.Exists(path))
      {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        try
        {
          stream.Position = 0;
          T model = (T)formatter.Deserialize(stream);
          return model;
        }
        catch (Exception e)
        {
          Debug.LogError("Failed to deserialize model: " + e.Message);
          return defaultModel;
        }
        finally
        {
          stream.Close();
        }
      }
      else
      {
        return defaultModel;
      }
    }
  }
}
