using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, IDamageable
{

    [SerializeField] float detectionRange = 10f;
    [SerializeField] LayerMask playerTag;

    [SerializeField] int health = 100;
    [SerializeField] private float deathDelay;

    [SerializeField] private GameObject skeletonRoot;

    NavMeshAgent navMeshAgent;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetRagdollState(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePathfinding();
        CheckIfPlayerInRange();

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    //Makes Zombie follor the player
    private void UpdatePathfinding()
    {



    }

    private void SetRagdollState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = !state;
        }

        foreach (var collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Rigidbody>().isKinematic = state;
        GetComponent<Collider>().enabled = !state;
    }

    void CheckIfPlayerInRange()
    {

        if (Physics.SphereCast(transform.position, detectionRange, transform.forward, out RaycastHit hit, detectionRange, playerTag))
        {
            Debug.Log(hit.collider.name);

            animator.SetBool("attack", true);
            transform.LookAt(Player.Instance.transform);
            return;
        }
        else
        {
            animator.SetBool("attack", false);
        }

        navMeshAgent.SetDestination(Player.Instance.transform.position);

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //turn dead zombie into ragdoll
            animator.enabled = false;
            navMeshAgent.enabled = false;
            SetRagdollState(true);
            print("deathdelay: " + deathDelay);
            Destroy(gameObject, deathDelay);
            enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
