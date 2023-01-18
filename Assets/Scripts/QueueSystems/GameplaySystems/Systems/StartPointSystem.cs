using Assets.Scripts.QueueSystems.BaseSystems;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class StartPointSystem : GameSystem
    {
        public override void Activate()
        {
            ActivateNextSystem();
        }
    }
}
