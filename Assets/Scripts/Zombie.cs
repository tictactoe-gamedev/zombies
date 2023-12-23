using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePathfinding();
    }

    //Makes Zombie follor the player
    private void UpdatePathfinding()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }
}
