using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Scores
    public int score = 0;
    public int highscore = 0;

    // Events
    public event Action OnStartGameplay;
    public event Action OnGamePause;
    public event Action OnGameUnpause;
    public event Action OnPlayerDeath;

    // References
    [SerializeField]
    private Player player;
    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private TMP_Text scoreText;

    // Timer
    private float timer = 0;
    private bool timerRunning = false;
    [SerializeField]
    private float difficultyInterval = 5;

    // Audio
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;
    [SerializeField]
    private AudioClip gameOverAudio;

    void Start()
    {
        scoreText.SetText($"Score: {score}");
        AudioManager.Instance.ChangeMusic(menuMusic);
    }

    void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;
            IncreaseDifficulty(timer);
        }
    }

    public void StartGameplay()
    {
        score = 0;
        timer = 0;
        scoreText.SetText($"Score: {score}");
        LogManager.Instance.OnNewGame();
        player.alive = true;
        OnStartGameplay?.Invoke();
        timerRunning = true;
        AudioManager.Instance.ChangeMusic(gameMusic);
    }

    public void IncreaseScore()
    {
        if (timerRunning)
        {
            score += 1;
            scoreText.SetText($"Score: {score}");
        }
    }

    private void IncreaseDifficulty(float seconds)
    {
        if(seconds >= difficultyInterval)
        {
            timer -= difficultyInterval;
            enemySpawner.IncreaseDifficulty();
        }
    }

    public void PauseGame()
    {
        OnGamePause?.Invoke();
        timerRunning = false;
    }

    public void UnpauseGame()
    {
        OnGameUnpause?.Invoke();
        timerRunning = true;
    }

    public void EndGame()
    {
        timerRunning = false;
        OnPlayerDeath?.Invoke();
        UIManager.Instance.GameOver(score, highscore);
        LogManager.Instance.SaveScoreAndUpdate(score);
        StartCoroutine(AudioManager.Instance.PlayMusicAfterSound(gameOverAudio, menuMusic));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
