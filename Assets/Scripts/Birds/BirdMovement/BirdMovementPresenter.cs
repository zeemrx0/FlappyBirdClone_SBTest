using LNE.Colliders;
using LNE.Core;
using LNE.Inputs;
using LNE.Pipes;
using LNE.Utilities;
using UnityEngine;
using Zenject;

namespace LNE.Birds
{
  public class BirdMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    private BirdMovementData _birdMovementData;

    [SerializeField]
    private PipePairSpawner _pipePairSpawner;

    [SerializeField]
    private Transform _groundContainer;

    [SerializeField]
    private Transform _floor;

    private GamePlayManager _gamePlayManager;
    private PlayerInputManager _playerInputManager;
    private BirdMovementView _view;
    private GameCircleCollider _collider;
    private BirdMovementModel _model = new BirdMovementModel();
    private AIBird _aiBird;
    private Vector2 _previousPosition;

    public float FlapForce => _birdMovementData.FlapForce;

    [Inject]
    private void Construct(
      GamePlayManager gamePlayManager,
      PlayerInputManager playerInputManager
    )
    {
      _gamePlayManager = gamePlayManager;
      _playerInputManager = playerInputManager;
    }

    private void Awake()
    {
      _view = GetComponent<BirdMovementView>();
      _collider = GetComponent<GameCircleCollider>();
      _aiBird = GetComponent<AIBird>();
    }

    private void OnEnable()
    {
      _playerInputManager.TapArea.PointerDownEvent += HandleFlap;
      _gamePlayManager.OnChangePlayMode += HandleOnChangePlayMode;
    }

    private void OnDisable()
    {
      _playerInputManager.TapArea.PointerDownEvent -= HandleFlap;
      _gamePlayManager.OnChangePlayMode -= HandleOnChangePlayMode;
    }

    void Update()
    {
      if (!_gamePlayManager.IsGameStarted || _gamePlayManager.IsGameOver)
      {
        return;
      }

      ApplyGravity();

      _view.Fly(_model.VerticalSpeed, _birdMovementData.RotateSpeed);

      CheckIsCollidingWithGround();

      if (_gamePlayManager.IsPlayerDead)
      {
        return;
      }

      CheckIsCollidingWithFloor();

      CheckIsCollidingWithPipe();

      if (_model.TimeUntilNextFlap > 0f)
      {
        _model.TimeUntilNextFlap -= Time.deltaTime;
      }
    }

    private void LateUpdate()
    {
      _previousPosition = transform.position;
    }

    private void CheckIsCollidingWithPipe()
    {
      if (_pipePairSpawner.FirstPipePair == null)
      {
        return;
      }

      foreach (Transform pipe in _pipePairSpawner.FirstPipePair.transform)
      {
        if (_collider.IsCollidingWith(pipe.GetComponent<GameBoxCollider>()))
        {
          _view.PlayDeadAnimation();
          _view.PlayHitSound();
          _view.PlayFallSound();

          _view.SpawnHitVFX(
            ColliderHelper.GetTouchPoint(
              _collider,
              pipe.GetComponent<GameBoxCollider>()
            ) ?? _collider.transform.position
          );
          _gamePlayManager.TriggerPlayerDead();
          transform.position = _previousPosition;
          return;
        }
      }
    }

    private void CheckIsCollidingWithGround()
    {
      foreach (Transform ground in _groundContainer)
      {
        if (_collider.IsCollidingWith(ground.GetComponent<GameBoxCollider>()))
        {
          _model.VerticalSpeed = 0f;
          if (!_gamePlayManager.IsPlayerDead)
          {
            _view.SpawnHitVFX(
              ColliderHelper.GetTouchPoint(
                _collider,
                ground.GetComponent<GameBoxCollider>()
              ) ?? _collider.transform.position
            );
            _view.PlayDeadAnimation();
            _view.PlayHitSound();
            transform.position = _previousPosition;
          }
          _gamePlayManager.TriggerGameOver();
          return;
        }
      }
    }

    private void CheckIsCollidingWithFloor()
    {
      if (_collider.IsCollidingWith(_floor.GetComponent<GameBoxCollider>()))
      {
        transform.position = _previousPosition;
      }
    }

    private void ApplyGravity()
    {
      _model.VerticalSpeed +=
        _birdMovementData.Gravity * _birdMovementData.Mass * Time.deltaTime;
    }

    public void Flap()
    {
      _model.VerticalSpeed = _birdMovementData.FlapForce;
      _view.PlayFlapSound();
      _view.PlayFlapAnimation();
    }

    public bool TryFlap()
    {
      if (_model.TimeUntilNextFlap <= 0f)
      {
        Flap();
        _model.TimeUntilNextFlap = _birdMovementData.FlapCooldown;
        return true;
      }

      return false;
    }

    private void HandleOnChangePlayMode(bool isAIPlayMode)
    {
      if (isAIPlayMode)
      {
        _aiBird.enabled = true;
      }
      else
      {
        _aiBird.enabled = false;
      }
    }

    public void HandleFlap()
    {
      if (_gamePlayManager.IsPlayerDead)
      {
        return;
      }

      if (!_gamePlayManager.IsGameStarted)
      {
        _gamePlayManager.StartGame();
      }

      if (_gamePlayManager.IsAIPlayMode)
      {
        _gamePlayManager.ShowAIModeMessage();
        return;
      }

      TryFlap();
    }
  }
}
