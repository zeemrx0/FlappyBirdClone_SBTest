using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipePairMovement : MonoBehaviour
  {
    [SerializeField]
    private float _speed = 1f;

    private GameCoreManager _gameOverManager;

    [Inject]
    private void Construct(GameCoreManager gameOverManager)
    {
      _gameOverManager = gameOverManager;
    }

    void Update()
    {
      if (!_gameOverManager.IsGameStarted)
      {
        return;
      }

      if (_gameOverManager.IsPlayerDead)
      {
        return;
      }

      transform.position += Vector3.left * _speed * Time.deltaTime;
    }
  }
}
