using UnityEngine;

namespace LNE.Birds
{
  [CreateAssetMenu(
    fileName = "AIBirdData",
    menuName = "Birds/AI Data",
    order = 0
  )]
  public class AIBirdData : ScriptableObject
  {
    [field: SerializeField]
    public float TargetGap { get; set; } = -0.4f;

    [field: SerializeField]
    public float FlapCooldownCoefficient { get; set; } = 0.0001f;

    [field: SerializeField]
    public float MaxFlapCooldown { get; set; } = 0.15f;
  }
}
