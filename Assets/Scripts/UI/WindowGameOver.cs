using System;
using Assets.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public sealed class WindowGameOver : Window
    {
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private Button _buttonRestart;

        public event Action EventRestartGame;

        private void Start()
        {
            _buttonRestart.onClick.AddListener(OnClickButtonStart);
        }

        public override void Open()
        {
            base.Open();
            _textScore.text = PlayerPrefs.GetInt(PlayerPrefsKeys.Score).ToString();
        }

        private void OnClickButtonStart()
        {
            Close();
            EventRestartGame?.Invoke();
        }
    }
}
