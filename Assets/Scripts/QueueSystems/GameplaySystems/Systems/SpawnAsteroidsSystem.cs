using Assets.Scripts.Data;
using Assets.Scripts.Enitites.Units;
using Assets.Scripts.Enitites.Units.Configs;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using Assets.Scripts.QueueSystems.GameplaySystems.Tools;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class SpawnAsteroidsSystem : GameSystem
    {
        private readonly EnemySpawnPositionCalculator _positionCalculator;
        private readonly SpawnAsteroidConfig _config;
        private readonly UnitsPool _unitsPool;
        private readonly GameScoreStats _gameScoreStats;
        private float _currentTime;

        public SpawnAsteroidsSystem(GameplaySessionComponent gameplaySessionComponent)
        {
            _gameScoreStats = gameplaySessionComponent.GameScoreStats;
            _config = gameplaySessionComponent.Configs.SpawnAsteroidConfig;
            _unitsPool = gameplaySessionComponent.Units.UnitsPool;
            _unitsPool.CreatePool(TypeUnit.Asteroid, _config.AsteroidPrefab, 10);
            _unitsPool.CreatePool(TypeUnit.FragmentAsteroid, _config.FragmentAsteroidPrefab, 20);
            _positionCalculator = new EnemySpawnPositionCalculator();
        }

        public override void Activate()
        {
            if (_currentTime >= _config.PeriodSpawn)
            {
                var activeAsteroids = _unitsPool.GetListActiveUnits(TypeUnit.Asteroid);
                if (activeAsteroids.Count < _config.MaxLimitSpawn)
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
            var asteroid = _unitsPool.GetInactiveUnit(TypeUnit.Asteroid) as Asteroid;
            asteroid.transform.position = position;
            asteroid.transform.LookAt(-position);
            asteroid.gameObject.SetActive(true);
            asteroid.EventGetDamage += OnGetDamageHandler;
        }

        private void OnGetDamageHandler(GameObject asteroid)
        {
            var unit = asteroid.GetComponent<Unit>();
            (unit as Asteroid).EventGetDamage -= OnGetDamageHandler;
            _unitsPool.PushToInactiveUnit(unit);
            _gameScoreStats.Score++;

            var angleStep = 60f;
            for (int i = 0; i < 2; i++)
            {
                CreateLittleAsteroid(angleStep, i, asteroid.transform);
            }

            angleStep = -60f;
            for (int i = 0; i < 2; i++)
            {
                CreateLittleAsteroid(angleStep, i, asteroid.transform);
            }
        }

        private void CreateLittleAsteroid(float angleStep, int index, Transform asteroid)
        {
            var fragmentAsteroid = _unitsPool.GetInactiveUnit(TypeUnit.FragmentAsteroid) as Asteroid;
            float angle = angleStep * (index + 1);
            var direction = Quaternion.Euler(0, angle, 0) * asteroid.forward;
            fragmentAsteroid.transform.position = asteroid.position;
            fragmentAsteroid.transform.rotation = Quaternion.LookRotation(direction);
            fragmentAsteroid.gameObject.SetActive(true);
            fragmentAsteroid.EventGetDamage += OnGetDamageLitleAsteroidHandler;
        }

        private void OnGetDamageLitleAsteroidHandler(GameObject fragmentAsteroid)
        {
            var unit = fragmentAsteroid.GetComponent<Unit>();
            _gameScoreStats.Score++;
            (unit as Asteroid).EventGetDamage -= OnGetDamageHandler;
            _unitsPool.PushToInactiveUnit(unit);
        }
    }
}
