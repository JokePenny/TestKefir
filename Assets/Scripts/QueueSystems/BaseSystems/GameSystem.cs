using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.BaseSystems
{
    public abstract class GameSystem
    {
        private QueueSystems _systemsConfig;
        private TypeSystem _nextSystem;

        public abstract void Activate();

        public virtual void Dispose()
        {
        }

        public void SetConfig(QueueSystems queueSystems)
        {
            _systemsConfig = queueSystems;
        }

        public void SetNextSystem(TypeSystem nextSystem)
        {
            _nextSystem = nextSystem;
        }

        protected void ActivateNextSystem()
        {
            if (_nextSystem == TypeSystem.None)
                return;

            _systemsConfig.ActivateSystem(_nextSystem);
        }
    }
}
