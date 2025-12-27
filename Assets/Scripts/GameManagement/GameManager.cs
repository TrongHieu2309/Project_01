using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPlaying;
    public bool IsWalking;
    public bool IsStartGame;
    public bool IsGameOver;
    public bool IsPauseGame;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        IsPlaying = false;
        IsWalking = false;
        IsPauseGame = false;
        IsStartGame = false;
        IsGameOver = false;
    }

    void Update()
    {
        HandlePauseGame();
        HandleGameOver();
    }

    private void HandlePauseGame()
    {
        if (IsPauseGame)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void HandleGameOver()
    {
        if (IsGameOver)
        {
            Time.timeScale = 0f;
        }
    }

    public void PauseGame() => IsPauseGame = !IsPauseGame;
    public void GameOver() => IsGameOver = !IsGameOver;

    public void StartGame()
    {
        IsPlaying = true;
        IsWalking = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}