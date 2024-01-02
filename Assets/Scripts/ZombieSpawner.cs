using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{


public GameObject objectToSpawn; // Reference to the prefab you want to spawn
    public int numberOfObjects = 10;  // Number of objects to spawn
    public float spawnRadius = 5f;    // Radius within which objects will be spawned

    public float activationDistance = 10f;//Distance at which the spawner activates

    private Transform player;
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform; 
    }


     void Update() {

        float distanceToPlayer = Vector3.Distance(transform.position,player.position);

        if(distanceToPlayer <= activationDistance)
        {
            SpawnObjects();
        }
        
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Calculate a random position within the spawn radius
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;

            // Instantiate the object at the random position
            GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            //adding type of boss in future updates
        }
        enabled = false;
    }
}
