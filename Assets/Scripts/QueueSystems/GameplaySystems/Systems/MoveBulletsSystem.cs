using Assets.Scripts.Enitites.Weapon;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class MoveBulletsSystem : GameSystem
    {
        private readonly AmmoPool _ammoPool;

        public MoveBulletsSystem(GameplaySessionComponent gameplaySessionComponent)
        {
            _ammoPool = gameplaySessionComponent.Ammo.AmmoPool;
        }

        public override void Activate()
        {
            MoveBullets();
            ActivateNextSystem();
        }

        private void MoveBullets()
        {
            var listActiveBullets = _ammoPool.GetListActiveUnits(TypeAmmo.SimpleBullet);
            for (int i = 0; i < listActiveBullets.Count; i++)
            {
                var bullet = listActiveBullets[i] as SimpleBullet;
                bullet.transform.position += bullet.Direction * bullet.SpeedMove * Time.deltaTime;
            }
        }
    }
}
