using UnityEngine;

public class BulletVisual : MonoBehaviour
{
    public float speed = 35f;
    public float lifeTime = 1.5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

