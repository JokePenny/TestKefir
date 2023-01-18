using UnityEngine;

namespace Assets.Scripts.Enitites.Units.Configs
{
    [CreateAssetMenu(fileName = "SpawnAlienConfig", menuName = "ScriptableObjects/SpawnAlienConfig")]
    public class SpawnAlienConfig : ScriptableObject
    {
        [SerializeField] private Alien _alienPrefab;
        [SerializeField] private float _periodSpawn;
        [SerializeField] private float _maxLimitSpawn;

        public float PeriodSpawn => _periodSpawn;
        public float MaxLimitSpawn => _maxLimitSpawn;
        public Alien AlienPrefab => _alienPrefab;
    }
}
