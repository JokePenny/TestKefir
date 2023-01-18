using System;
using Assets.Scripts.Enitites.Units;
using UnityEngine;

namespace Assets.Scripts.Enitites.GameField
{
    public class GameFieldBoxCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        public event Action<GameObject> ExitFromGameField;
        public event Action<GameObject> EnterInGameField;

        public BoxCollider BoxCollider => _boxCollider;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Unit unit))
            {
                unit.InsidePlayArea = true;
            }

            EnterInGameField?.Invoke(collider.gameObject);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Unit unit))
            {
                unit.InsidePlayArea = false;
            }

            ExitFromGameField?.Invoke(collider.gameObject);
        }
    }
}
