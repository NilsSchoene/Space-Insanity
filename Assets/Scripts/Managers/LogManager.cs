using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Leaderboards;
using System;
using Unity.Services.Leaderboards.Models;
using System.Collections.Generic;

public class LogManager : Singleton<LogManager>
{
    private int lastScore = 0;
    private int playerHighscore = 0;
    private string playerId;
    public string playerName;
    const string leaderboardId = "spaceshooter_leaderboard_config";
    private bool offline = false;
    private bool loginError = false;

    void Start()
    {
        //Initialize();
        Login();
    }

    private async void Initialize()
    {
        // SignIn
        await UnityServices.InitializeAsync();
        await SignInAnonymouslyAsync();

        // Get the players highscore
        while (playerName == null)
        {
            await GetPlayerHighScoreAsync();
        }

        // Store the players username
        UIManager.Instance.UpdateUsername(playerName);
    }

    private async void Login()
    {
        loginError = false;
        // Init
        await InitializeUnityServices();

        // SignIn
        if(!loginError)
        {
            await SignInAnonymouslyAsync();
        }

        // Get highscore
        if(!loginError)
        {
            await GetPlayerHighScoreAsync();
        }

        // Get player name
        if(!loginError)
        {
            await GetPlayerNameAsync();
        }
    }

    async Task InitializeUnityServices()
    {
        try
        {
            UIManager.Instance.UpdateLoginStatus("Initializing...");
            await UnityServices.InitializeAsync();
            UIManager.Instance.UpdateLoginStatus("Initialization successful");
        }
        catch (Exception ex)
        {
            UIManager.Instance.UpdateLoginStatus("Initialization failed");
            Debug.LogException(ex);
            UIManager.Instance.EnableRetryAndPlayOffline();
            loginError = true;
        }
    }

    async Task SignInAnonymouslyAsync()
    {
        try
        {
            UIManager.Instance.UpdateLoginStatus("Logging in...");
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            playerId = AuthenticationService.Instance.PlayerId;
            UIManager.Instance.UpdateLoginStatus("Login successful");
        }
        catch (AuthenticationException ex)
        {
            UIManager.Instance.UpdateLoginStatus("Login failed");
            Debug.LogException(ex);
            UIManager.Instance.EnableRetryAndPlayOffline();
            loginError = true;
        }
        catch (RequestFailedException ex)
        {
            UIManager.Instance.UpdateLoginStatus("Login failed");
            Debug.LogException(ex);
            UIManager.Instance.EnableRetryAndPlayOffline();
            loginError = true;
        }
    }

    async Task GetPlayerHighScoreAsync()
    {
        try
        {
            UIManager.Instance.UpdateLoginStatus("Fetching highscore...");
            var playerList = new List<string>{playerId};
            var highscoreResponse = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(leaderboardId, playerList);
            Debug.Log($"Player Highscore: {highscoreResponse.Results[0].Score}");
            playerHighscore = (int)highscoreResponse.Results[0].Score;
            GameManager.Instance.highscore = playerHighscore;
            UIManager.Instance.UpdateLoginStatus("Highscore fetched successful");
        }
        catch (Exception ex)
        {
            UIManager.Instance.UpdateLoginStatus("Failed to fetch highscore");
            UIManager.Instance.UpdateLoginStatus("Adding player to leaderboard");
            playerHighscore = 0;
            AddScore();
            Debug.Log(ex);
        }
    }

    async Task GetPlayerNameAsync()
    {
        try
        {
            UIManager.Instance.UpdateLoginStatus("Fetching player name...");
            var playerList = new List<string> { playerId };
            var response = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(leaderboardId, playerList);
            playerName = response.Results[0].PlayerName;
            UIManager.Instance.UpdateLoginStatus("Player name fetched successful");
            UIManager.Instance.UpdateUsername(playerName);
            UIManager.Instance.DisableLoginPanel();
        }
        catch (Exception ex)
        {
            UIManager.Instance.UpdateLoginStatus("Failed to get player name");
            Debug.LogException(ex);
            UIManager.Instance.EnableRetryAndPlayOffline();
            loginError = true;
        }
    }

    public void RetryLogin()
    {
        Login();
    }

    public void PlayOffline()
    {
        offline = true;
    }

    public void SaveScoreAndUpdate(int score)
    {
        lastScore = score;
        if (score > playerHighscore)
        {
            AddScore();
            playerHighscore = score;
            GameManager.Instance.highscore = playerHighscore;
        }
    }

    public async void AddScore()
    {
        if(!offline)
        {
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, lastScore);
                Debug.Log($"Score updated successful");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }

    public void OnNewGame()
    {
        lastScore = 0;
    }
}
