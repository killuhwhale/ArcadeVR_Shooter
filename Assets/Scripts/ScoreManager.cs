using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score { get; private set; }

    public void ResetScore()
    {
        score = 0;
        Debug.Log("SCORE RESET");
    }

    public void Add(int amount)
    {
        score += amount;
        Debug.Log($"SCORE: {score}");
    }
}

