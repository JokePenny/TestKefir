using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.Context;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class FixedUpdatePointSystem : GameSystem
    {
        private readonly GameplaySessionComponent _sessionComponent;

        public FixedUpdatePointSystem(GameplaySessionComponent sessionComponent)
        {
            _sessionComponent = sessionComponent;
        }

        public override void Activate()
        {
            if (_sessionComponent.GameplayState == GameplayState.Play)
            {
                ActivateNextSystem();
            }
        }
    }
}
