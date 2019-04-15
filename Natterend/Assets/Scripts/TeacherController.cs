using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherController : MonoBehaviour
{
    public NavMeshAgent agent;

    //All children of this transform are waypoints.
    public Transform waypointParent;

    Transform[] waypoints;
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

        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

        GetNewWaypoint();
        agent.SetDestination(waypoints[curWaypoint].position);
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
            agent.SetDestination(waypoints[curWaypoint].position);
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
        curWaypoint = list[Random.Range(0, list.Count)];
    }

    void Chasing()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist < SightRange)
        {
            //Mark new player position
            agent.SetDestination(playerTransform.position);

            //Did we reach player?
            if (dist < 2f)
            {
                GameInitializer.Instance.LoadGameOver();
            }
        }
        else
        {
            //We cant see player, start patrolling
            currentState = State.Patrolling;
            //Set target
            agent.SetDestination(waypoints[curWaypoint].position);

            return;
        }
    }

    private void OnDrawGizmos()
    {
        if (curWaypoint != -1)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(waypoints[curWaypoint].position, 1f);
        }
    }
}
