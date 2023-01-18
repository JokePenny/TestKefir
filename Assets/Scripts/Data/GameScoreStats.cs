using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class GameScoreStats
    {
        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                PlayerPrefs.SetInt(PlayerPrefsKeys.Score, Score);
            }
        }
    }
}
