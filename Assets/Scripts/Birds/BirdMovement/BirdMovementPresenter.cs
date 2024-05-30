using LNE.Colliders;
using LNE.Inputs;
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
    private GameObject _pipePairsContainer;

    private PlayerInputManager _playerInputManager;
    private PlayerInputAction _playerInputAction;
    private BirdMovementView _view;
    private GameBoxCollider _collider;
    
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
      _verticalSpeed += _gravity * _mass * Time.deltaTime;
      _view.Flap(_verticalSpeed, _rotateSpeed);

      foreach (Transform pipePair in _pipePairsContainer.transform)
      {
        foreach (Transform pipe in pipePair)
        {
          if (_collider.IsCollidingWith(pipe.GetComponent<GameBoxCollider>()))
          {
            Debug.Log("Game Over!");
          }
        }
      }
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
