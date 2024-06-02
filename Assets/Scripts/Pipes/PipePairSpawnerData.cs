using UnityEngine;

namespace LNE.Pipes
{
  [CreateAssetMenu(
    fileName = "PipePairSpawnerData",
    menuName = "Pipes/Pipe Pair Spawner Data",
    order = 0
  )]
  public class PipePairSpawnerData : ScriptableObject
  {
    [field: SerializeField]
    public PipePair PipePairPrefab { get; set; }

    [field: SerializeField]
    public float MinSpawnInterval { get; set; } = 1.5f;

    [field: SerializeField]
    public float MaxSpawnInterval { get; set; } = 2.1f;

    [field: SerializeField]
    public float MinSpawnY { get; set; } = -6f;

    [field: SerializeField]
    public float MaxSpawnY { get; set; } = 6f;

    [field: SerializeField]
    public float MinSpaceBetweenPipes { get; set; } = 24.3f;

    [field: SerializeField]
    public float MaxSpaceBetweenPipes { get; set; } = 25.3f;
  }
}
