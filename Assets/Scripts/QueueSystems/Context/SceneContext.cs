using Assets.Scripts.QueueSystems.BaseSystems;
using Assets.Scripts.QueueSystems.GameplaySystems;
using Assets.Scripts.QueueSystems.GameplaySystems.Components;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.Context
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private GameplaySessionComponent _gameplaySessionComponent;
        private GameplayQueueUpdatableSystems _gameplayQueueUpdatableSystems;

        private void Awake()
        {
            _gameplaySessionComponent.Initialize();
            _gameplayQueueUpdatableSystems = new GameplayQueueUpdatableSystems(_gameplaySessionComponent);
        }

        private void Start()
        {
            _gameplaySessionComponent.GameplayState = GameplayState.Play;
        }

        private void Update()
        {
            _gameplayQueueUpdatableSystems.ActivateSystem(TypeSystem.Update);
        }

        private void FixedUpdate()
        {
            _gameplayQueueUpdatableSystems.ActivateSystem(TypeSystem.FixedUpdate);
        }
    }
}
