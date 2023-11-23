using UnityEngine;

public class AgentTimer : MonoBehaviour
{
    private float spawnTime;
    private bool isLoggingTime = false;

    private void Start()
    {
        spawnTime = Time.time;
    }

    public void StartLoggingTime()
    {
        isLoggingTime = true;
    }

    public void StopLoggingTime()
    {
        isLoggingTime = false;
    }

    public void LogTimeToReachLine(int agentID, string lineName)
    {
        if (isLoggingTime)
        {
            // Calculate and log the time taken for this agent to reach the Line
            float timeToReachLine = Time.time - spawnTime;
            Debug.Log("Agent " + agentID + " has reached safety in " + timeToReachLine + " seconds.");
            isLoggingTime = false; // Stop logging time to prevent duplicate logs
        }
    }
}