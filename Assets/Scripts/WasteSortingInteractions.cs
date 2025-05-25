using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class WasteSortingInteractions : MonoBehaviour
{
    [Header("Score Settings")]
    private int _score = 0;
    private int _errorCount = 0;

    [Header("References")]
    private GameController _gameControllerScript;
    [SerializeField] private string _tagField;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private ParticleSystem _particle;

    private void Start()
    {
        _gameControllerScript = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Waste")) return;
        if (other.CompareTag(_tagField))
        {
            addScore(1);
            _gameControllerScript.addScore(1);
            var main = _particle.main;
            main.startColor = Color.green;
            _particle.Play();
        }

        else
        {
            addError(1);
            _gameControllerScript.addError(1);
            var main = _particle.main;
            main.startColor = Color.red;
            _particle.Play();
        }

        Destroy(other.transform.root.gameObject);
        _gameControllerScript.SpawnWaste();
    }

    public void addScore(int points)
    {
        _score += points;
        _scoreText.text = "Score : " + _score;
    }

    public void addError(int points)
    {
        _errorCount += points;
        _errorText.text = "Errors : " + _errorCount;
    }
}
