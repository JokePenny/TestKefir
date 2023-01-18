using Assets.Scripts.Data;
using Assets.Scripts.Enitites.Units;
using Assets.Scripts.Enitites.Units.Configs;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using Assets.Scripts.QueueSystems.GameplaySystems.Tools;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class SpawnAlienSystem : GameSystem
    {
        private readonly EnemySpawnPositionCalculator _positionCalculator;
        private readonly SpawnAlienConfig _config;
        private readonly UnitsPool _unitsPool;
        private readonly GameScoreStats _gameScoreStats;
        private float _currentTime;

        public SpawnAlienSystem(GameplaySessionComponent sessionComponent)
        {
            _gameScoreStats = sessionComponent.GameScoreStats;
            _config = sessionComponent.Configs.SpawnAlienConfig;
            _unitsPool = sessionComponent.Units.UnitsPool;
            _unitsPool.CreatePool(TypeUnit.Alien, _config.AlienPrefab, 10);
            _positionCalculator = new EnemySpawnPositionCalculator();
        }

        public override void Activate()
        {
            if (_currentTime >= _config.PeriodSpawn)
            {
                var activeAliens = _unitsPool.GetListActiveUnits(TypeUnit.Alien);
                if (activeAliens.Count < _config.MaxLimitSpawn)
                {
                    var newRandomPosition = _positionCalculator.Calculate();
                    Spawn(newRandomPosition);
                }

                _currentTime = 0f;
            }
            else
            {
                _currentTime += Time.deltaTime;
            }

            ActivateNextSystem();
        }

        private void Spawn(Vector3 position)
        {
            var alien = _unitsPool.GetInactiveUnit(TypeUnit.Alien) as Alien;
            alien.transform.position = position;
            alien.gameObject.SetActive(true);
            alien.EventGetDamage += OnGetDamageHandler;
        }

        private void OnGetDamageHandler(GameObject alien)
        {
            _gameScoreStats.Score++;
            var unit = alien.GetComponent<Unit>();
            (unit as Alien).EventGetDamage -= OnGetDamageHandler;
            _unitsPool.PushToInactiveUnit(unit);
        }
    }
}
