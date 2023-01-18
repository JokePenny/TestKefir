using Assets.Scripts.Enitites.Units.Configs;
using Assets.Scripts.Enitites.Weapon.Configs;
using Assets.Scripts.QueueSystems.BaseSystems;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Components
{
    public class ConfigsComponent : ComponentSystem
    {
        [SerializeField] private SpawnAlienConfig _spawnAlienConfig;
        [SerializeField] private SpawnAsteroidConfig _spawnAsteroidConfig;
        [SerializeField] private SpawnSheepConfig _spawnSheepConfig;
        [SerializeField] private AmmoConfig _ammoConfig;

        public SpawnAlienConfig SpawnAlienConfig => _spawnAlienConfig;
        public SpawnAsteroidConfig SpawnAsteroidConfig => _spawnAsteroidConfig;
        public SpawnSheepConfig SpawnSheepConfig => _spawnSheepConfig;
        public AmmoConfig AmmoConfig => _ammoConfig;
    }
}
