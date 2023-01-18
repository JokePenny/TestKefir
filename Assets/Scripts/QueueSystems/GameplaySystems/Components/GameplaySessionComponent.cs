using System;
using Assets.Scripts.Data;
using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.Context;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Components
{
    public class GameplaySessionComponent : ComponentSystem
    {
        [SerializeField] private UnitsComponent _unitsComponent;
        [SerializeField] private AmmoComponent _ammoComponent;
        [SerializeField] private ConfigsComponent _configsComponent;
        [SerializeField] private GameFieldComponent _gameFieldComponent;
        [SerializeField] private WindowsManager _windowsManager;
        private GameplayState _gameplayState;
        private GameScoreStats _scoreStats;

        public event Action<GameplayState> EventUpdateStateGame;

        public GameplayState GameplayState
        {
            get => _gameplayState;
            set
            {
                _gameplayState = value;
                EventUpdateStateGame?.Invoke(_gameplayState);
            }
        }

        public UnitsComponent Units => _unitsComponent;
        public AmmoComponent Ammo => _ammoComponent;
        public ConfigsComponent Configs => _configsComponent;
        public GameFieldComponent GameFieldComponent => _gameFieldComponent;
        public WindowsManager WindowsManager => _windowsManager;
        public GameScoreStats GameScoreStats => _scoreStats;

        public override void Initialize()
        {
            _scoreStats = new GameScoreStats();
            _windowsManager.Initialize();
            _gameFieldComponent.Initialize();
            _unitsComponent.Initialize();
            _ammoComponent.Initialize();
        }
    }
}
