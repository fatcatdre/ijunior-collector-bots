using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _delay;

    private List<SpawnPoint> _spawnPoints;
    private WaitForSeconds _spawnDelay;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());

        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            spawnPoint.OnSpawnedDelegate += OnResourceSpawned;
            spawnPoint.OnEmptiedDelegate += OnResourceTaken;
        }
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnResource());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    private void OnValidate()
    {
        UpdateSpawnDelay();
    }

    private void OnResourceSpawned(SpawnPoint spawnPoint)
    {
        if (spawnPoint == null)
            return;

        _spawnPoints.Remove(spawnPoint);
    }

    private void OnResourceTaken(SpawnPoint spawnPoint)
    {
        if (spawnPoint == null)
            return;

        _spawnPoints.Add(spawnPoint);
    }

    private IEnumerator SpawnResource()
    {
        while (enabled)
        {
            SpawnPoint spawnPoint = GetRandomSpawnPoint();

            if (spawnPoint != null)
                spawnPoint.Spawn();

            yield return _spawnDelay;
        }
    }

    private void UpdateSpawnDelay()
    {
        _spawnDelay = new WaitForSeconds(_delay);
    }

    private SpawnPoint GetRandomSpawnPoint()
    {
        if (_spawnPoints.Count == 0)
            return null;

        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}