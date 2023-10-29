using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _resourceDetectionRadius;
    [SerializeField] private float _scanFrequency;

    private Coroutine _scanCoroutine;
    private WaitForSeconds _frequency;
    private List<Resource> _scannedResources = new();

    public List<Resource> ScannedResources => new(_scannedResources);
    public int ScannedResourcesCount => _scannedResources.Count;

    private void Awake()
    {
        OnValidate();
    }

    private void OnEnable()
    {
        _scanCoroutine = StartCoroutine(ScanForResources());
    }

    private void OnDisable()
    {
        StopCoroutine(_scanCoroutine);
    }

    private void OnValidate()
    {
        _frequency = new WaitForSeconds(_scanFrequency);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _resourceDetectionRadius);
    }

    public void Remove(Resource resouce)
    {
        _scannedResources.Remove(resouce);
    }

    private IEnumerator ScanForResources()
    {
        while (enabled)
        {
            Collider[] results = Physics.OverlapSphere(transform.position, _resourceDetectionRadius);

            foreach (var result in results)
            {
                if (result.TryGetComponent(out Resource resource) == false)
                    continue;

                if (resource.HasBeenScanned)
                    continue;

                resource.HasBeenScanned = true;

                _scannedResources.Add(resource);
            }

            yield return _frequency;
        }
    }
}
