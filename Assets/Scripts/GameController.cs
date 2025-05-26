using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Global Score Settings")]
    private int _globalScore = 0;
    private int _globalErrorCount = 0;
    private bool _isGameOver = false;
    private Countdown _countdownScript;
    private MenuManager _menuManagerScript;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _errorText;

    [Header("Trashbins")]
    [SerializeField] private WasteSortingInteractions _packagingTrashbin;
    [SerializeField] private WasteSortingInteractions _glassTrashbin;
    [SerializeField] private WasteSortingInteractions _foodTrashbin;

    [Header("Spawns")]
    [SerializeField] private Transform[] _spawns;

    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _scoreSound;
    [SerializeField] private AudioClip _errorSound;

    [Header("Waste Prefabs")]
    [SerializeField] private GameObject[] _packagingWaste;
    [SerializeField] private GameObject[] _foodWaste;
    [SerializeField] private GameObject[] _glassWaste;

    private void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        _countdownScript = gameController.GetComponent<Countdown>();
        _menuManagerScript = gameController.GetComponent<MenuManager>();
        SpawnWaste();
    }

    public void SpawnWaste()
    {
        if (_isGameOver) return;
        int randomWasteType = Random.Range(0, 3);
        GameObject[] wastes = null;
        Transform spawnPoint = _spawns[Random.Range(0, _spawns.Length)];
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
        Instantiate(wasteToSpawn, new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.5f, spawnPoint.position.z), Quaternion.identity);
    }

    public void GameOver()
    {
        _isGameOver = true;
        _menuManagerScript.DisplayScoreDetails();
        DestroyAllWaste();
    }

    private void DestroyAllWaste()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("Untagged")) continue;
            if (obj.tag == "Waste")
            {
                Destroy(obj.gameObject);
            }
        }
    }

    public void StartGame()
    {
        DestroyAllWaste();
        _isGameOver = false;
        ResetStats();
        SpawnWaste();
        _countdownScript.BeginTimer();
    }

    private void ResetStats()
    {
        _packagingTrashbin.ResetStats();
        _glassTrashbin.ResetStats();
        _foodTrashbin.ResetStats();
        _globalScore = 0;
        _globalErrorCount = 0;
        _scoreText.text = "Total score : " + _globalScore;
        _errorText.text = "Total errors : " + _globalErrorCount;
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
