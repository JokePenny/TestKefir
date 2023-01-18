using Assets.Scripts.Enitites.GameField;
using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class ReflectSheepExitEnterGameFieldSystem : GameSystem
    {
        private readonly GameFieldBoxCollider _gameField;
        private readonly UnitsPool _unitsPool;
        private readonly Vector3 _limitScreen;

        public ReflectSheepExitEnterGameFieldSystem(GameplaySessionComponent sessionComponent)
        {
            _unitsPool = sessionComponent.Units.UnitsPool;
            _gameField = sessionComponent.GameFieldComponent.GameField;
            _gameField.ExitFromGameField += OnExitFromGameFieldObject;

            Vector3 screenPoint = new Vector3(Screen.width, Screen.height, 0);
            _limitScreen = Camera.main.ScreenToWorldPoint(screenPoint) * 2;
            _limitScreen.y = 0;
        }

        private void OnExitFromGameFieldObject(GameObject collision)
        {
            if (collision.gameObject.TryGetComponent(out Sheep sheep))
            {
                if (sheep.TypeGenerationSheep != TypeGenerationSheep.Main)
                    return;

                var activeSheeps = _unitsPool.GetListActiveUnits(TypeUnit.Sheep);
                foreach (var activeSheep in activeSheeps)
                {
                    if (!activeSheep.InsidePlayArea)
                        continue;

                    var nextMainSheep = activeSheep as Sheep;
                    var position = nextMainSheep.transform.position;
                    var upPosition = position;
                    var downPosition = position;
                    var leftPosition = position;
                    var rightPosition = position;

                    upPosition.z += _limitScreen.z;
                    downPosition.z -= _limitScreen.z;
                    leftPosition.x += _limitScreen.x;
                    rightPosition.x -= _limitScreen.x;

                    switch (nextMainSheep.TypeGenerationSheep)
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

                    sheep.TypeGenerationSheep = nextMainSheep.TypeGenerationSheep;
                    sheep.ClearTrails();
                    nextMainSheep.TypeGenerationSheep = TypeGenerationSheep.Main;
                    nextMainSheep.ClearTrails();
                    return;
                }
            }
        }

        public override void Activate()
        {
            ActivateNextSystem();
        }
    }
}
