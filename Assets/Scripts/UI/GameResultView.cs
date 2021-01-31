using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private GameListener _gameListener;

        public override void Init()
        {
            _gameListener = FindObjectOfType<GameListener>();
            _gameListener.GameOver += Show;
            _playAgainButton.onClick.AddListener(PlayAgainOnButtonClick);
        }

        private void Show(bool isWin, TimeSpan time)
        {
            _resultText.SetText(isWin ? _winText : _defeatText);
            _gameTime.SetText(time.ToString(@"mm\:ss"));
            Open();
        }

        private void PlayAgainOnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
            Close();
        }
    }
}