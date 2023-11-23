using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Passenger : MonoBehaviour
{
    public NavMeshAgent agent;
    Collider agentCollider;
    bool hasReachedExit = false; // Variable to check if the agent has reached the exit
    public Collider AgentCollider { get { return agentCollider; } }
    public bool HasReachedExit { get { return hasReachedExit; } }

    private List<Transform> lineTransforms = new List<Transform>(); // List of Line objects' transforms

    // Unique ID for each agent
    private static int agentCount = 0;
    private int agentID;

    private AgentTimer agentTimer;

    public float despawnRadius = 1.0f; // Adjust this value to control the area for despawning

    // Start is called before the first frame update
    void Start()
    {
        agentID = agentCount++; // Assign a unique ID to the agent and increment the count
        agentCollider = GetComponent<CapsuleCollider>();
        MoveToExit();

        // Find all Line objects by tag
        GameObject[] lineObjects = GameObject.FindGameObjectsWithTag("Line");

        foreach (var lineObject in lineObjects)
        {
            lineTransforms.Add(lineObject.transform);
        }

        // Create and attach AgentTimer component
        agentTimer = gameObject.GetComponent<AgentTimer>();
        if (agentTimer == null)
        {
            // If the AgentTimer component is not already attached, add it.
            agentTimer = gameObject.AddComponent<AgentTimer>();
        }

        agentTimer.StartLoggingTime();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the agent has reached the exit
        if (!hasReachedExit && !agent.pathPending && IsNearAnyLine())
        {
            hasReachedExit = true;

            // Log the time taken for this agent to reach any Line
            if (agentTimer != null)
            {
                agentTimer.LogTimeToReachLine(agentID, "Any Line");
            }

            Despawn();
        }
    }

    // Despawn the agent
    void Despawn()
    {
        Destroy(gameObject);
    }

    void MoveToExit()
    {
        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        GameObject nearestExit = null;
        float smallestDistance = Mathf.Infinity;

        foreach (var exit in exits)
        {
            float distanceToAgent = Vector3.Distance(agent.transform.position, exit.transform.position);
            if (distanceToAgent < smallestDistance)
            {
                nearestExit = exit;
                smallestDistance = distanceToAgent;
            }
        }

        if (nearestExit != null)
        {
            agent.SetDestination(nearestExit.transform.position);
        }
    }

    bool IsNearAnyLine()
    {
        foreach (var lineTransform in lineTransforms)
        {
            if (Vector3.Distance(transform.position, lineTransform.position) <= despawnRadius)
            {
                return true;
            }
        }
        return false;
    }
}
