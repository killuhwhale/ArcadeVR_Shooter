using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackRange = 1.2f;
    public float damage = 10f;
    public float attackCooldown = 1.0f;

    [Header("Target")]
    public Transform target; // usually XR Main Camera

    private float nextAttackTime;
 
    void Update()
    {
        if (target == null) return;
        if (Time.time < nextAttackTime) return;

        // Measure distance in XZ (ignore height)
        Vector3 a = transform.position;
        Vector3 b = target.position;
        a.y = 0f;
        b.y = 0f;

        float dist = Vector3.Distance(a, b);
        if (dist > attackRange) return;

        // Find player health (on XR Origin or anywhere in scene)
        var playerHealth = FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null && !playerHealth.isDead)
        {
            playerHealth.TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}

