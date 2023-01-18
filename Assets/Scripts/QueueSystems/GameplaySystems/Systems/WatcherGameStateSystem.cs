using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.Context;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using Assets.Scripts.UI;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Systems
{
    public sealed class WatcherGameStateSystem : GameSystem
    {
        private readonly WindowsManager _windowsManager;
        private readonly GameplaySessionComponent _sessionComponent;
        private readonly BaseSystems.QueueSystems _queueSystem;

        public WatcherGameStateSystem(GameplaySessionComponent sessionComponent, BaseSystems.QueueSystems queueSystem)
        {
            _queueSystem = queueSystem;
            _sessionComponent = sessionComponent;
            _windowsManager = _sessionComponent.WindowsManager;
            var windowGameOver = (WindowGameOver)_windowsManager.GetWindow<WindowGameOver>();
            windowGameOver.EventRestartGame += OnRestartGameHandle;
            _sessionComponent.EventUpdateStateGame += OnUpdateStateGame;
        }

        public override void Activate()
        {
            ActivateNextSystem();
        }

        private void OnRestartGameHandle()
        {
            _sessionComponent.GameplayState = GameplayState.Play;
        }

        private void OnUpdateStateGame(GameplayState state)
        {
            switch (state)
            {
                case GameplayState.Play:
                    _windowsManager.Close<WindowGameOver>();
                    _windowsManager.Open<WindowStatisticsGameplay>();
                    _queueSystem.ActivateSystem(TypeSystem.RestartGame);
                    break;
                case GameplayState.Defeat:
                    _windowsManager.Close<WindowStatisticsGameplay>();
                    _windowsManager.Open<WindowGameOver>();
                    break;
            }
        }
    }
}
