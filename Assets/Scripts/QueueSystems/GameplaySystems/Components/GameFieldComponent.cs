using Assets.Scripts.Enitites.GameField;
using Assets.Scripts.QueueSystems.BaseSystems;
using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Components
{
    public class GameFieldComponent : ComponentSystem
    {
        [SerializeField] private GameFieldBoxCollider _gameField;
        [SerializeField] private BorderBoxCollider _border;
        public GameFieldBoxCollider GameField => _gameField;
        public BorderBoxCollider Border => _border;

        public override void Initialize()
        {
            Vector3 screenPoint = new Vector3(Screen.width, Screen.height, 0);
            Vector3 limitScreen = Camera.main.ScreenToWorldPoint(screenPoint) * 2;
            limitScreen.y = 10f;
            _gameField.BoxCollider.size = limitScreen;
            _border.BoxCollider.size = limitScreen * 2;
        }
    }
}
