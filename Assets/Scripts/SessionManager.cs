using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public float sessionLengthSeconds = 180f; // 3 minutes

    float endTime;
    bool running;

    public float TimeLeft => running
        ? Mathf.Max(0f, endTime - Time.time)
        : 0f;

    void Start()
    {
        StartSession();
    }

    public void StartSession()
    {
        endTime = Time.time + sessionLengthSeconds;
        running = true;
        Debug.Log("SESSION STARTED");
    }

    public void EndSession()
    {
        running = false;
        Debug.Log("SESSION ENDED");
    }
}

