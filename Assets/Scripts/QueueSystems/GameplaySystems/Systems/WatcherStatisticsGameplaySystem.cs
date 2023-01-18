using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class WatcherStatisticsGameplaySystem : GameSystem
    {
        private readonly WindowStatisticsGameplay _windowStatisticsGameplay;
        private readonly SheepStats _sheepStats;
        private readonly AmmoStats _ammoStats;

        public WatcherStatisticsGameplaySystem(GameplaySessionComponent sessionComponent)
        {
            _windowStatisticsGameplay =
                (WindowStatisticsGameplay)sessionComponent.WindowsManager.GetWindow<WindowStatisticsGameplay>();

            var configs = sessionComponent.Configs;
            _sheepStats = configs.SpawnSheepConfig.SheepStats;
            _ammoStats = configs.AmmoConfig.AmmoStats;
            _sheepStats.EventChangePosition += OnUpdateCoordinateSheep;
            _sheepStats.EventChangeRotation += OnUpdateAngleRotationSheep;
            _sheepStats.EventChangeVelocity += OnUpdateVelocitySheep;
            _ammoStats.EventChangeCurrentAmmoLazer += OnUpdateAmmoLazer;
            _ammoStats.EventChangeTimeCooldownLazer += OnUpdateCooldownLazer;
        }

        public override void Activate()
        {
            ActivateNextSystem();
        }

        public void OnUpdateCoordinateSheep(Vector3 position)
        {
            _windowStatisticsGameplay.UpdateCoordinateSheep(position);
        }

        public void OnUpdateAngleRotationSheep(Vector3 angle)
        {
            _windowStatisticsGameplay.UpdateAngleRotationSheep(angle);
        }

        public void OnUpdateVelocitySheep(float velocity)
        {
            _windowStatisticsGameplay.UpdateVelocitySheep(velocity);
        }

        public void OnUpdateAmmoLazer(int ammo)
        {
            _windowStatisticsGameplay.UpdateAmmoLazer(ammo);
        }

        public void OnUpdateCooldownLazer(float cooldown)
        {
            _windowStatisticsGameplay.UpdateCooldownLazer(cooldown);
        }
    }
}
