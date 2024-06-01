using UnityEngine;

namespace LNE.Birds
{
  [CreateAssetMenu(
    fileName = "BirdMovementData",
    menuName = "Birds/Movement Data",
    order = 0
  )]
  public class BirdMovementData : ScriptableObject
  {
    [field: SerializeField]
    public float FlapForce { get; private set; } = 18f;

    [field: SerializeField]
    public float Gravity { get; private set; } = -9.8f;

    [field: SerializeField]
    public float Mass { get; private set; } = 8f;

    [field: SerializeField]
    public float RotateSpeed { get; private set; } = 1f;

    [field: SerializeField]
    public float FlapCooldown { get; private set; } = 0.1f;
  }
}
