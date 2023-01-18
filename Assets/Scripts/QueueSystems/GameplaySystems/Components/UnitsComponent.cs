using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Components
{
    public class UnitsComponent : ComponentSystem
    {
        [SerializeField] private Transform _rootPools;
        private UnitsPool _unitsPool;
        public UnitsPool UnitsPool => _unitsPool;

        public override void Initialize()
        {
            _unitsPool = new UnitsPool(_rootPools);
        }
    }
}
