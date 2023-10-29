using UnityEngine;

namespace BotStates
{
    public class Return : BotState
    {
        private Transform _base;

        public override void Enter()
        {
            _base = _bot.Base.transform;
        }

        public override void Process()
        {
            _bot.LookAt(_base);
            _bot.MoveTowards(_base);

            TryDeposit(_base);
        }

        private void TryDeposit(Transform target)
        {
            float distanceSquared = Vector3.SqrMagnitude(_bot.transform.position - target.position);

            if (distanceSquared > _bot.DropoffDistance * _bot.DropoffDistance)
                return;

            _bot.Base.DepositResource(_bot, _bot.TargetResource);
            _bot.ClearResource();

            ChangeState("Idle");
        }
    }
}