using UnityEngine;

namespace BotStates
{
    public class Gather : BotState
    {
        private Transform _resource;

        public override void Enter()
        {
            if (_bot.TargetResource == null)
                ChangeState("Idle");

            _resource = _bot.TargetResource.transform;
        }

        public override void Process()
        {
            if (_resource == null)
                ChangeState("Idle");

            _bot.LookAt(_resource);
            _bot.MoveTowards(_resource);

            TryPickUp(_resource);
        }

        private void TryPickUp(Transform target)
        {
            float distanceSquared = Vector3.SqrMagnitude(_bot.transform.position - target.position);

            if (distanceSquared > _bot.PickupDistance * _bot.PickupDistance)
                return;

            target.parent = _bot.ResourceHolder;
            target.localPosition = Vector3.zero;

            ChangeState("Return");
        }
    }
}
