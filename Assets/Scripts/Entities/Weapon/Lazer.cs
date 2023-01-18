using System.Collections;
using Assets.Scripts.Enitites.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon
{
    public class Lazer : Ammo, IReturnableToPool
    {
        [SerializeField] private Transform _lazer;
        [SerializeField] private Vector3 _targetScale;
        [SerializeField] private Vector3 _targetScaleHide;
        [SerializeField] private Vector3 _startScale;
        [SerializeField] private float _timeWork;
        private Vector3 _startPosition;
        private Vector3 _shiftAmmount;

        public override void Shoot(Transform startPoint)
        {
            transform.position = startPoint.position;
            transform.forward = startPoint.forward;
            transform.localScale = _startScale;
            _startPosition = transform.localPosition;
            gameObject.SetActive(true);
            _shiftAmmount = (_targetScale - transform.localScale) / 2f;
            StartCoroutine(ScretchLazer());
        }

        private IEnumerator ScretchLazer()
        {
            var time = Time.time;
            while (Time.time - time < _timeWork)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _startPosition + _shiftAmmount,
                    (Time.time - time) / _timeWork);
                transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, (Time.time - time) / _timeWork);
                yield return null;
            }

            ReturnToPool();
        }
    }
}
