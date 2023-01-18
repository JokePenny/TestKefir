using Assets.Scripts.Enitites.Weapon.Configs;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon.Configs
{
    public class AmmoFactory
    {
        private readonly AmmoConfig _config;
        private readonly AmmoPool _ammoPool;

        public AmmoFactory(AmmoConfig config, AmmoPool ammoPool)
        {
            _config = config;
            _ammoPool = ammoPool;
            InitializePool(TypeAmmo.SimpleBullet, 20);
            InitializePool(TypeAmmo.Lazer, 4);
        }

        public Ammo GetAmmo(TypeAmmo typeAmmo, Transform spawnPoint)
        {
            Ammo newAmmo = null;
            switch (typeAmmo)
            {
                case TypeAmmo.SimpleBullet:
                    newAmmo = CreateAmmo(typeAmmo, spawnPoint.parent, spawnPoint.transform.position);
                    break;
                case TypeAmmo.Lazer:
                    newAmmo = CreateAmmo(typeAmmo, spawnPoint, spawnPoint.transform.position);
                    break;
            }

            return newAmmo;
        }

        private void InitializePool(TypeAmmo typeAmmo, int count)
        {
            AmmoInfo lazerInfo = _config.GetAmmo(typeAmmo);
            _ammoPool.CreatePool(typeAmmo, lazerInfo.Prefab, count);
        }

        private Ammo CreateAmmo(TypeAmmo typeAmmo, Transform parent, Vector3 position)
        {
            Ammo newAmmo = _ammoPool.GetInactiveAmmo(typeAmmo);
            var transformAmmo = newAmmo.transform;
            transformAmmo.position = position;
            transformAmmo.SetParent(parent);
            return newAmmo;
        }
    }
}
