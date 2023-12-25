using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    [SerializeField] float detectionRange = 10f;
    [SerializeField] string playerTag = "Player";
    NavMeshAgent navMeshAgent;

    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePathfinding();
        CheckIfPlayerInRange();
    }

    //Makes Zombie follor the player
    private void UpdatePathfinding()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    void CheckIfPlayerInRange()
    {

        if (Physics.SphereCast(transform.position, detectionRange, transform.forward, out RaycastHit hit))
        {
            Debug.Log(hit.collider.name);
            // Check if the hit object has the specified tag
            if (hit.collider.CompareTag(playerTag))
            {
                Debug.Log("Player in range!");

                animator.SetTrigger("attack");
            }
        }
    }
}
