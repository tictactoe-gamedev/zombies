using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] int health = 100;

    private Rigidbody rb;

    public static Player Instance;
    void Start()
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
        Vector3 movement = transform.forward * speed * verticalInput;

        // Apply forces to Rigidbody
        rb.AddForce(movement);

        // Check if the car is moving forward or backward
        if (rb.velocity.magnitude > 0.1f)
        {
            // Calculate rotation based on speed
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);

            // Apply rotation to Rigidbody
            rb.MoveRotation(rb.rotation * deltaRotation);
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

