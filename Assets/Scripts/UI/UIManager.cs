using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    // Panels
    [SerializeField]
    private GameObject menuPanelGO;
    [SerializeField]
    private GameObject leaderboardPanelGO;
    [SerializeField]
    private UILeaderboard leaderboardUIElement;
    [SerializeField]
    private GameObject gameOverPanelGO;
    [SerializeField]
    private UIGameOver gameOverUIElement;
    [SerializeField]
    private GameObject loginPanelGO;

    // Login
    [SerializeField]
    private TMP_Text usernameText;
    [SerializeField]
    private TMP_Text LoginStatusText;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button playOfflineButton;
    [SerializeField]
    private Image loadingVisual;


    public void GameOver(int score, int highscore)
    {
        DisableMenuPanels();
        SwitchMenuPanel(gameOverPanelGO);
        gameOverUIElement.Initialize(score, highscore);
    }

    public void UpdateUsername(string username)
    {
        usernameText.text = username;
    }

    public void UpdateLoginStatus(string LoginStatus)
    {
        LoginStatusText.text = LoginStatus;
    }

    public void OnRetryButtonClick()
    {
        LogManager.Instance.RetryLogin();
        playOfflineButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        loadingVisual.gameObject.SetActive(true);
    }

    public void OnPlayOfflineButtonClick()
    {
        LogManager.Instance.PlayOffline();
        UpdateUsername("Offline Mode");
        retryButton.gameObject.SetActive(false);
        playOfflineButton.gameObject.SetActive(false);
        DisableLoginPanel();
    }

    public void OnMenuButtonClick()
    {
        SwitchMenuPanel(menuPanelGO);
    }

    public void OnPlayButtonClick()
    {
        DisableMenuPanels();
        GameManager.Instance.StartGameplay();
    }

    public void OnLeaderboardButtonClick()
    {
        SwitchMenuPanel(leaderboardPanelGO);
        leaderboardUIElement.Initialize();
    }

    public void OnQuitButtonClick()
    {
        GameManager.Instance.QuitGame();
    }

    private void SwitchMenuPanel(GameObject panelGO)
    {
        DisableMenuPanels();
        panelGO.SetActive(true);
    }

    private void DisableMenuPanels()
    {
        menuPanelGO.SetActive(false);
        leaderboardPanelGO.SetActive(false);
        gameOverPanelGO.SetActive(false);
        leaderboardUIElement.ClearPlayerList();
    }
    
    public void DisableLoginPanel()
    {
        loginPanelGO.SetActive(false);
    }

    public void EnableRetryAndPlayOffline()
    {
        loadingVisual.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        playOfflineButton.gameObject.SetActive(true);
    }
}
