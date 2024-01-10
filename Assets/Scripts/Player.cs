using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float acceleration = 5f;
    [SerializeField] float reverseAcceleration = 5f;
    public float TopSpeed;
    public float CurrentSpeed { get; private set; }
    [SerializeField] float topReverseSpeed = 5f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] int health = 100;
    [SerializeField] private float damageMultiplier; //car driving into zombie at speed does around 1500 damage with a multiplier of 1
    public float fuelPercentage { get; private set; } = 1f; // 1 = 100% fuel
    [SerializeField] private float fuelUsage; //how much fuel the car uses whenthe player accelerates
    private Controls controls;
    private Vector2 moveInput;

    private Rigidbody rb;

    public static Player Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        controls = new Controls();
        controls.InGame.Enable();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (fuelPercentage < 0f) //player ran out of fuel -> GAME OVER
        {
            Debug.Log("OUT OF FUEL! GAME OVER!!!");
            return;
        }
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        moveInput = controls.InGame.Movement.ReadValue<Vector2>();
        print(controls.InGame.Movement.ReadValue<Vector2>());

        // Calculate movement
        Vector3 movement = Vector3.zero;

        if (moveInput.y > 0f) //accelerate forward
        {
            movement = acceleration * moveInput.y * transform.forward;
            fuelPercentage -= fuelUsage * Time.deltaTime;
        }
        else //reverse
        {
            movement = reverseAcceleration * moveInput.y * transform.forward;
            fuelPercentage -= fuelUsage * Time.deltaTime;
        }

        // Apply forces to Rigidbody
        if (rb.velocity.magnitude < TopSpeed)
        {
            rb.AddForce(movement);
        }

        // Check if the car is moving forward or backward
        if (rb.velocity.magnitude > 0.1f)
        {
            // Calculate rotation based on speed
            float rotation = moveInput.x * rotationSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);

            // Apply rotation to Rigidbody
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        CurrentSpeed = rb.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Based on the documentation, to get the force applied you would just divide this value by the last frame's Time.fixedDeltaTime (since in physics, impulse = force * time):
        Vector3 collisionForce = other.impulse / Time.deltaTime;
        float collisionDamage = collisionForce.magnitude * damageMultiplier;

        //if player hits zombie -> damage zombie
        if (other.gameObject.TryGetComponent(out Zombie zombie) &&
            GetComponent<Rigidbody>().velocity.magnitude > zombie.GetComponent<Rigidbody>().velocity.magnitude) //check if player is running into zombie and not the other way around so that zombie doesnt damage itself
        {
            zombie.TakeDamage((int)collisionDamage);
        }
        else //if player hits a wall or something -> damage player
        {

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
}

