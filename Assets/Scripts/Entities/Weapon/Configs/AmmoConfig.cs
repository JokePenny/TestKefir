using System.Collections.Generic;
using Assets.Scripts.Enitites.Units;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon.Configs
{
    [CreateAssetMenu(fileName = "AmmoConfig", menuName = "ScriptableObjects/AmmoConfig")]
    public class AmmoConfig : ScriptableObject
    {
        [SerializeField] private List<AmmoInfo> _listAmmo;
        [SerializeField] private AmmoStats _ammoStats;

        public AmmoStats AmmoStats
        {
            get => _ammoStats;
            set { _ammoStats = value; }
        }

        public AmmoInfo GetAmmo(TypeAmmo type)
        {
            var indexAmmo = _listAmmo.FindIndex(findAmmo => findAmmo.Type == type);

            if (indexAmmo != -1)
            {
                return _listAmmo[indexAmmo];
            }

            Debug.LogError("Not find ammo by type: " + type);
            return null;
        }
    }
}
