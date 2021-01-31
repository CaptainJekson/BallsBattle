using System.IO;
using Serializable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Creators
{
    public class InitGame : MonoBehaviour
    {
        private Config _config;
        private static string _path;

        public GameConfig GameConfig => _config.GameConfig;

        private void Awake()
        {
            LoadConfig();
            LoadScenes();
        }
        
        private void LoadConfig()
        {
            _config = new Config();

#if UNITY_ANDROID && !UNITY_EDITOR
            _path = Path.Combine(Application.persistentDataPath, "GameConfig.json");
#else
            _path = Path.Combine(Application.dataPath, "GameConfig.json");
#endif
            if (File.Exists(_path))
            {
                _config = JsonUtility.FromJson<Config>(File.ReadAllText(_path));
            }
        }

        private void LoadScenes()
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
