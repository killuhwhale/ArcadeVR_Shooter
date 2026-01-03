using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Enemy prefab to spawn (must have EnemyMover somewhere on it).")]
    public GameObject enemyPrefab;

    [Tooltip("Spawn point transforms in the scene.")]
    public Transform[] spawnPoints;

    [Tooltip("What enemies chase (usually XR rig Main Camera transform). If left blank, will auto-find Camera.main.")]
    public Transform playerTarget;

    [Header("Wave Settings")]
    public int startCount = 3;
    public int addPerWave = 2;
    public float timeBetweenSpawns = 0.6f;
    public float timeBetweenWaves = 2.5f;

    [Header("Auto Start")]
    public bool autoStart = true;

    private int wave = 0;
    private int alive = 0;
    private Coroutine loopRoutine;

    void Awake()
    {
        // Auto-find player target if not assigned in Inspector.
        // IMPORTANT: Your XR camera must be tagged "MainCamera" for this to work.
        if (playerTarget == null)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                playerTarget = cam.transform;
                Debug.Log($"WaveSpawner: Auto-assigned playerTarget to Camera.main ({playerTarget.name}).");
            }
            else
            {
                Debug.LogWarning("WaveSpawner: playerTarget is null and Camera.main not found. " +
                                 "Tag your XR camera as MainCamera or assign playerTarget in Inspector.");
            }
        }
    }

    void Start()
    {
        if (autoStart) StartGame();
    }

    public void StartGame()
    {
        // Stop any existing loop cleanly
        if (loopRoutine != null) StopCoroutine(loopRoutine);

        wave = 0;
        alive = 0;
        loopRoutine = StartCoroutine(WaveLoop());
    }

    public void StopGame()
    {
        if (loopRoutine != null) StopCoroutine(loopRoutine);
        loopRoutine = null;
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            wave++;
            int count = startCount + (wave - 1) * addPerWave;

            Debug.Log($"--- Wave {wave} | Spawning {count} ---");

            yield return new WaitForSeconds(timeBetweenWaves);

            for (int i = 0; i < count; i++)
            {
                SpawnOne();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            // Wait until all enemies are dead
            while (alive > 0)
                yield return null;
        }
    }

    void SpawnOne()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("WaveSpawner: enemyPrefab is NOT assigned.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("WaveSpawner: spawnPoints are NOT assigned.");
            return;
        }

        if (playerTarget == null)
        {
            Debug.LogError("WaveSpawner: playerTarget is NOT assigned. Set it to XR Main Camera (or tag XR camera as MainCamera).");
            return;
        }

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject e = Instantiate(enemyPrefab, sp.position, sp.rotation);
        alive++;

        // Assign chase target (EnemyMover can be on root or child)
       var mover = e.GetComponentInChildren<EnemyMover>(true);
	if (mover != null) mover.target = playerTarget;

	var attacker = e.GetComponentInChildren<EnemyAttack>(true);
	if (attacker != null) attacker.target = playerTarget;

        // Track alive count when destroyed
        var notify = e.AddComponent<OnDestroyNotify>();
        notify.onDestroyed += () => alive--;
    }
}

public class OnDestroyNotify : MonoBehaviour
{
    public System.Action onDestroyed;
    void OnDestroy() => onDestroyed?.Invoke();
}

