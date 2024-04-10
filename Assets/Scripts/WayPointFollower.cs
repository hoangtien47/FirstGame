using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWayPointIndex = 0;

    public AIPath aiPath;




    // Update is called once per frame
    void Update()
    {

    }
    public void Patrol()
    {

            if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .5f)
            {
                currentWayPointIndex++;
                if (currentWayPointIndex >= waypoints.Length)
                {
                    currentWayPointIndex = 0;
                }

                //timeToWait = waitTime; // Reset the wait time.
            }
        //transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
        aiPath.maxSpeed = 3;    
        aiPath.destination = waypoints[currentWayPointIndex].transform.position;

    }

    public GameObject GetNextWaypoint() { return waypoints[currentWayPointIndex]; }
}
