using System;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    [Serializable]
    public class AmmoStats
    {
        [SerializeField] private int _countMaxAmmoLazer;
        [SerializeField] private int _currentAmmoLazer;
        [SerializeField] private float _timeCooldownLazer;
        [SerializeField] private float _currentCooldownLazer;

        public event Action<int> EventChangeCurrentAmmoLazer;
        public event Action<float> EventChangeTimeCooldownLazer;

        public int CountMaxAmmoLazer => _countMaxAmmoLazer;
        public float TimeCooldownLazer => _timeCooldownLazer;

        public int CurrentAmmoLazer
        {
            get => _currentAmmoLazer;
            set
            {
                _currentAmmoLazer = value;
                EventChangeCurrentAmmoLazer?.Invoke(_currentAmmoLazer);
            }
        }

        public float CurrentCooldownLazer
        {
            get => _currentCooldownLazer;
            set
            {
                _currentCooldownLazer = value;
                EventChangeTimeCooldownLazer?.Invoke(_currentCooldownLazer);
            }
        }
    }
}
