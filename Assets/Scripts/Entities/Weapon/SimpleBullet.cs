using System;
using Assets.Scripts.Enitites.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon
{
    public class SimpleBullet : Ammo, IDamagable
    {
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private float _speed;
        private Vector3 _direction;
        public float SpeedMove => _speed;
        public Vector3 Direction => _direction;

        public event Action<GameObject> EventGetDamage;

        private void OnEnable()
        {
            _trail.Clear();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IEnemy enemy))
            {
                GetDamage();
            }
        }

        public override void Shoot(Transform startPoint)
        {
            transform.rotation = startPoint.rotation;
            transform.position = startPoint.position;
            _direction = transform.forward;
            gameObject.SetActive(true);
        }

        public void GetDamage()
        {
            EventGetDamage?.Invoke(gameObject);
        }
    }
}
