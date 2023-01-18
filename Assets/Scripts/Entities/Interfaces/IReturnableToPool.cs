using System;
using UnityEngine;

namespace Assets.Scripts.Enitites.Interfaces
{
    public interface IReturnableToPool
    {
        public event Action<GameObject> EventReturnToPool;
        public void ReturnToPool();
    }
}
