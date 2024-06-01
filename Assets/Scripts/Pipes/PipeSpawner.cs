using System.Collections.Generic;
using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipeSpawner : MonoBehaviour
  {
    public List<PipePair> IncomingPipePairs { get; private set; } =
      new List<PipePair>();

    [SerializeField]
    private PipePair _pipePairPrefab;

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

    private DiContainer _diContainer;
    private GameOverManager _gameOverManager;

    private float _timeUntilNextSpawn = 0f;

    [Inject]
    public void Construct(
      DiContainer container,
      GameOverManager gameOverManager
    )
    {
      _diContainer = container;
      _gameOverManager = gameOverManager;
    }

    void Update()
    {
      if (_gameOverManager.IsGameOver)
      {
        return;
      }

      _timeUntilNextSpawn -= Time.deltaTime;

      if (_timeUntilNextSpawn <= 0)
      {
        float spawnInterval = Random.Range(
          _minSpawnInterval,
          _maxSpawnInterval
        );
        _timeUntilNextSpawn = spawnInterval;

        float randomY = Random.Range(_minSpawnY, _maxSpawnY);
        PipePair pipePair = _diContainer
          .InstantiatePrefab(_pipePairPrefab)
          .GetComponent<PipePair>();

        pipePair.transform.position = new Vector3(
          transform.position.x,
          transform.position.y + randomY,
          transform.position.z
        );

        pipePair.transform.SetParent(transform);

        IncomingPipePairs.Add(pipePair);

        float randomSpaceBetweenPipes = Random.Range(
          _minSpaceBetweenPipes,
          _maxSpaceBetweenPipes
        );

        pipePair.SetSpaceBetween(randomSpaceBetweenPipes);
      }
    }
  }
}
