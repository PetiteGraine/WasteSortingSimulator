using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputActionProperty _secondaryButtonAction;
    [SerializeField] private GameObject _pauseMenuUI;
    private bool _isPaused = false;

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
        _pauseMenuUI.SetActive(_isPaused);
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
