using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameResultView : View
    {
        [SerializeField] private string _winText;
        [SerializeField] private string _defeatText;

        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private TextMeshProUGUI _gameTime;
        [SerializeField] private Button _playAgainButton;

        private void OnEnable()
        {
            GameListener.GameOver += Show;
            _playAgainButton.onClick.AddListener(Close);
            _playAgainButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            GameListener.GameOver -= Show;
            _playAgainButton.onClick.RemoveListener(Close);
        }

        private void Show(bool isWin, TimeSpan time)
        {
            _resultText.SetText(isWin ? _winText : _defeatText);
            _gameTime.SetText(time.ToString(@"mm\:ss"));
            Open();
        }
    }
}