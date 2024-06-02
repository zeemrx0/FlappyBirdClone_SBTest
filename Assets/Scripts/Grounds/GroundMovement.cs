using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Grounds
{
  public class GroundMovement : MonoBehaviour
  {
    [SerializeField]
    private float _destroyXPosition = -21f;

    [SerializeField]
    private GameObject _otherGround;

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

    void LateUpdate()
    {
      if (transform.position.x < _destroyXPosition)
      {
        transform.position = new Vector3(
          _otherGround.transform.position.x + 21f,
          transform.position.y,
          transform.position.z
        );
      }
    }
  }
}
