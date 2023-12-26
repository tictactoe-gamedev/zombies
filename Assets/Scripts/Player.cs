using System;
using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody rb;

    public static Player Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Calculate movement
        Vector3 movement = Vector3.zero;

        if (verticalInput > 0f) //accelerate forward
        {
            movement = transform.forward * acceleration * verticalInput;
        }
        else //reverse
        {
            movement = transform.forward * reverseAcceleration * verticalInput;
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
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
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

