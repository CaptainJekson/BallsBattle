using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeScaleView : View
    {
        [SerializeField] private Slider _slider;
        
        public override void Init()
        {
            _slider.onValueChanged.AddListener(ChangeSpeedOfTime);
            
            Open();
        }

        private void ChangeSpeedOfTime(float value)
        {
            Time.timeScale = value;
        }
    }
}