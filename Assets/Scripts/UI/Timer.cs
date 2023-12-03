using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Colorado.UI
{
    public class Timer
    {
        private Image _fillImage;
        private TMP_Text _timerText;
        private int _time;

        public int CurrentTime { get; private set; }

        public Timer(int time, GameObject timerObject)
        {
            _time = time;
            CurrentTime = time;
            _fillImage = timerObject.GetComponentInChildren<Image>();
            _fillImage.fillAmount = 1;
            _timerText = timerObject.GetComponentInChildren<TMP_Text>();
        }

        public void AddTime(int time) => SetTime(CurrentTime + time);
        public void RemoveTime(int time) => SetTime(CurrentTime - time);

        private void SetTime(int time)
        {
            if (time > CurrentTime)
                return;

            CurrentTime = time;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _fillImage.fillAmount = CurrentTime / _time;
            _timerText.text = CurrentTime.ToString();
        }
    }
}