using System;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon.Configs
{
    [Serializable]
    public class AmmoInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private TypeAmmo _type;
        [SerializeField] private GameObject _prefab;

        public TypeAmmo Type => _type;
        public GameObject Prefab => _prefab;
    }
}
