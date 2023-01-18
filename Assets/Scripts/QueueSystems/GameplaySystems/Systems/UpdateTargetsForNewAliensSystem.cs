using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class UpdateTargetsForNewAliensSystem : GameSystem
    {
        private readonly UnitsPool _unitsPool;

        public UpdateTargetsForNewAliensSystem(GameplaySessionComponent sessionComponent)
        {
            _unitsPool = sessionComponent.Units.UnitsPool;
        }

        public override void Activate()
        {
            UpdateTargets();
            ActivateNextSystem();
        }

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

                    continue;
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
