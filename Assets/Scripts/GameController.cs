using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Global Score Settings")]
    private int _globalScore = 0;
    private int _globalErrorCount = 0;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _errorText;

    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _scoreSound;
    [SerializeField] private AudioClip _errorSound;

    [Header("Waste Prefabs")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject[] _packagingWaste;
    [SerializeField] private GameObject[] _foodWaste;
    [SerializeField] private GameObject[] _glassWaste;


    public void SpawnWaste()
    {
        int randomWasteType = Random.Range(0, 3);
        GameObject[] wastes = null;
        switch (randomWasteType)
        {
            case 0:
                wastes = _packagingWaste;
                break;
            case 1:
                wastes = _foodWaste;
                break;
            case 2:
                wastes = _glassWaste;
                break;
        }

        int randomIndex = Random.Range(0, wastes.Length);
        GameObject wasteToSpawn = wastes[randomIndex];
        Instantiate(wasteToSpawn, _spawnPoint.position, Quaternion.identity);
    }
    public void addScore(int points)
    {
        _globalScore += points;
        _scoreText.text = "Total score : " + _globalScore;
        _audioSource.PlayOneShot(_scoreSound);
    }

    public void addError(int points)
    {
        _globalErrorCount += points;
        _errorText.text = "Total errors : " + _globalErrorCount;
        _audioSource.PlayOneShot(_errorSound);
    }

    public int getGlobalScore()
    {
        return _globalScore;
    }

    public int getGlobalErrorCount()
    {
        return _globalErrorCount;
    }
    
    
}
