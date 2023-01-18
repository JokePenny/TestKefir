using System.Collections.Generic;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using Assets.Scripts.QueueSystems.GameplaySystems.Systems;

namespace Assets.Scripts.QueueSystems.GameplaySystems
{
    public class GameplayQueueUpdatableSystems : BaseSystems.QueueSystems
    {
        public GameplayQueueUpdatableSystems(GameplaySessionComponent gameplaySessionComponent)
        {
            _systems = new Dictionary<TypeSystem, GameSystem>
            {
                [TypeSystem.WatcherGameState] = new WatcherGameStateSystem(gameplaySessionComponent, this),
                [TypeSystem.WatcherStatisticsGamePlay] = new WatcherStatisticsGameplaySystem(gameplaySessionComponent),
                [TypeSystem.WatcherOutOfBorderObjects] = new WatcherOutOfBorderObjectsSystem(gameplaySessionComponent),

                #region Start systems

                [TypeSystem.Start] = new StartPointSystem(),
                [TypeSystem.RestartGame] = new RestartGameSystem(gameplaySessionComponent),
                [TypeSystem.ReflectSheepExitEnterGameField] =
                    new ReflectSheepExitEnterGameFieldSystem(gameplaySessionComponent),
                [TypeSystem.SpawnAliens] = new SpawnAlienSystem(gameplaySessionComponent),
                [TypeSystem.SpawnAsteroids] = new SpawnAsteroidsSystem(gameplaySessionComponent),
                [TypeSystem.SpawnSheep] = new SpawnSheepSystem(gameplaySessionComponent),
                [TypeSystem.UpdateTargetsAliens] = new UpdateTargetsAliensSystem(gameplaySessionComponent),

                #endregion

                #region Update systems

                [TypeSystem.Update] = new UpdatePointSystem(gameplaySessionComponent),
                [TypeSystem.FireSheep] = new FireSheepSystem(gameplaySessionComponent),
                [TypeSystem.UpdateTargetsForNewAliens] = new UpdateTargetsForNewAliensSystem(gameplaySessionComponent),

                #endregion

                #region FixedUpdate systems

                [TypeSystem.FixedUpdate] = new FixedUpdatePointSystem(gameplaySessionComponent),
                [TypeSystem.MoveAliens] = new MoveAliensSystem(gameplaySessionComponent),
                [TypeSystem.MoveAsteroids] = new MoveAsteroidsSystem(gameplaySessionComponent),
                [TypeSystem.MoveBullets] = new MoveBulletsSystem(gameplaySessionComponent),
                [TypeSystem.MoveSheep] = new MoveSheepSystem(gameplaySessionComponent),

                #endregion
            };

            SetConfigToSystems();
            ConfigureTransitions();
        }

        protected override void ConfigureTransitions()
        {
            #region Start systems

            _systems[TypeSystem.Start].SetNextSystem(TypeSystem.SpawnSheep);
            _systems[TypeSystem.RestartGame].SetNextSystem(TypeSystem.SpawnSheep);
            _systems[TypeSystem.SpawnSheep].SetNextSystem(TypeSystem.UpdateTargetsAliens);
            _systems[TypeSystem.UpdateTargetsAliens].SetNextSystem(TypeSystem.ReflectSheepExitEnterGameField);
            _systems[TypeSystem.ReflectSheepExitEnterGameField].SetNextSystem(TypeSystem.WatcherGameState);

            #endregion

            #region Update systems

            _systems[TypeSystem.Update].SetNextSystem(TypeSystem.SpawnAliens);
            _systems[TypeSystem.SpawnAliens].SetNextSystem(TypeSystem.SpawnAsteroids);
            _systems[TypeSystem.SpawnAsteroids].SetNextSystem(TypeSystem.UpdateTargetsForNewAliens);
            _systems[TypeSystem.UpdateTargetsForNewAliens].SetNextSystem(TypeSystem.FireSheep);

            #endregion

            #region FixedUpdate systems

            _systems[TypeSystem.FixedUpdate].SetNextSystem(TypeSystem.MoveAliens);
            _systems[TypeSystem.MoveAliens].SetNextSystem(TypeSystem.MoveAsteroids);
            _systems[TypeSystem.MoveAsteroids].SetNextSystem(TypeSystem.MoveBullets);
            _systems[TypeSystem.MoveBullets].SetNextSystem(TypeSystem.MoveSheep);

            #endregion
        }
    }
}
