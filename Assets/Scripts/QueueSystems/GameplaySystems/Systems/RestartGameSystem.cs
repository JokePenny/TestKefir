using Assets.Scripts.Enitites.Units;
using Assets.Scripts.Enitites.Weapon;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class RestartGameSystem : GameSystem
    {
        private readonly GameplaySessionComponent _sessionComponent;
        private readonly UnitsPool _unitsPool;
        private readonly AmmoPool _ammoPool;
        private readonly SheepStats _sheepStats;

        public RestartGameSystem(GameplaySessionComponent sessionComponent)
        {
            _sessionComponent = sessionComponent;
            _ammoPool = _sessionComponent.Ammo.AmmoPool;
            _unitsPool = _sessionComponent.Units.UnitsPool;
            _sheepStats = _sessionComponent.Configs.SpawnSheepConfig.SheepStats;
        }

        public override void Activate()
        {
            _ammoPool.DisableAllUnits();
            _unitsPool.DisableAllUnits();
            _sheepStats.Velocity = 0f;
            ActivateNextSystem();
        }
    }
}
