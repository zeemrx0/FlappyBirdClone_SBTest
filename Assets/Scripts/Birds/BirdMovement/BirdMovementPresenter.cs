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

    [SerializeField]
    private float _flapCooldown = 0.2f;

    public float FlapForce => _flapForce;

    private GameCoreManager _gameCoreManager;
    private PlayerInputManager _playerInputManager;
    private PlayerInputAction _playerInputAction;
    private BirdMovementView _view;
    private GameBoxCollider _collider;

    private float _verticalSpeed = 0f;
    private float _timeUntilNextFlap = 0f;

    [Inject]
    private void Construct(
      GameCoreManager gameCoreManager,
      PlayerInputManager playerInputManager
    )
    {
      _gameCoreManager = gameCoreManager;
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
      if (!_gameCoreManager.IsGameStarted || _gameCoreManager.IsGameOver)
      {
        return;
      }

      ApplyGravity();

      _view.Flap(_verticalSpeed);
      _view.Rotate(_verticalSpeed, _rotateSpeed);

      CheckIsCollidingWithGround();

      if (_gameCoreManager.IsPlayerDead)
      {
        return;
      }

      CheckIsCollidingWithPipe();

      if (_timeUntilNextFlap > 0f)
      {
        _timeUntilNextFlap -= Time.deltaTime;
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
          _gameCoreManager.TriggerPlayerDead();
          return;
        }
      }
    }

    private void CheckIsCollidingWithGround()
    {
      if (_collider.IsCollidingWith(_ground.GetComponent<GameBoxCollider>()))
      {
        _verticalSpeed = 0f;
        _gameCoreManager.TriggerGameOver();
        return;
      }
    }

    private void ApplyGravity()
    {
      _verticalSpeed += _gravity * _mass * Time.deltaTime;
    }

    public void Flap()
    {
      _verticalSpeed = _flapForce;
    }

    public bool TryFlap()
    {
      if (_timeUntilNextFlap <= 0f)
      {
        Flap();
        _timeUntilNextFlap = _flapCooldown;
        return true;
      }

      return false;
    }

    private void HandleFlap(
      UnityEngine.InputSystem.InputAction.CallbackContext ctx
    )
    {
      if (!_gameCoreManager.IsGameStarted)
      {
        return;
      }

      if (_gameCoreManager.IsPlayerDead)
      {
        return;
      }

      TryFlap();
    }
  }
}
