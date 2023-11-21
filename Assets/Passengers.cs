using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passengers : MonoBehaviour
{
    public Passenger agentPrefab;
    List<Passenger> passengers = new List<Passenger>();
    int agentsReachedExitCount = 0; // Variable to keep track of agents that reached the exit

    [Range(1f, 10f)]
    public float neighRadius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for agents that reached the exit and increment the count
        for (int i = passengers.Count - 1; i >= 0; i--)
        {
          
        }
    }

    void Spawn()
    {
        GameObject frontSeats = GameObject.Find("Seats");

        for (int i = 0; i < frontSeats.transform.childCount; i++)
        {
            GameObject seat = frontSeats.transform.GetChild(i).gameObject;
            Vector3 seatpos = seat.transform.position;
            var x = seatpos.x;
            var y = seatpos.y;
            var z = seatpos.z - 33;
            Vector3 newpos = new Vector3(x, y, z);

            Passenger passenger = Instantiate(agentPrefab, newpos, Quaternion.identity);
            passengers.Add(passenger);
        }
    }

    // Note: This method should be adjusted based on your specific requirements
    List<Transform> GetNearbyObjects(Passenger passenger)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextCollider = Physics2D.OverlapCircleAll(passenger.transform.position, neighRadius);
        foreach (Collider2D c in contextCollider)
        {
            if (c == passenger.AgentCollider)
                continue;

            context.Add(c.transform);
        }

        return context;
    }
}
