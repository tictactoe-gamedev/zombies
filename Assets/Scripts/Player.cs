using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f; // Adjust the speed as needed
    [SerializeField] float rotationSpeed = 100f; // Adjust the rotation speed as needed   
    public static Player Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        // Get input from the arrow keys or joystick
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement and rotation
        Vector3 movement = speed * Time.deltaTime * new Vector3(0f, 0f, verticalInput);
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;

        // Move and rotate the car
        transform.Translate(movement);
        transform.Rotate(0f, rotation, 0f);
    }
}
