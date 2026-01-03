using UnityEngine;
using UnityEngine.InputSystem;

public class GunRayShooter : MonoBehaviour
{
    public Transform muzzle;
    public InputActionProperty fireAction;

    public float range = 50f;
    public float damage = 25f;

    [Header("Trigger Settings")]
    public float triggerThreshold = 0.7f; // pull past this
    public float fireCooldown = 0.15f;

    private float nextFireTime;
    private bool wasDownLastFrame;
    
    [Header("Visual Bullet")]
    public GameObject bulletPrefab;
    public float bulletLifeTime = 1.5f; // optional if you want to override
    
    [Header("Hit Effect")]
    public GameObject hitEffectPrefab;
    
	[Header("Raycast")]
	public LayerMask hitMask;

    void OnEnable() => fireAction.action.Enable();
    void OnDisable() => fireAction.action.Disable();

    void Update()
    {
        if (fireAction.action == null) return;

        // Read trigger as a float (works even if action is axis/value)
        float v = 0f;
        try { v = fireAction.action.ReadValue<float>(); }
        catch { /* if it's really a button action, we'll handle below */ }

        bool isDown = v >= triggerThreshold;

        // If it’s actually a Button action, use IsPressed instead
        if (fireAction.action.type == InputActionType.Button)
            isDown = fireAction.action.IsPressed();

        // Rising edge: only fire when it goes from not-down -> down
        if (!wasDownLastFrame && isDown && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            Shoot();
        }

        wasDownLastFrame = isDown;
    }

    void Shoot()
    {
        var origin = muzzle ? muzzle.position : transform.position;
        var dir = muzzle ? muzzle.forward : transform.forward;

        Debug.DrawRay(origin, dir * range, Color.green, 1f);
        Debug.Log("SHOT!");
        
       GameObject bulletInstance = null;

	if (bulletPrefab != null && muzzle != null)
	{
	    bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
	}

	if (Physics.Raycast(origin, dir, out RaycastHit hit, range, hitMask))
	{
	    if (bulletInstance != null)
		Destroy(bulletInstance);

	    var hp = hit.collider.GetComponentInParent<Health>();
	    if (hp != null) hp.TakeDamage(damage);

	    if (hitEffectPrefab != null)
		Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
	}
    }
}

