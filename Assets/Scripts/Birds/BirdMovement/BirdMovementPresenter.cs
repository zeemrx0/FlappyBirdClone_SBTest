using LNE.Colliders;
using LNE.Core;
using LNE.Inputs;
using LNE.Pipes;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private GamePlayManager _gamePlayManager;
    private PlayerInputManager _playerInputManager;
    private BirdMovementView _view;
    private GameBoxCollider _collider;
    private BirdMovementModel _model = new BirdMovementModel();
    private AIBird _aiBird;

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
      _collider = GetComponent<GameBoxCollider>();
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

      CheckIsCollidingWithPipe();

      if (_model.TimeUntilNextFlap > 0f)
      {
        _model.TimeUntilNextFlap -= Time.deltaTime;
      }
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
          _gamePlayManager.TriggerPlayerDead();
          _view.PlayHitSound();
          _view.PlayFallSound();
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
          _gamePlayManager.TriggerGameOver();
          if (!_gamePlayManager.IsPlayerDead)
          {
            _view.PlayHitSound();
          }
          return;
        }
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
