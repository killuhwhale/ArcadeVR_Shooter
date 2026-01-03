using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform target;
    public float speed = 1.2f;
    public float stopDistance = 1.2f;

    void Update()
    {
        if (target == null) return;

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f; // stay on ground

        float dist = toTarget.magnitude;
        if (dist <= stopDistance) return;

        Vector3 dir = toTarget.normalized;
        transform.position += dir * speed * Time.deltaTime;

        // Face the player (optional)
        if (dir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }
}

