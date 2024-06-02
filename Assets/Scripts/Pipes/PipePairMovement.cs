using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipePairMovement : MonoBehaviour
  {
    private GamePlayManager _gamePlayManager;

    [Inject]
    private void Construct(GamePlayManager gamePlayManager)
    {
      _gamePlayManager = gamePlayManager;
    }

    void Update()
    {
      if (!_gamePlayManager.IsGameStarted || _gamePlayManager.IsPlayerDead)
      {
        return;
      }

      transform.position +=
        Vector3.left * _gamePlayManager.GameSpeed * Time.deltaTime;
    }
  }
}
