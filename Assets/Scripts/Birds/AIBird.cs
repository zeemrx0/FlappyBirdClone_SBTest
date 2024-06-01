using System;
using LNE.Colliders;
using LNE.Core;
using LNE.Pipes;
using UnityEngine;
using Zenject;

namespace LNE.Birds
{
  public class AIBird : MonoBehaviour
  {
    [SerializeField]
    private PipeSpawner _pipeSpawner;

    [SerializeField]
    private AIBirdData _aiBirdData;

    private GameCoreManager _gameCoreManager;

    private BirdMovementPresenter _birdMovementPresenter;
    private GameBoxCollider _collider;
    private float _heightDifference = 0f;
    private float _timeUntilNextFlap = 0f;

    [Inject]
    private void Construct(GameCoreManager gameCoreManager)
    {
      _gameCoreManager = gameCoreManager;
    }

    private void Awake()
    {
      _birdMovementPresenter = GetComponent<BirdMovementPresenter>();
      _collider = GetComponent<GameBoxCollider>();
    }

    private void Update()
    {
      if (
        _gameCoreManager.IsGameOver
        || _gameCoreManager.IsPlayerDead
        || !_gameCoreManager.IsGameStarted
      )
      {
        return;
      }

      if (_pipeSpawner.FirstPipePair == null)
      {
        return;
      }

      TryFlap();

      if (_timeUntilNextFlap > 0f)
      {
        _timeUntilNextFlap -= Time.deltaTime;
      }
    }

    private bool TryFlap()
    {
      if (_timeUntilNextFlap <= 0f)
      {
        _heightDifference =
          _pipeSpawner.FirstPipePair.transform.position.y
          + _aiBirdData.TargetGap
          - _collider.transform.position.y;

        if (_heightDifference > 0)
        {
          if (_birdMovementPresenter.TryFlap())
          {
            _timeUntilNextFlap = Math.Clamp(
              _aiBirdData.FlapCooldownCoefficient
                * _birdMovementPresenter.FlapForce
                / _heightDifference,
              0f,
              0.1f
            );
            return true;
          }
        }
      }

      return false;
    }
  }
}
