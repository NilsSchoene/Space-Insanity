using System;
using Unity.Services.Leaderboards;
using UnityEngine;

public class UILeaderboard : MonoBehaviour
{
    [SerializeField]
    private UILeaderboardEntry leaderboardEntryUIElement;
    [SerializeField]
    private RectTransform playerEntriesParent;

    private string leaderboardID = "spaceshooter_leaderboard_config";

    public void Initialize()
    {
        LoadPlayers();
    }

    private async void LoadPlayers()
    {
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);
            ClearPlayerList();
            for (int i = 0; i < scores.Results.Count; i++)
            {
                UILeaderboardEntry entry = Instantiate(leaderboardEntryUIElement, playerEntriesParent);
                entry.Initialize(scores.Results[i]);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            UILeaderboardEntry entry = Instantiate(leaderboardEntryUIElement, playerEntriesParent);
            entry.InitializeOffline();
        }
    }

    public void ClearPlayerList()
    {
        UILeaderboardEntry[] entries = GetComponentsInChildren<UILeaderboardEntry>();
        if (entries != null)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                Destroy(entries[i].gameObject);
            }
        }
    }
}
