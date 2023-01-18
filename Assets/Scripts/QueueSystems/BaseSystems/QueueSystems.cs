using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.BaseSystems
{
    public abstract class QueueSystems
    {
        protected Dictionary<TypeSystem, GameSystem> _systems;

        public void ActivateSystem(TypeSystem systemType)
        {
            _systems[systemType].Activate();
        }

        protected void SetConfigToSystems()
        {
            foreach (var system in _systems)
            {
                system.Value.SetConfig(this);
            }
        }

        protected abstract void ConfigureTransitions();
    }
}
