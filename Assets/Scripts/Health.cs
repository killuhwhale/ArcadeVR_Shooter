using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP = 100f;
    public int pointsOnDeath = 10;

    float hp;
    bool dead;

    void Awake() => hp = maxHP;

    public void TakeDamage(float amount)
    {
        if (dead) return;

        hp -= amount;
        if (hp <= 0f)
            Die();
    }

    void Die()
    {
        dead = true;

        // Give points
        var sm = FindFirstObjectByType<ScoreManager>();
        if (sm != null) sm.Add(pointsOnDeath);

        Destroy(gameObject);
    }
}

