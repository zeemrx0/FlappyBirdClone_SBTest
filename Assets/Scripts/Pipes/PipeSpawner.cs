using System.Collections.Generic;
using LNE.Colliders;
using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipeSpawner : MonoBehaviour
  {
    public List<PipePair> IncomingPipePairs { get; private set; } =
      new List<PipePair>();

    public PipePair FirstPipePair =>
      IncomingPipePairs.Count > 0 ? IncomingPipePairs[0] : null;

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
    private GamePlayManager _gameCoreManager;

    private GameBoxCollider _playerCollider;
    private float _timeUntilNextSpawn = 0f;

    [Inject]
    public void Construct(
      DiContainer container,
      GamePlayManager gameCoreManager
    )
    {
      _diContainer = container;
      _gameCoreManager = gameCoreManager;
    }

    private void Start()
    {
      _playerCollider = GameObject
        .FindWithTag(TagName.Player)
        .GetComponent<GameBoxCollider>();
    }

    void Update()
    {
      if (!_gameCoreManager.IsGameStarted)
      {
        return;
      }

      if (_gameCoreManager.IsPlayerDead)
      {
        return;
      }

      _timeUntilNextSpawn -= Time.deltaTime;

      if (_timeUntilNextSpawn <= 0)
      {
        SpawnPipePair();
      }

      if (IsFirstPipePairPassedPlayer())
      {
        _gameCoreManager.AddPoint();
      }
    }

    private bool IsFirstPipePairPassedPlayer()
    {
      if (FirstPipePair == null)
      {
        return false;
      }

      // Get one of the colliders from the first pipe pair
      GameBoxCollider firstPipePairCollider = FirstPipePair.transform
        .GetChild(0)
        .GetComponent<GameBoxCollider>();

      // Check if the player has passed the first pipe pair
      if (
        firstPipePairCollider.transform.position.x
          + firstPipePairCollider.Size.x / 2
        < _playerCollider.transform.position.x - _playerCollider.Size.x / 2
      )
      {
        IncomingPipePairs.RemoveAt(0);
        return true;
      }

      return false;
    }

    private void SpawnPipePair()
    {
      float spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
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
