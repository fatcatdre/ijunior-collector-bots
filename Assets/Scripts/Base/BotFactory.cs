using UnityEngine;

public class BotFactory : MonoBehaviour
{
    [SerializeField] private Bot _prefab;
    [SerializeField] private Transform _spawnPoint;

    public Bot CreateBot()
    {
        return Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
