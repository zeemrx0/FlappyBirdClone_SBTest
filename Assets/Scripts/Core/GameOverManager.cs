using UnityEngine;

namespace LNE.Core
{
  public class GameOverManager : MonoBehaviour
  {
    public bool IsGameOver { get; private set; } = false;

    public void GameOver()
    {
      IsGameOver = true;
    }
  }
}
