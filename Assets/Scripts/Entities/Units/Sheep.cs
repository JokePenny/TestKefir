using System;
using System.Collections;
using Assets.Scripts.Enitites.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    public class Sheep : Unit, IDamagable
    {
        [SerializeField] private SheepStats _sheepStats;
        [SerializeField] private Rigidbody _rigibody;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _pointGun;
        [SerializeField] private TrailRenderer[] _trails;

        public SheepStats SheepStats
        {
            get => _sheepStats;
            set { _sheepStats = value; }
        }

        public TypeGenerationSheep TypeGenerationSheep { get; set; }
        public Transform PointGun => _pointGun;

        public event Action<GameObject> EventGetDamage;

        private void OnEnable()
        {
            foreach (var trail in _trails)
            {
                trail.enabled = false;
                trail.Clear();
            }

            StartCoroutine(EnableTrails());
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!InsidePlayArea)
                return;

            if (collider.gameObject.TryGetComponent(out IEnemy enemy))
            {
                GetDamage();
            }
        }

        public void ClearTrails()
        {
            foreach (var trail in _trails)
            {
                trail.Clear();
            }
        }

        public void GetDamage()
        {
            EventGetDamage?.Invoke(gameObject);
        }

        private IEnumerator EnableTrails()
        {
            yield return null;

            foreach (var trail in _trails)
            {
                trail.enabled = true;
            }
        }
    }
}
