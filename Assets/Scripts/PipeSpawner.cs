using LNE.Core;
using UnityEngine;

namespace LNE.Spawners
{
  public class PipeSpawner : MonoBehaviour
  {
    [SerializeField]
    private PipePair _pipePrefab;

    [SerializeField]
    private float _minSpawnInterval = 1.5f;

    [SerializeField]
    private float _maxSpawnInterval = 2.1f;

    [SerializeField]
    private float _minSpawnY = -6f;

    [SerializeField]
    private float _maxSpawnY = 6f;

    [SerializeField]
    private float _minSpaceBetweenPipes = 24.3f;

    [SerializeField]
    private float _maxSpaceBetweenPipes = 25.3f;

    private float _timeUntilNextSpawn = 0f;

    void Update()
    {
      _timeUntilNextSpawn -= Time.deltaTime;

      if (_timeUntilNextSpawn <= 0)
      {
        float spawnInterval = Random.Range(
          _minSpawnInterval,
          _maxSpawnInterval
        );
        _timeUntilNextSpawn = spawnInterval;

        float randomY = Random.Range(_minSpawnY, _maxSpawnY);
        PipePair pipePair = Instantiate(
          _pipePrefab,
          new Vector3(
            transform.position.x,
            transform.position.y + randomY,
            transform.position.z
          ),
          Quaternion.identity,
          transform
        );

        float randomSpaceBetweenPipes = Random.Range(
          _minSpaceBetweenPipes,
          _maxSpaceBetweenPipes
        );
        pipePair.SetSpaceBetween(randomSpaceBetweenPipes);
      }
    }
  }
}
