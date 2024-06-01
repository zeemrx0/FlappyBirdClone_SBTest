using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipePairMovement : MonoBehaviour
  {
    [SerializeField]
    private float _speed = 1f;

    private GameCoreManager _gameCoreManager;

    [Inject]
    private void Construct(GameCoreManager gameCoreManager)
    {
      _gameCoreManager = gameCoreManager;
    }

    void Update()
    {
      if (!_gameCoreManager.IsGameStarted || _gameCoreManager.IsPlayerDead)
      {
        return;
      }

      transform.position += Vector3.left * _speed * Time.deltaTime;
    }
  }
}
