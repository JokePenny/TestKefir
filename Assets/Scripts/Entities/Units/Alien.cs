using System;
using Assets.Scripts.Enitites.Interfaces;
using Assets.Scripts.Enitites.Weapon;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    public class Alien : Unit, IEnemy, IDamagable, IReturnableToPool
    {
        [SerializeField] private float _speed;
        public Unit Target { get; set; }
        public float Speed => _speed;

        public event Action<GameObject> EventGetDamage;
        public event Action<GameObject> EventReturnToPool;

        private void OnTriggerEnter(Collider collider)
        {
            if (!InsidePlayArea)
                return;

            if (collider.gameObject.TryGetComponent(out Ammo ammo))
            {
                GetDamage();
            }
        }

        public void GetDamage()
        {
            EventGetDamage?.Invoke(gameObject);
        }

        public void ReturnToPool()
        {
            EventReturnToPool?.Invoke(gameObject);
        }
    }
}
