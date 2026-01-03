using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public WaveSpawner spawner;
    public PlayerHealth playerHealth;

    private bool ended;

    void Update()
    {
        if (ended) return;
        if (playerHealth == null) return;

        if (playerHealth.isDead)
        {
            ended = true;
            if (spawner != null) spawner.StopGame();
            Debug.Log("GAME OVER (spawner stopped).");
        }
    }

    public void ResetGame()
    {
        ended = false;
        playerHealth?.ResetHealth();
        spawner?.StartGame();
    }
}

