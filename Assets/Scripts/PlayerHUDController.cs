using TMPro;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text hpText;
    public TMP_Text timeText;

    public ScoreManager score;
    public PlayerHealth player;
    public SessionManager session; // optional

    void Update()
    {
        if (scoreText != null && score != null)
            scoreText.text = $"SCORE: {score.score}";

        if (hpText != null && player != null)
            hpText.text = $"HP: {player.hp:0}";

        if (timeText != null)
        {
            if (session != null)
                timeText.text = $"TIME: {session.TimeLeft:0}";
            else
                timeText.text = ""; // hide if no session
        }
    }
}

