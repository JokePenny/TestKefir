using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.BaseSystems
{
    public enum TypeSystem
    {
        None,
        Start,
        Update,
        FixedUpdate,

        #region State system

        WatcherGameState,
        RestartGame,

        #endregion

        #region Gameplay Systems

        WatcherOutOfBorderObjects,
        WatcherStatisticsGamePlay,
        ReflectSheepExitEnterGameField,
        UpdateTargetsAliens,
        UpdateTargetsForNewAliens,
        SpawnAliens,
        MoveAliens,
        SpawnAsteroids,
        MoveAsteroids,
        SpawnSheep,
        MoveSheep,
        FireSheep,
        MoveBullets,

        #endregion
    }
}
