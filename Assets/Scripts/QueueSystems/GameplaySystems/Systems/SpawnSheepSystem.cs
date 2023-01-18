using Assets.Scripts.Enitites.Units;
using Assets.Scripts.Enitites.Units.Configs;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.Context;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class SpawnSheepSystem : GameSystem
    {
        private readonly GameplaySessionComponent _gameplaySessionComponent;
        private readonly SpawnSheepConfig _sheepConfig;
        private readonly UnitsPool _unitsPool;
        private readonly Vector3 _limitScreen;

        public SpawnSheepSystem(GameplaySessionComponent gameplaySessionComponent)
        {
            _gameplaySessionComponent = gameplaySessionComponent;
            var unitsComponent = gameplaySessionComponent.Units;
            var configs = gameplaySessionComponent.Configs;
            _sheepConfig = configs.SpawnSheepConfig;
            _unitsPool = unitsComponent.UnitsPool;
            _unitsPool.CreatePool(TypeUnit.Sheep, _sheepConfig.SheepPrefab, 5);
            Vector3 screenPoint = new Vector3(Screen.width, Screen.height, 0);
            _limitScreen = Camera.main.ScreenToWorldPoint(screenPoint) * 2;
            _limitScreen.y = 0;
        }

        public override void Activate()
        {
            var mainSheep = SpawnSheep(TypeGenerationSheep.Main);
            SpawnSheep(TypeGenerationSheep.Top);
            SpawnSheep(TypeGenerationSheep.Down);
            SpawnSheep(TypeGenerationSheep.Left);
            SpawnSheep(TypeGenerationSheep.Right);

            var position = mainSheep.transform.position;
            var upPosition = position;
            var downPosition = position;
            var leftPosition = position;
            var rightPosition = position;

            upPosition.z += _limitScreen.z;
            downPosition.z -= _limitScreen.z;
            leftPosition.x += _limitScreen.x;
            rightPosition.x -= _limitScreen.x;

            var activeSheeps = _unitsPool.GetListActiveUnits(TypeUnit.Sheep);
            for (int i = 0; i < activeSheeps.Count; i++)
            {
                var sheep = activeSheeps[i] as Sheep;
                switch (sheep.TypeGenerationSheep)
                {
                    case TypeGenerationSheep.Top:
                        sheep.transform.position = upPosition;
                        break;
                    case TypeGenerationSheep.Down:
                        sheep.transform.position = downPosition;
                        break;
                    case TypeGenerationSheep.Left:
                        sheep.transform.position = leftPosition;
                        break;
                    case TypeGenerationSheep.Right:
                        sheep.transform.position = rightPosition;
                        break;
                }
            }
        }

        private Sheep SpawnSheep(TypeGenerationSheep typeGeneration)
        {
            var sheep = _unitsPool.GetInactiveUnit(TypeUnit.Sheep) as Sheep;
            sheep.gameObject.SetActive(true);
            sheep.TypeGenerationSheep = typeGeneration;
            sheep.transform.position = Vector3.zero;
            sheep.transform.rotation = Quaternion.identity;
            sheep.SheepStats = _sheepConfig.SheepStats;
            sheep.EventGetDamage += OnGetDamageHandler;
            return sheep;
        }

        private void OnGetDamageHandler(GameObject sheep)
        {
            _gameplaySessionComponent.GameplayState = GameplayState.Defeat;
        }
    }
}
