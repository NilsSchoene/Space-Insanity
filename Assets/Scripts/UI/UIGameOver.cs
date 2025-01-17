using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    private TMP_Text finalScoreText;
    [SerializeField]
    private TMP_Text newHighscoreText;

    public void Initialize(int score,  int highscore)
    {
        finalScoreText.text = $"Final Score: {score}";
        newHighscoreText.gameObject.SetActive(false);

        if(score > highscore)
        {
            newHighscoreText.gameObject.SetActive(true);
        }
    }
}
