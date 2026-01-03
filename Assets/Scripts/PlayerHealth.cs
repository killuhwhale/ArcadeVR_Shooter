using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100f;
    public float hp { get; private set; }

    public bool isDead => hp <= 0f;

    void Awake()
    {
        hp = maxHP;
    }

    public void ResetHealth()
    {
        hp = maxHP;
        Debug.Log($"PLAYER HP RESET: {hp}");
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        hp -= amount;
        Debug.Log($"PLAYER HIT! HP: {hp:0}");

        if (hp <= 0f)
        {
            hp = 0f;
            Debug.Log("PLAYER DEAD!");
        }
    }
}

