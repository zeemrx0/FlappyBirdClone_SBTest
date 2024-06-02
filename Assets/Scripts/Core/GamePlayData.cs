using UnityEngine;

namespace LNE.Core
{
  [CreateAssetMenu(
    fileName = "GamePlayData",
    menuName = "Core/Game Play Data",
    order = 0
  )]
  public class GamePlayData : ScriptableObject
  {
    [field: SerializeField]
    public float InitialGameSpeed { get; set; }

    [field: SerializeField]
    public float GameSpeedIncrement { get; set; }

    [field: SerializeField]
    public int GameSpeedIncrementInterval { get; set; }
  }
}
