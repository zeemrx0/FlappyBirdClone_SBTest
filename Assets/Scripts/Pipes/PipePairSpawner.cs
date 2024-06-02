using System.Collections.Generic;
using LNE.Colliders;
using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Pipes
{
  public class PipePairSpawner : MonoBehaviour
  {
    public List<PipePair> IncomingPipePairs { get; private set; } =
      new List<PipePair>();

    public PipePair FirstPipePair =>
      IncomingPipePairs.Count > 0 ? IncomingPipePairs[0] : null;

    [SerializeField]
    private PipePairSpawnerData _pipePairSpawnerData;

    private DiContainer _diContainer;
    private GamePlayManager _gamePlayManager;

    private GameBoxCollider _playerCollider;
    private float _timeUntilNextSpawn = 0f;

    [Inject]
    public void Construct(
      DiContainer container,
      GamePlayManager gamePlayManager
    )
    {
      _diContainer = container;
      _gamePlayManager = gamePlayManager;
    }

    private void Start()
    {
      _playerCollider = GameObject
        .FindWithTag(TagName.Player)
        .GetComponent<GameBoxCollider>();
    }

    void Update()
    {
      if (!_gamePlayManager.IsGameStarted)
      {
        return;
      }

      if (_gamePlayManager.IsPlayerDead)
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
        _gamePlayManager.AddPoint();
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
      float spawnInterval = Random.Range(
        _pipePairSpawnerData.MinSpawnInterval,
        _pipePairSpawnerData.MaxSpawnInterval
      );
      _timeUntilNextSpawn = spawnInterval;

      float randomY = Random.Range(
        _pipePairSpawnerData.MinSpawnY,
        _pipePairSpawnerData.MaxSpawnY
      );
      PipePair pipePair = _diContainer
        .InstantiatePrefab(_pipePairSpawnerData.PipePairPrefab)
        .GetComponent<PipePair>();

      pipePair.transform.position = new Vector3(
        transform.position.x,
        transform.position.y + randomY,
        transform.position.z
      );

      pipePair.transform.SetParent(transform);

      IncomingPipePairs.Add(pipePair);

      float randomSpaceBetweenPipes = Random.Range(
        _pipePairSpawnerData.MinSpaceBetweenPipes,
        _pipePairSpawnerData.MaxSpaceBetweenPipes
      );

      pipePair.SetSpaceBetween(randomSpaceBetweenPipes);
    }
  }
}
