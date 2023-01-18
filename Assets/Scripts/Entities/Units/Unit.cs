using System;
using Assets.Scripts.Enitites.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    public abstract class Unit : MonoBehaviour
    {
        private TypeUnit _typeUnit;

        public TypeUnit TypeUnit
        {
            get => _typeUnit;
            set => _typeUnit = value;
        }

        public bool InsidePlayArea { get; set; }
    }
}
