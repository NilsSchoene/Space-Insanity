using TMPro;
using UnityEngine;
using Unity.Services.Leaderboards.Models;
using UnityEngine.UI;

public class UILeaderboardEntry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rankText;
    [SerializeField]
    private TMP_Text playerNameText;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Image leaderboardEntryImage;

    private LeaderboardEntry player;

    public void Initialize(LeaderboardEntry player)
    {
        this.player = player;
        //Dictionary<string, string> metadata = JsonConvert.DeserializeObject<Dictionary<string, string>>(player.Metadata);
        rankText.text = (player.Rank + 1).ToString();
        //playerNameText.text = metadata["username"];
        playerNameText.text = player.PlayerName;
        scoreText.text = player.Score.ToString();
        if(player.PlayerName == LogManager.Instance.playerName)
        {
            Highlight();
        }
    }

    public void InitializeOffline()
    {
        rankText.text = "";
        playerNameText.text = "Currently not available";
        scoreText.text = "";
    }

    private void Highlight()
    {
        var color = leaderboardEntryImage.color;
        color.r = 1.0f;
        color.g = 1.0f;
        color.b = 1.0f;
        leaderboardEntryImage.color = color;
    }
}
