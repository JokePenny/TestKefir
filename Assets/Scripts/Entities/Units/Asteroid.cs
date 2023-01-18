using System;
using Assets.Scripts.Enitites.Interfaces;
using Assets.Scripts.Enitites.Weapon;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    public class Asteroid : Unit, IEnemy, IDamagable, IReturnableToPool
    {
        [SerializeField] private float _speedMove;
        public float SpeedMove => _speedMove;

        public event Action<GameObject> EventGetDamage;
        public event Action<GameObject> EventReturnToPool;

        public void GetDamage()
        {
            EventGetDamage?.Invoke(gameObject);
        }

        public void ReturnToPool()
        {
            EventReturnToPool?.Invoke(gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!InsidePlayArea)
                return;

            if (collider.gameObject.TryGetComponent(out Ammo ammo))
            {
                GetDamage();
            }
        }
    }
}
