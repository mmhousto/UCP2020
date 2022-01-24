using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public enum State { Patroling, Chasing, Idle }

    public State state;

    public Transform target;

    public NavMeshAgent ai;

    public GameObject[] waypoints;

    public Animator anim;

    [SerializeField]
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Patroling;
        ai = GetComponent<NavMeshAgent>();
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Patroling:
                anim.CrossFade("Patroling", 0);
                ai.isStopped = false;
                ai.SetDestination(waypoints[currentWaypoint].transform.position);
                break;
            case State.Chasing:
                anim.CrossFade("Chase", 0);
                ai.SetDestination(target.position);
                break;
            case State.Idle:
                anim.CrossFade("Idle", 0);
                ai.isStopped = true;
                break;
            default:
                break;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = State.Chasing;
        }

        if (other.CompareTag("Waypoint") && state == State.Patroling && other.gameObject.Equals(waypoints[currentWaypoint]))
        {
            other.gameObject.SetActive(false);
            if (currentWaypoint < waypoints.Length-1)
            {
                currentWaypoint++;
            }
            else
            {
                currentWaypoint = 0;
                
            }
            
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = State.Patroling;
        }

        if (other.CompareTag("Waypoint") && state == State.Patroling)
        {
            if(currentWaypoint == 0)
            {
                foreach (GameObject gO in waypoints)
                {
                    gO.SetActive(true);
                }
            }
        }

    }


}
