using LNE.Inputs;
using UnityEngine;
using Zenject;

public class BirdMovement : MonoBehaviour
{
  private PlayerInputManager _playerInputManager;
  private PlayerInputAction _playerInputAction;

  [Inject]
  private void Construct(PlayerInputManager playerInputManager)
  {
    _playerInputManager = playerInputManager;
    _playerInputManager.Init();
    _playerInputAction = _playerInputManager.PlayerInputAction;
  }

  private void OnEnable()
  {
    _playerInputAction.BirdMovement.Flap.performed += ctx => HandleFlap(ctx);
  }

  private void HandleFlap(
    UnityEngine.InputSystem.InputAction.CallbackContext ctx
  ) { }
}
