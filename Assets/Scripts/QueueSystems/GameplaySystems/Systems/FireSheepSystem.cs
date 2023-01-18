using Assets.Scripts.Enitites.Units;
using Assets.Scripts.Enitites.Weapon;
using Assets.Scripts.Enitites.Weapon.Configs;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.Context;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class FireSheepSystem : GameSystem
    {
        private readonly InputController _input;
        private readonly AmmoFactory _ammoFactory;
        private readonly UnitsPool _unitsPool;
        private readonly AmmoStats _ammoStats;
        private readonly GameplaySessionComponent _sessionComponent;

        public FireSheepSystem(GameplaySessionComponent sessionComponent)
        {
            _sessionComponent = sessionComponent;
            var ammoConfigs = _sessionComponent.Configs.AmmoConfig;
            _ammoStats = ammoConfigs.AmmoStats;
            _unitsPool = _sessionComponent.Units.UnitsPool;
            _ammoFactory = new AmmoFactory(ammoConfigs, _sessionComponent.Ammo.AmmoPool);
            _input = new InputController();
            _input.Enable();
            _input.Player.FireBullets.performed += OnClickFireBullets;
            _input.Player.FireLazers.performed += OnClickFireLazers;
        }

        public override void Activate()
        {
            UpdateCooldownLazer();
            ActivateNextSystem();
        }

        private void UpdateCooldownLazer()
        {
            if (_ammoStats.CurrentAmmoLazer < _ammoStats.CountMaxAmmoLazer)
            {
                if (_ammoStats.CurrentCooldownLazer <= 0f)
                {
                    _ammoStats.CurrentCooldownLazer = _ammoStats.TimeCooldownLazer;
                    _ammoStats.CurrentAmmoLazer++;
                }
                else
                {
                    _ammoStats.CurrentCooldownLazer -= Time.deltaTime;
                }
            }
        }

        private void OnClickFireBullets(InputAction.CallbackContext args)
        {
            if (_sessionComponent.GameplayState != GameplayState.Play)
                return;

            var activeSheeps = _unitsPool.GetListActiveUnits(TypeUnit.Sheep);
            foreach (var activeSheep in activeSheeps)
            {
                if (!activeSheep.InsidePlayArea)
                    continue;

                var sheep = activeSheep as Sheep;
                var ammo = _ammoFactory.GetAmmo(TypeAmmo.SimpleBullet, sheep.transform);
                ammo.Shoot(sheep.PointGun);
            }
        }

        private void OnClickFireLazers(InputAction.CallbackContext args)
        {
            if (_ammoStats.CurrentAmmoLazer <= 0 || _sessionComponent.GameplayState != GameplayState.Play)
                return;

            _ammoStats.CurrentAmmoLazer--;
            _ammoStats.CurrentCooldownLazer = _ammoStats.TimeCooldownLazer;

            var activeSheeps = _unitsPool.GetListActiveUnits(TypeUnit.Sheep);
            foreach (var activeSheep in activeSheeps)
            {
                var sheep = activeSheep as Sheep;
                var ammo = _ammoFactory.GetAmmo(TypeAmmo.Lazer, activeSheep.transform);
                ammo.Shoot(sheep.PointGun);
            }
        }
    }
}
