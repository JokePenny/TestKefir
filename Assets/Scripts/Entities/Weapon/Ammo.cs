using System;
using Assets.Scripts.Enitites.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon
{
    public abstract class Ammo : MonoBehaviour, IReturnableToPool
    {
        private TypeAmmo _typeAmmo;

        public event Action<GameObject> EventReturnToPool;

        public TypeAmmo TypeAmmo
        {
            get => _typeAmmo;
            set => _typeAmmo = value;
        }

        public void ReturnToPool()
        {
            EventReturnToPool?.Invoke(gameObject);
        }

        public abstract void Shoot(Transform startPoint);
    }
}
