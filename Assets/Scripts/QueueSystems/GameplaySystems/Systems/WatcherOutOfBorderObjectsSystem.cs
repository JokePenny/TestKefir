using Assets.Scripts.Enitites.GameField;
using Assets.Scripts.Enitites.Interfaces;
using Assets.Scripts.Enitites.Units;
using Assets.Scripts.Enitites.Weapon;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class WatcherOutOfBorderObjectsSystem : GameSystem
    {
        private readonly WindowStatisticsGameplay _windowStatisticsGameplay;
        private readonly UnitsPool _unitsPool;
        private readonly AmmoPool _ammoPool;
        private readonly BorderBoxCollider _borderBoxCollider;

        public WatcherOutOfBorderObjectsSystem(GameplaySessionComponent sessionComponent)
        {
            _unitsPool = sessionComponent.Units.UnitsPool;
            _borderBoxCollider = sessionComponent.GameFieldComponent.Border;
            _borderBoxCollider.ExitFromGameField += OnExitFromGameFieldObject;
        }

        public override void Activate()
        {
            ActivateNextSystem();
        }

        private void OnExitFromGameFieldObject(GameObject colliderOutOfBorder)
        {
            if (colliderOutOfBorder.TryGetComponent(out IReturnableToPool returnableToPool))
            {
                returnableToPool.ReturnToPool();
            }
        }
    }
}
