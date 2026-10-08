using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GameManager controls game state, score tracking, and lifecycle events for the 2D Unity scene.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public int Score { get; private set; } = 0;
    public bool IsPaused { get; private set; } = false;

    [Header("Events")]
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<bool> OnPauseToggled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        OnPauseToggled?.Invoke(IsPaused);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Score = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}