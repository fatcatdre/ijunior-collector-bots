using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _resourceHolder;
    [SerializeField] private float _pickupDistance;
    [SerializeField] private float _dropoffDistance;

    private StateMachine _stateMachine;
    private Resource _resource;
    private Base _base;

    public Resource TargetResource => _resource;
    public Transform ResourceHolder => _resourceHolder;
    public Base Base => _base;
    public float PickupDistance => _pickupDistance;
    public float DropoffDistance => _dropoffDistance;

    private void Awake()
    {
        _stateMachine = GetComponentInChildren<StateMachine>();
    }

    public void SetBase(Base homeBase)
    {
        _base = homeBase;
    }

    public void LookAt(Transform target)
    {
        if (target == null)
            return;

        Vector3 lookAtPosition = new(target.position.x, transform.position.y, target.position.z);

        transform.LookAt(lookAtPosition);
    }

    public void MoveTowards(Transform target)
    {
        if (target == null)
            return;

        float previousY = transform.position.y;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        newPosition.y = previousY;

        transform.position = newPosition;
    }

    public void GatherResource(Resource resource)
    {
        _resource = resource;

        _stateMachine.ChangeState("Gather");
    }

    public void ClearResource()
    {
        _resource = null;
    }
}
