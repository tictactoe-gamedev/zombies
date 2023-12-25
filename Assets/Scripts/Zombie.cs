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
        //CheckIfPlayerInRange();

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

                animator.SetBool("attack", true);
                transform.LookAt(Player.Instance.transform);

            }
            else
            {
                animator.SetBool("attack", false);
            }
        }
        else
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
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
