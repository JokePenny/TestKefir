using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enitites.GameField
{
    public class BorderBoxCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        public event Action<GameObject> ExitFromGameField;
        public event Action<GameObject> EnterInGameField;

        public BoxCollider BoxCollider => _boxCollider;

        private void OnTriggerEnter(Collider collider)
        {
            EnterInGameField?.Invoke(collider.gameObject);
        }

        private void OnTriggerExit(Collider collider)
        {
            ExitFromGameField?.Invoke(collider.gameObject);
        }
    }
}
