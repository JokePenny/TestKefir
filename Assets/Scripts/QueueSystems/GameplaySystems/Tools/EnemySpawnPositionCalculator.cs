using UnityEngine;

namespace Assets.Scripts.QueueSystems.GameplaySystems.Tools
{
    public class EnemySpawnPositionCalculator
    {
        private Vector3 _limitScreen;

        public EnemySpawnPositionCalculator()
        {
            Vector3 screenPoint = new Vector3(Screen.width, Screen.height, 0);
            _limitScreen = Camera.main.ScreenToWorldPoint(screenPoint) * 2;
            _limitScreen.y = 0;
        }

        public Vector3 Calculate()
        {
            var isVerticalSpawn = Random.Range(0, 100) > 50;
            Vector3 positionSpawn = Vector3.zero;
            if (isVerticalSpawn)
            {
                var isDownSpawn = Random.Range(0, 100) > 50;
                if (isDownSpawn)
                {
                    positionSpawn = new Vector3(Random.Range(-_limitScreen.x, _limitScreen.x), 0f, _limitScreen.z);
                }
                else
                {
                    positionSpawn = new Vector3(Random.Range(-_limitScreen.x, _limitScreen.x), 0f, -_limitScreen.z);
                }
            }
            else
            {
                var isLeftSpawn = Random.Range(0, 100) > 50;
                if (isLeftSpawn)
                {
                    positionSpawn = new Vector3(_limitScreen.x, 0f, Random.Range(-_limitScreen.z, _limitScreen.z));
                }
                else
                {
                    positionSpawn = new Vector3(-_limitScreen.x, 0f, Random.Range(-_limitScreen.z, _limitScreen.z));
                }
            }

            return positionSpawn;
        }
    }
}
