using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{

    public Transform[] waypoints;

    private EnemyAi enemy;

    private int currentState;

    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
        enemy = GetComponent<EnemyAi>();
        currentState = (int)enemy.state;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case 0:
                enemy.ai.SetDestination(waypoints[currentWaypoint].position);
                if(enemy.ai.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    if(currentWaypoint < waypoints.Length-1)
                    {
                        currentWaypoint++;
                        enemy.ai.SetDestination(waypoints[currentWaypoint].position);
                    }
                    else
                    {
                        currentWaypoint = 0;
                        enemy.ai.SetDestination(waypoints[currentWaypoint].position);
                    }
                    
                }
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
