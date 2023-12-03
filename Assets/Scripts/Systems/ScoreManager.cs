using Colorado.Core;
using TMPro;
using UnityEngine;

namespace Colorado.Systems
{
    public class ScoreManager : BaseSingeltonSystem<ScoreManager>
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _bestScoreText;

        private int _score;

        public override void StartGame()
        {
            if (!PlayerPrefs.HasKey("BestScore"))
                PlayerPrefs.SetInt("BestScore", 0);

            _bestScoreText.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore").ToString();
        }

        private void UpdateUI()
        {
            _scoreText.text = _score.ToString();
        }

        private void GetBestScore()
        {
            if (_score > PlayerPrefs.GetInt("BestScore"))
                PlayerPrefs.SetInt("BestScore", _score);

            _bestScoreText.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore").ToString();
        }

        public void AddScore() => SetScore(_score + 1);

        public void SetScore(int score)
        {
            _score = score;
            UpdateUI();
            GetBestScore();
        }
    }
}
