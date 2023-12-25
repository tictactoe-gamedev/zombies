using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, IDamageable
{

    [SerializeField] float detectionRange = 10f;
    [SerializeField] string playerTag = "Player";

    [SerializeField] int health = 100;

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

        if(navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if(Physics.SphereCast(transform.position, 0.5f, transform.forward, out RaycastHit hit, 1f))
        {
            if(hit.collider.CompareTag(playerTag))
            {
                transform.LookAt(Player.Instance.transform);
            }

        }
    }

    //Makes Zombie follor the player
    private void UpdatePathfinding()
    {
        if(navMeshAgent.destination != Player.Instance.transform.position)
        {
            navMeshAgent.SetDestination(Player.Instance.transform.position);
        }

    }

    void CheckIfPlayerInRange()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionRange))
        {
            Debug.Log(hit.collider.name);
            // Check if the hit object has the specified tag
            if (hit.collider.CompareTag(playerTag))
            {
                Debug.Log("Player in range!");

                animator.SetBool("attack", true);
            }
            else
            {
                animator.SetBool("attack", false);
            }
        }else
            {
                animator.SetBool("attack", false);
            }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionRange);
    }

}
