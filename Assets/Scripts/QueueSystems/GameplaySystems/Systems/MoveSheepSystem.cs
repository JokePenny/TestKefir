using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class MoveSheepSystem : GameSystem
    {
        private readonly InputController _input;
        private readonly Vector3 _limitScreen;
        private readonly UnitsPool _unitsPool;
        private Sheep _mainSheep;

        public MoveSheepSystem(GameplaySessionComponent gameplaySessionComponent)
        {
            _unitsPool = gameplaySessionComponent.Units.UnitsPool;
            _input = new InputController();
            _input.Enable();
            Vector3 screenPoint = new Vector3(Screen.width, Screen.height, 0);
            _limitScreen = Camera.main.ScreenToWorldPoint(screenPoint) * 2;
            _limitScreen.y = 0;
        }

        public override void Activate()
        {
            MoveSheep();
            ActivateNextSystem();
        }

        private void MoveSheep()
        {
            var units = _unitsPool.GetListActiveUnits(TypeUnit.Sheep);
            if (_mainSheep == null || _mainSheep.TypeGenerationSheep != TypeGenerationSheep.Main)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    var findMainSheep = units[i] as Sheep;
                    if (findMainSheep.TypeGenerationSheep == TypeGenerationSheep.Main)
                    {
                        _mainSheep = findMainSheep;
                        break;
                    }
                }
            }

            var inputVector = _input.Player.Move.ReadValue<Vector2>();
            var sheepStats = _mainSheep.SheepStats;
            sheepStats.Velocity += inputVector.magnitude * sheepStats.Acceleration * Time.deltaTime;
            sheepStats.Velocity -= sheepStats.Velocity * sheepStats.Drag * Time.deltaTime;
            _mainSheep.transform.position += _mainSheep.transform.forward * sheepStats.Velocity * sheepStats.SpeedMove *
                                             Time.deltaTime;
            sheepStats.Position = _mainSheep.transform.position;
            var upPosition = sheepStats.Position;
            var downPosition = sheepStats.Position;
            var leftPosition = sheepStats.Position;
            var rightPosition = sheepStats.Position;

            upPosition.z += _limitScreen.z;
            downPosition.z -= _limitScreen.z;
            leftPosition.x += _limitScreen.x;
            rightPosition.x -= _limitScreen.x;

            for (int i = 0; i < units.Count; i++)
            {
                var sheep = units[i] as Sheep;
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

            var deltaChangeRotate = _input.Player.Rotate.ReadValue<Vector2>().x;
            if (deltaChangeRotate != 0)
            {
                var currentAngle = _mainSheep.transform.rotation.eulerAngles;
                var newAngle = new Vector3(currentAngle.x,
                    currentAngle.y + deltaChangeRotate * Time.deltaTime * sheepStats.SpeedRotation, currentAngle.z);
                Quaternion newRotation = Quaternion.Euler(newAngle);
                sheepStats.Rotation = newAngle;

                for (int i = 0; i < units.Count; i++)
                {
                    units[i].transform.rotation = newRotation;
                }
            }
        }
    }
}
