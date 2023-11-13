using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passengers : MonoBehaviour
{
    public Passenger agentPrefab;
    List<Passenger> passengers = new List<Passenger>();

    

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
        // if (Input.GetMouseButtonDown(0)) 
        // {
        //     for (int i=0; i<3; i++) { // try 3 times
        //         Passenger agent = passengers[Random.Range(0, passengers.Count)];
        //         if (agent.IsInQueue() == false) {
        //             agent.MoveToQueue();
        //             break;
        //         }
        //     }
        // }

        
    }

    void Spawn()
    {
        
        GameObject frontSeats = GameObject.Find("Seats");

        for(int i = 0; i < frontSeats.transform.childCount; i++)
        {
            GameObject seat = frontSeats.transform.GetChild(i).gameObject;
            Vector3 seatpos = seat.transform.position;
            var x = seatpos.x;
            var y = seatpos.y;
            var z = seatpos.z - 33;
            Vector3 newpos = new Vector3(x, y, z);

            Passenger passenger = Instantiate(agentPrefab, newpos, Quaternion.identity);

            // if(passenger.CollideSomething())
            // {
            //     continue;
            // }

            passengers.Add(passenger);

        };

    }

    List<Transform> GetNearbyObjects(Passenger passenger) {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextCollider = Physics2D.OverlapCircleAll(passenger.transform.position, neighRadius);
        foreach (Collider2D c in contextCollider) {
            if (c == passenger.AgentCollider) 
                continue;
            
            context.Add(c.transform);
        }

        return context;
    }
}
