using UnityEngine;

namespace LNE.Inputs
{
  public class PlayerInputManager : MonoBehaviour
  {
    public PlayerInputAction PlayerInputAction { get; private set; }

    private void OnEnable()
    {
      PlayerInputAction.Enable();
    }

    private void OnDisable()
    {
      PlayerInputAction.Disable();
    }

    public void Init()
    {
      PlayerInputAction ??= new PlayerInputAction();
    }
  }
}
