using UnityEngine;

namespace Game
{
    public class GameTimeScaler : MonoBehaviour
    {
        [SerializeField] [Range(0.0f, 15.0f)] private float _time = 1.0f; 
        
        private void Update() 
        {
            Time.timeScale = _time;
        }
    }
}