using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passengers : MonoBehaviour
{
    public Passenger agentPrefab;
    List<Passenger> passengers = new List<Passenger>();
    
    public int numberOfAgents = 10; // Adjustable number of agents

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject frontSeats = GameObject.Find("Seats");

        for (int i = 0; i < numberOfAgents; i++)
        {
            GameObject seat = frontSeats.transform.GetChild(i % frontSeats.transform.childCount).gameObject;
            Vector3 seatpos = seat.transform.position;
            var x = seatpos.x;
            var y = seatpos.y;
            var z = seatpos.z + 2;
            Vector3 newpos = new Vector3(x, y, z);

            Passenger passenger = Instantiate(agentPrefab, newpos, Quaternion.identity);
            passengers.Add(passenger);
        }
    }
}
