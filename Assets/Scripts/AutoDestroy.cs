using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float life = 0.25f;
    void Start() => Destroy(gameObject, life);
}

