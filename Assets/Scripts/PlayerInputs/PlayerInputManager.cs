using UnityEngine;

namespace LNE.Inputs
{
  public class PlayerInputManager : MonoBehaviour
  {
    [field: SerializeField]
    public TapArea TapArea { get; private set; }
  }
}
