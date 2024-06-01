using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipePairMovement : MonoBehaviour
  {
    [SerializeField]
    private float _speed = 1f;

    private GameOverManager _gameOverManager;

    [Inject]
    private void Construct(GameOverManager gameOverManager)
    {
      _gameOverManager = gameOverManager;
    }

    void Update()
    {
      if (_gameOverManager.IsGameOver)
      {
        return;
      }

      transform.position += Vector3.left * _speed * Time.deltaTime;
    }
  }
}
