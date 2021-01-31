using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MiniMenuView : View
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _resetButton;

        private GameSaver _gameSaver;
        private GameListener _gameListener;
        
        public override void Init()
        {
            _gameSaver = FindObjectOfType<GameSaver>();
            _saveButton.onClick.AddListener(SaveButtonOnClick);
            _loadButton.onClick.AddListener(LoadButtonOnClick);
            _resetButton.onClick.AddListener(ResetButtonOnClick);
            
            _gameListener = FindObjectOfType<GameListener>();
            _gameListener.GameStarted += Open;
        }

        private void Open(float playerBalls, float enemyBalls)
        {
            Open();
        }

        private void SaveButtonOnClick()
        {
            _gameSaver.Save();
        }

        private void LoadButtonOnClick()
        {
            _gameSaver.Load();
        }

        private void ResetButtonOnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
        }
    }
}