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

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<CapsuleCollider>();
        MoveToExit();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the agent has reached the exit
        if (!hasReachedExit && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            hasReachedExit = true;
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
}