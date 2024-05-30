using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Movements
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

    private PlayerInputManager _playerInputManager;
    private PlayerInputAction _playerInputAction;
    private BirdMovementView _view;
    
    private float _verticalSpeed = 0f;

    [Inject]
    private void Construct(PlayerInputManager playerInputManager)
    {
      _playerInputManager = playerInputManager;
      _playerInputManager.Init();
      _playerInputAction = _playerInputManager.PlayerInputAction;
    }

    private void Awake()
    {
      _view = GetComponent<BirdMovementView>();
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
      _verticalSpeed += _gravity * _mass * Time.deltaTime;
      _view.Flap(_verticalSpeed, _rotateSpeed);
    }

    private void Flap()
    {
      _verticalSpeed = _flapForce;
    }

    private void HandleFlap(
      UnityEngine.InputSystem.InputAction.CallbackContext ctx
    )
    {
      Flap();
    }
  }
}
