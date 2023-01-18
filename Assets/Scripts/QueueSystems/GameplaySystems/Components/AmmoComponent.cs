using Assets.Scripts.Enitites.Weapon;
using Assets.Scripts.QueueSystems.BaseSystems;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Components
{
    public class AmmoComponent : ComponentSystem
    {
        [SerializeField] private Transform _rootPools;
        private AmmoPool _ammoPool;
        public AmmoPool AmmoPool => _ammoPool;

        public override void Initialize()
        {
            _ammoPool = new AmmoPool(_rootPools);
        }
    }
}
