using Assets.Scripts.Enitites.Units;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class MoveAsteroidsSystem : GameSystem
    {
        private readonly UnitsPool _unitsPool;

        public MoveAsteroidsSystem(GameplaySessionComponent gameplaySessionComponent)
        {
            _unitsPool = gameplaySessionComponent.Units.UnitsPool;
        }

        public override void Activate()
        {
            MoveAsteroids();
            ActivateNextSystem();
        }

        private void MoveAsteroids()
        {
            var listActiveAsteroids = _unitsPool.GetListActiveUnits(TypeUnit.Asteroid);
            for (int i = 0; i < listActiveAsteroids.Count; i++)
            {
                var asteroid = listActiveAsteroids[i] as Asteroid;
                asteroid.transform.position += asteroid.transform.forward * asteroid.SpeedMove * Time.deltaTime;
            }

            var listActiveFragmentAsteroids = _unitsPool.GetListActiveUnits(TypeUnit.FragmentAsteroid);
            for (int i = 0; i < listActiveFragmentAsteroids.Count; i++)
            {
                var asteroid = listActiveFragmentAsteroids[i] as Asteroid;
                asteroid.transform.position += asteroid.transform.forward * asteroid.SpeedMove * Time.deltaTime;
            }
        }
    }
}
