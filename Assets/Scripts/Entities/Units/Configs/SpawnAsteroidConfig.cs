using UnityEngine;

namespace Assets.Scripts.Enitites.Units.Configs
{
    [CreateAssetMenu(fileName = "SpawnAsteroidConfig", menuName = "ScriptableObjects/SpawnAsteroidConfig")]
    public class SpawnAsteroidConfig : ScriptableObject
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Asteroid _fragmentAsteroidPrefab;
        [SerializeField] private float _periodSpawn;
        [SerializeField] private float _maxLimitSpawn;

        public float PeriodSpawn => _periodSpawn;
        public float MaxLimitSpawn => _maxLimitSpawn;
        public Asteroid AsteroidPrefab => _asteroidPrefab;
        public Asteroid FragmentAsteroidPrefab => _fragmentAsteroidPrefab;
    }
}
