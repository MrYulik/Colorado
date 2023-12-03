using Colorado.Systems;
using Colorado.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Colorado.Core
{
    public class GameManager : BaseSystem
    {
        [SerializeField] private GameObject _gameCanvas;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Image _mainColorPanel;
        [SerializeField] private Image[] _variableColorPanel;
        [SerializeField] private GameObject _timerObject;

        private Color _mainColor;
        private Color[] _variableColor;

        private Timer _timer;
        private int _time = 30;
        private int _countRound = 0;

        public bool IsStarted { get; private set; }

        private IEnumerator SecondUpdate()
        {
            if (IsStarted && _timer != null)
            {
                for (;;)
                {
                    _timer.RemoveTime(1);
                    if (_timer.CurrentTime <= 0)
                        LoseRound();

                    yield return new WaitForSeconds(1f);
                }
            }
        }

        private void InitGame()
        {
            _timer = null;
            _time = 30;
            if (_time > 5)
                _time -= _countRound;

            _timer = new Timer(_time, _timerObject);
            if (_countRound == 0)
                StartCoroutine(SecondUpdate());

            _losePanel.SetActive(false);
            _mainColor = new Color(Random.Range(0.09f, 0.89f), Random.Range(0.09f, 0.89f), Random.Range(0.09f, 0.89f));
            _variableColor = new Color[_variableColorPanel.Length];
            for(int i = 0; i < _variableColor.Length; i++)
            {
                var tempColor = new Color(_mainColor.r + Random.Range(0.01f, 0.07f), 
                    _mainColor.g + Random.Range(0.01f, 0.07f), _mainColor.b + Random.Range(0.01f, 0.07f));

                _variableColor[i] = tempColor;
            }

            _variableColor[Random.Range(0, _variableColor.Length)] = _mainColor;

            _mainColorPanel.color = _mainColor;
            for (int i = 0; i < _variableColorPanel.Length; i++)
            {
                _variableColorPanel[i].color = _variableColor[i];
                if (_variableColorPanel[i].GetComponent<ClickObject>() == null) 
                {
                    var click = _variableColorPanel[i].gameObject.AddComponent<ClickObject>();
                    click.OnClick += OnClickVariable;
                }
            }
        }

        private void OnClickVariable(GameObject obj)
        {
            if(IsStarted)
            {
                var tempColor = obj.GetComponent<Image>().color;
                if (tempColor != _mainColor)
                {
                    LoseRound();
                }
                else
                {
                    ++_countRound;
                    InitGame();
                    ScoreManager.Instance.AddScore();
                }
            }
        }

        private void LoseRound()
        {
            IsStarted = false;
            ScoreManager.Instance.SetScore(0);
            _countRound = 0;
            _losePanel.SetActive(true);
            StopAllCoroutines();
        }

        public void StopGame()
        {
            _timer = null;
            IsStarted = false;
            _gameCanvas.SetActive(false);
            _losePanel.SetActive(false);
            _mainColor = Color.white;
            for (int i = 0; i < _variableColorPanel.Length; i++)
            {
                var click = _variableColorPanel[i].gameObject.GetComponent<ClickObject>();
                if(click != null)
                {
                    click.OnClick -= OnClickVariable;
                    Destroy(click);
                }
            }
        }
        
        public void RestartGame()
        {
            _losePanel.SetActive(false);
            _countRound = 0;
            StopAllCoroutines();
            StartRound();
        }

        public void StartRound()
        {
            MenuManager.Instance.CloseAllMenus();
            _gameCanvas.SetActive(true);
            IsStarted = true;
            InitGame();
        }
    }
}