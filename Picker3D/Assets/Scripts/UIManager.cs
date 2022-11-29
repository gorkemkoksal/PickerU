using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public Canvas RestartCanvas { get; private set; }
    [field: SerializeField] public Canvas LevelEndCanvas { get; private set; }

    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas playingCanvas;
    [SerializeField] private Canvas rampCanvas;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private Image firstBar;
    [SerializeField] private Image secondBar;

    public TextMeshProUGUI moneyText;

    private bool _isFirstBorderPassed;
    private bool _isStarted;

    private void OnEnable()
    {
        Collector.OpeningBorders += ProgressBar;
    }
    private void OnDisable()
    {
        Collector.OpeningBorders -= ProgressBar;
    }
    void Start()
    {
        var currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();

    }
    private void Update()
    {
        if (_isStarted) return;
        if (Input.touchCount > 0)
        {
            _isStarted = true;
            startCanvas.gameObject.SetActive(false);
            playingCanvas.gameObject.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        if (levelIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(levelIndex + 1);
        else
            SceneManager.LoadScene(0);
    }
    private void ProgressBar()
    {
        if (!_isFirstBorderPassed)
        {
            firstBar.gameObject.SetActive(false);
            _isFirstBorderPassed = true;
        }
        else
        {
            secondBar.gameObject.SetActive(false);
        }
    }
}
