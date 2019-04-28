using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherController : MonoBehaviour
{
    public NavMeshAgent agent;

    //All children of this transform are waypoints.
    public Transform waypointParent;

    public Transform SightOrigin;

    public float Speed = 1.8f;
    public float RunMultiplier = 1.5f;

    Transform[] waypoints;
    float SightRange = 18f;

    int lastWaypoint = -1;
    int curWaypoint = -1;

    Animator anim;

    enum State
    {
        Patrolling,
        Chasing
    }

    State currentState;

    Transform playerTransform;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
        //Set speed
        agent.speed = Speed;
        //Animation speed
        anim.speed = 1f;

        //Check if we can see player
        if (CanSeePlayer())
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
        print("CanSeePlayer: " + CanSeePlayer().ToString());

        //Set speed
        agent.speed = Speed * RunMultiplier;
        //Animation speed
        anim.speed = RunMultiplier;

        if (CanSeePlayer())
        {
            //Mark new player position
            agent.SetDestination(playerTransform.position);

            float dist = Vector3.Distance(transform.position, playerTransform.position);
            //Did we reach player?
            if (dist < 2f)
            {
                GameInitializer.Instance.LoadGameOver();
            }
        }
        else
        {
            //We can't see player, start patrolling
            currentState = State.Patrolling;
            //Set target
            agent.SetDestination(waypoints[curWaypoint].position);

            return;
        }
    }

    float sightCooldown = 0f;

    bool CanSeePlayer()
    {
        //Ignore y
        Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 ppos = new Vector3(playerTransform.position.x, 0f, playerTransform.position.z);
        //Is player close enough?
        float dist = Vector3.Distance(pos, ppos);
        if (dist > SightRange)
            return false;

        //Do we have direct line of sight?
        Vector3 dir = (playerTransform.position + Vector3.up * 2f) - SightOrigin.position;
        RaycastHit[] hits = Physics.RaycastAll(SightOrigin.position, dir, dist);
       
        for (int i = 0; i < hits.Length; i++)
        {
            print("Hit " + hits[i].collider.name);
            //Did we hit anything besides ourselves and player?
            if (hits[i].transform != playerTransform && hits[i].transform != transform)
            {
                //Then sight is blocked.
                sightCooldown = 0f;
                return false;
            }

        }

        //We have seen player for over one second
        if (sightCooldown > 1f)
        {
            return true;
        }
        sightCooldown += Time.deltaTime;

        return false;
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
