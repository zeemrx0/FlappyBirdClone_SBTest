using LNE.Colliders;
using LNE.Core;
using LNE.Grounds;
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
    private PipeSpawner _pipeSpawner;

    [SerializeField]
    private Ground _ground;

    private GamePlayManager _gamePlayManager;
    private PlayerInputManager _playerInputManager;
    private PlayerInputAction _playerInputAction;
    private BirdMovementView _view;
    private GameBoxCollider _collider;
    private BirdMovementModel _model;
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
      _playerInputManager.Init();
      _playerInputAction = _playerInputManager.PlayerInputAction;
    }

    private void Awake()
    {
      _view = GetComponent<BirdMovementView>();
      _collider = GetComponent<GameBoxCollider>();
      _model = new BirdMovementModel();
      _aiBird = GetComponent<AIBird>();
    }

    private void OnEnable()
    {
      _playerInputAction.BirdMovement.Flap.performed += ctx => HandleFlap(ctx);
      _gamePlayManager.OnChangePlayMode += HandleOnChangePlayMode;
    }

    private void OnDisable()
    {
      _playerInputAction.BirdMovement.Flap.performed -= ctx => HandleFlap(ctx);
      _gamePlayManager.OnChangePlayMode -= HandleOnChangePlayMode;
    }

    void Update()
    {
      if (!_gamePlayManager.IsGameStarted || _gamePlayManager.IsGameOver)
      {
        return;
      }

      ApplyGravity();

      _view.Flap(_model.VerticalSpeed, _birdMovementData.RotateSpeed);

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
      if (_pipeSpawner.FirstPipePair == null)
      {
        return;
      }

      foreach (Transform pipe in _pipeSpawner.FirstPipePair.transform)
      {
        if (_collider.IsCollidingWith(pipe.GetComponent<GameBoxCollider>()))
        {
          _gamePlayManager.TriggerPlayerDead();
          return;
        }
      }
    }

    private void CheckIsCollidingWithGround()
    {
      if (_collider.IsCollidingWith(_ground.GetComponent<GameBoxCollider>()))
      {
        _model.VerticalSpeed = 0f;
        _gamePlayManager.TriggerGameOver();
        return;
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

    private void HandleFlap(
      UnityEngine.InputSystem.InputAction.CallbackContext ctx
    )
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
          _gamePlayManager.ShowAIModeMessage();
        }
        return;
      }

      TryFlap();
    }
  }
}
