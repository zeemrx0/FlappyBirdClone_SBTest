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
    private BirdMovementData _birdMovementData;

    [SerializeField]
    private PipeSpawner _pipeSpawner;

    [SerializeField]
    private Ground _ground;

    private GameCorePresenter _gameCoreManager;
    private PlayerInputManager _playerInputManager;
    private PlayerInputAction _playerInputAction;
    private BirdMovementView _view;
    private GameBoxCollider _collider;
    private BirdMovementModel _model;

    public float FlapForce => _birdMovementData.FlapForce;

    [Inject]
    private void Construct(
      GameCorePresenter gameCoreManager,
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
      _model = new BirdMovementModel();
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

      _view.Flap(_model.VerticalSpeed, _birdMovementData.RotateSpeed);

      CheckIsCollidingWithGround();

      if (_gameCoreManager.IsPlayerDead)
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
          _gameCoreManager.TriggerPlayerDead();
          return;
        }
      }
    }

    private void CheckIsCollidingWithGround()
    {
      if (_collider.IsCollidingWith(_ground.GetComponent<GameBoxCollider>()))
      {
        _model.VerticalSpeed = 0f;
        _gameCoreManager.TriggerGameOver();
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

    private void HandleFlap(
      UnityEngine.InputSystem.InputAction.CallbackContext ctx
    )
    {
      if (!_gameCoreManager.IsGameStarted || _gameCoreManager.IsPlayerDead)
      {
        return;
      }

      TryFlap();
    }
  }
}
