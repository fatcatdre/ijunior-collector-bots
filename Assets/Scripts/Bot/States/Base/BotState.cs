using UnityEngine;

namespace BotStates
{
    public abstract class BotState : State
    {
        protected Bot _bot;

        protected override void Awake()
        {
            base.Awake();

            _bot = GetComponentInParent<Bot>(true);

            if (_bot == null)
                Debug.LogError($"State {name} couldn't find a Bot script on any parent objects.");
        }
    }
}
