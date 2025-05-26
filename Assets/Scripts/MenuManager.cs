using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionProperty _secondaryButtonAction;

    [Header("UI Elements")]
    [SerializeField] private GameObject _MenuCanvas;
    [SerializeField] private GameObject _OptionsPanel;
    [SerializeField] private GameObject _ScoreDetailsPanel;
    [SerializeField] private TextMeshProUGUI _arrows;

    [Header("Trashbins")]
    [SerializeField] private WasteSortingInteractions _packagingTrashbin;
    [SerializeField] private WasteSortingInteractions _glassTrashbin;
    [SerializeField] private WasteSortingInteractions _foodTrashbin;

    [Header("Score Texts")]
    [SerializeField] private TextMeshProUGUI _packagingScoreText;
    [SerializeField] private TextMeshProUGUI _glassScoreText;
    [SerializeField] private TextMeshProUGUI _foodScoreText;

    [Header("Error Texts")]
    [SerializeField] private TextMeshProUGUI _packagingErrorText;
    [SerializeField] private TextMeshProUGUI _glassErrorText;
    [SerializeField] private TextMeshProUGUI _foodErrorText;

    [Header("Total Texts")]
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private TextMeshProUGUI _totalErrorText;
    [SerializeField] private TextMeshProUGUI _averageScoreText;

    private bool _isPaused = false;
    private GameObject _player;
    private bool _arrowsActive = true;
    private Countdown _countdownScript;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _countdownScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Countdown>();
        _MenuCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        _secondaryButtonAction.action.performed += OnPauseButtonPressed;
    }

    private void OnDisable()
    {
        _secondaryButtonAction.action.performed -= OnPauseButtonPressed;
    }

    private void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        _isPaused = !_isPaused;
        _MenuCanvas.SetActive(_isPaused);
        _OptionsPanel.SetActive(_isPaused);
        _ScoreDetailsPanel.SetActive(false);
    }

    public void DisplayScoreDetails()
    {
        _MenuCanvas.SetActive(true);
        _OptionsPanel.SetActive(false);
        ScoreDetailsUpdate();
        _ScoreDetailsPanel.SetActive(true);
        
    }

    public void ScoreDetailsUpdate()
    {
        float totalScore = _packagingTrashbin.getScore() + _glassTrashbin.getScore() + _foodTrashbin.getScore();
        float totalErrors = _packagingTrashbin.getErrorCount() + _glassTrashbin.getErrorCount() + _foodTrashbin.getErrorCount();
        _packagingScoreText.text = "Score : " + _packagingTrashbin.getScore();
        _glassScoreText.text = "Score : " + _glassTrashbin.getScore();
        _foodScoreText.text = "Score : " + _foodTrashbin.getScore();
        _packagingErrorText.text = "Errors : " + _packagingTrashbin.getErrorCount();
        _glassErrorText.text = "Errors : " + _glassTrashbin.getErrorCount();
        _foodErrorText.text = "Errors : " + _foodTrashbin.getErrorCount();
        _totalScoreText.text = "Total Score : " + totalScore;
        _totalErrorText.text = "Total Errors : " + totalErrors;
        _averageScoreText.text = "Score/sc : " + (totalScore / _countdownScript.GetCountdownTotalTime()).ToString("F2");
    }

    public void ToggleArrows()
    {
        _arrowsActive = !_arrowsActive;
        if (_arrowsActive)
        {
            _arrows.text = "Arrows ON";
        }
        else
        {
            _arrows.text = "Arrows OFF";
            DestroAllArrows();
        }
    }

    public bool AreArrowsActive()
    {
        return _arrowsActive;
    }

    private void DestroAllArrows()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Arrow"))
        {
            Destroy(obj);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
