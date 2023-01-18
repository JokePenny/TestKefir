using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class MoveAliensSystem : GameSystem
    {
        private readonly UnitsPool _unitsPool;

        public MoveAliensSystem(GameplaySessionComponent gameplaySessionComponent)
        {
            _unitsPool = gameplaySessionComponent.Units.UnitsPool;
        }

        public override void Activate()
        {
            MoveAlien();
            ActivateNextSystem();
        }

        private void MoveAlien()
        {
            var listActiveAliens = _unitsPool.GetListActiveUnits(TypeUnit.Alien);
            for (int i = 0; i < listActiveAliens.Count; i++)
            {
                var alien = listActiveAliens[i] as Alien;
                if (alien.Target == null)
                    continue;

                var transformAlien = alien.transform;
                Vector3 direction = alien.Target.transform.position - transformAlien.position;
                transformAlien.position += direction.normalized * alien.Speed * Time.deltaTime;
                transformAlien.forward = direction.normalized;
            }
        }
    }
}
