using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float _totalTime = 30f;
    private bool _isCountdownTimerOn = false;

    [Header("UI Elements")]
    private float _countdownTime;
    [SerializeField] private TextMeshProUGUI _timerText;

    private TimeSpan _timePlaying;
    private GameObject _gameplayController;

    private void Start()
    {
        _timerText.text = "Timer : " + _totalTime.ToString("F2");
        _gameplayController = GameObject.FindGameObjectWithTag("GameController");
        BeginTimer();
    }

    public void BeginTimer()
    {
        _countdownTime = _totalTime;
        _timerText.text = "Timer : " + _countdownTime.ToString("F2");
        _isCountdownTimerOn = true;
        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        _isCountdownTimerOn = false;
        StopCoroutine(UpdateTimer());
    }

    private void EndTimer()
    {
        _isCountdownTimerOn = false;
        _gameplayController.GetComponent<GameController>().GameOver();
        StopCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (_isCountdownTimerOn)
        {
            _countdownTime -= Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(_countdownTime);
            string timePlayingStr = _timePlaying.ToString(@"ss\.ff");
            _timerText.text = "Timer : " + timePlayingStr;
            if (_countdownTime <= 0)
            {
                EndTimer();
            }
            yield return null;
        }
    }
}