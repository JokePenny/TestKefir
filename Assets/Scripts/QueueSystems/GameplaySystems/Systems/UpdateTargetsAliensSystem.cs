using Assets.Scripts.Enitites.GameField;
using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class UpdateTargetsAliensSystem : GameSystem
    {
        private readonly GameFieldBoxCollider _gameFieldBoxCollider;
        private readonly UnitsPool _unitsPool;

        public UpdateTargetsAliensSystem(GameplaySessionComponent sessionComponent)
        {
            _gameFieldBoxCollider = sessionComponent.GameFieldComponent.GameField;
            _gameFieldBoxCollider.ExitFromGameField += OnReflectGameFieldChanges;
            _gameFieldBoxCollider.EnterInGameField += OnReflectGameFieldChanges;
            _unitsPool = sessionComponent.Units.UnitsPool;
            UpdateTargets();
        }

        public override void Activate()
        {
            UpdateTargets();
            ActivateNextSystem();
        }

        private void OnReflectGameFieldChanges(GameObject collision) => UpdateTargets();

        private void UpdateTargets()
        {
            var unitsAlien = _unitsPool.GetListActiveUnits(TypeUnit.Alien);
            var unitsSheep = _unitsPool.GetListActiveUnits(TypeUnit.Sheep);

            foreach (var unitAlien in unitsAlien)
            {
                var alien = unitAlien as Alien;

                if (alien.Target != null)
                {
                    if (!alien.Target.InsidePlayArea)
                    {
                        alien.Target = null;
                    }
                }

                foreach (var unitSheep in unitsSheep)
                {
                    if (!unitSheep.InsidePlayArea)
                        continue;

                    if (alien.Target == null)
                    {
                        alien.Target = unitSheep;
                    }

                    var distanceToOldTarget = (alien.transform.position - alien.Target.transform.position).magnitude;
                    var distanceToNewTarget = (alien.transform.position - unitSheep.transform.position).magnitude;
                    if (distanceToOldTarget > distanceToNewTarget || !alien.Target.InsidePlayArea)
                    {
                        alien.Target = unitSheep;
                    }
                }
            }
        }
    }
}
