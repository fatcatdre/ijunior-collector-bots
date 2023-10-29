using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    private bool _isFull;

    public delegate void OnSpawned(SpawnPoint spawnPoint);
    public delegate void OnEmptied(SpawnPoint spawnPoint);

    public event OnSpawned OnSpawnedDelegate;
    public event OnEmptied OnEmptiedDelegate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource _))
        {
            _isFull = true;

            OnSpawnedDelegate?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Resource _))
        {
            _isFull = false;

            OnEmptiedDelegate?.Invoke(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    public void Spawn()
    {
        if (_isFull)
            return;

        Instantiate(_prefab, transform.position, Quaternion.identity);
    }
}
