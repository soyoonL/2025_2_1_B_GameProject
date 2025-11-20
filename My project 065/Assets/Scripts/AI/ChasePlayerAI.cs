using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerAI : MonoBehaviour
{
    public Transform player;                
    public float chaseRange = 50.01f;
    public float attackRange = 2.01f;

    private NavMeshAgent agent;             
    private float distanceToPlayer;         

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

  
void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    
void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        Debug.Log("Attacking player!");
    }
}
