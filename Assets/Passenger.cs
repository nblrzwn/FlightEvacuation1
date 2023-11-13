using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Passenger : MonoBehaviour
{
    public NavMeshAgent agent;
    Collider agentCollider;
    public Collider AgentCollider {get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
         agentCollider = GetComponent<CapsuleCollider>();
         MoveToExit();
         
         
    }

    public bool CollideSomething() {
        float rad = GetComponent<CapsuleCollider>().radius;
        Collider[] contextCollider = Physics.OverlapSphere(transform.position, rad);
        if (contextCollider.Length > 1)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
    //    if (Input.GetMouseButtonDown(1)) {
    //     Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(movePosition, out var hitInfo)) {
    //         agent.SetDestination(hitInfo.point);            
    //     }

    //    }
        
        

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

        if(nearestExit != null)
        {
            agent.SetDestination(nearestExit.transform.position);
        }
    }
    

    
}
