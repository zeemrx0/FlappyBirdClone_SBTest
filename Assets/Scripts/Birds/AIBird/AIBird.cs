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
    private AIBirdData _aiBirdData;

    [SerializeField]
    private PipePairSpawner _pipePairSpawner;

    private GamePlayManager _gamePlayManager;
    private BirdMovementPresenter _birdMovementPresenter;
    private GameBoxCollider _collider;
    private float _heightDifference = 0f;
    private float _timeUntilNextFlap = 0f;

    [Inject]
    private void Construct(GamePlayManager gamePlayManager)
    {
      _gamePlayManager = gamePlayManager;
    }

    private void Awake()
    {
      _birdMovementPresenter = GetComponent<BirdMovementPresenter>();
      _collider = GetComponent<GameBoxCollider>();
    }

    private void Update()
    {
      if (
        _gamePlayManager.IsGameOver
        || _gamePlayManager.IsPlayerDead
        || !_gamePlayManager.IsGameStarted
      )
      {
        return;
      }

      if (_pipePairSpawner.FirstPipePair == null)
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
          _pipePairSpawner.FirstPipePair.transform.position.y
          + _aiBirdData.TargetGap
          - _collider.transform.position.y;

        if (_heightDifference > 0)
        {
          if (_birdMovementPresenter.TryFlap())
          {
            _timeUntilNextFlap = Math.Clamp(
              (_aiBirdData.FlapCooldownCoefficient/1000)
                * _birdMovementPresenter.FlapForce
                / _heightDifference,
              0f,
              _aiBirdData.MaxFlapCooldown
            );
            return true;
          }
        }
      }

      return false;
    }
  }
}
