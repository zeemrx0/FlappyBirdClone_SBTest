using LNE.Colliders;
using LNE.Core;
using LNE.Grounds;
using LNE.Inputs;
using LNE.Pipes;
using UnityEngine;
using Zenject;

namespace LNE.Birds
{
  public class BirdMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    private float _flapForce = 10f;

    [SerializeField]
    private float _gravity = -9.8f;

    [SerializeField]
    private float _mass = 1f;

    [SerializeField]
    private float _rotateSpeed = 1f;

    [SerializeField]
    private PipeSpawner _pipeSpawner;

    [SerializeField]
    private Ground _ground;

    private GameCoreManager _gameOverManager;
    private PlayerInputManager _playerInputManager;
    private PlayerInputAction _playerInputAction;
    private BirdMovementView _view;
    private GameBoxCollider _collider;

    private float _verticalSpeed = 0f;

    [Inject]
    private void Construct(
      GameCoreManager gameOverManager,
      PlayerInputManager playerInputManager
    )
    {
      _gameOverManager = gameOverManager;
      _playerInputManager = playerInputManager;
      _playerInputManager.Init();
      _playerInputAction = _playerInputManager.PlayerInputAction;
    }

    private void Awake()
    {
      _view = GetComponent<BirdMovementView>();
      _collider = GetComponent<GameBoxCollider>();
    }

    private void OnEnable()
    {
      _playerInputAction.BirdMovement.Flap.performed += ctx => HandleFlap(ctx);
    }

    private void OnDisable()
    {
      _playerInputAction.BirdMovement.Flap.performed -= ctx => HandleFlap(ctx);
    }

    void Update()
    {
      if (!_gameOverManager.IsGameStarted)
      {
        return;
      }

      if (_gameOverManager.IsGameOver)
      {
        return;
      }

      ApplyGravity();

      _view.Flap(_verticalSpeed, _rotateSpeed);
      _view.Rotate(_verticalSpeed, _rotateSpeed);

      CheckIsCollidingWithGround();

      if (_gameOverManager.IsPlayerDead)
      {
        return;
      }

      CheckIsCollidingWithPipe();
    }

    private void CheckIsCollidingWithPipe()
    {
      foreach (PipePair pipePair in _pipeSpawner.IncomingPipePairs)
      {
        foreach (Transform pipe in pipePair.transform)
        {
          if (_collider.IsCollidingWith(pipe.GetComponent<GameBoxCollider>()))
          {
            _gameOverManager.TriggerPlayerDead();
            return;
          }
        }
      }
    }

    private void CheckIsCollidingWithGround()
    {
      if (_collider.IsCollidingWith(_ground.GetComponent<GameBoxCollider>()))
      {
        _verticalSpeed = 0f;
        _gameOverManager.TriggerGameOver();
        return;
      }
    }

    private void ApplyGravity()
    {
      _verticalSpeed += _gravity * _mass * Time.deltaTime;
    }

    private void Flap()
    {
      _verticalSpeed = _flapForce;
    }

    private void HandleFlap(
      UnityEngine.InputSystem.InputAction.CallbackContext ctx
    )
    {
      if (!_gameOverManager.IsGameStarted)
      {
        return;
      }

      if (_gameOverManager.IsPlayerDead)
      {
        return;
      }

      Flap();
    }
  }
}
