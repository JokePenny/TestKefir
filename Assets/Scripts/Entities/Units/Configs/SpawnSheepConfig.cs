using UnityEngine;

namespace Assets.Scripts.Enitites.Units.Configs
{
    [CreateAssetMenu(fileName = "SpawnSheepConfig", menuName = "ScriptableObjects/SpawnSheepConfig")]
    public class SpawnSheepConfig : ScriptableObject
    {
        [SerializeField] private Sheep _sheepPrefab;
        [SerializeField] private SheepStats _sheepStats;

        public Sheep SheepPrefab => _sheepPrefab;

        public SheepStats SheepStats
        {
            get => _sheepStats;
            set { _sheepStats = value; }
        }
    }
}
