using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform[] waypoints;

    float SightRange = 10f;

    int lastWaypoint = -1;
    int curWaypoint = -1;

    enum State
    {
        Patrolling,
        Chasing
    }

    State currentState;

    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

        GetNewWaypoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrolling();
                break;
            case State.Chasing:
                Chasing();
                break;
        }
    }

    void Patrolling()
    {
        //Check if we can see player
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist < SightRange)
        {
            //Switch to chase player
            currentState = State.Chasing;
            return;
        }


        Vector3 target = waypoints[curWaypoint].position;

        if (Vector3.Distance(transform.position, target) < 2f)
        {
            //print("Reached point " + curWaypoint.ToString());
            GetNewWaypoint();
        }
    }

    void GetNewWaypoint()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (i != curWaypoint && i != lastWaypoint)
                list.Add(i);
        }
        lastWaypoint = curWaypoint;
        //print("LastWaypoint: " + curWaypoint);
        curWaypoint = list[Random.Range(0, list.Count)];
        //print("NextWaypoint: " + curWaypoint);

        agent.SetDestination(waypoints[curWaypoint].position);
    }

    Vector3 lastSpotted = Vector3.zero;

    void Chasing()
    {
        if (lastSpotted == Vector3.zero)
        {
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            if (dist < SightRange)
            {
                //Mark new player position
                lastSpotted = playerTransform.position;
                agent.SetDestination(lastSpotted);
            }
            else
            {
                //We cant see player, start patrolling
                currentState = State.Patrolling;
            }
            return;
        }

        float d = Vector3.Distance(transform.position, playerTransform.position);
        //Did we reach player?
        if (d < 2f)
        {
            GameInitializer.Instance.LoadGameOver();
        }

        d = Vector3.Distance(transform.position, lastSpotted);
        //Did we reach target point?
        if (d < 2f)
        {
            //Get new point
            lastSpotted = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentState == State.Chasing)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(lastSpotted, 1f);
        }
        else if (curWaypoint != -1)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(waypoints[curWaypoint].position, 1f);
        }
    }
}
