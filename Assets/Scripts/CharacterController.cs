using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    Rigidbody rigidbody;
    float horizontal;
    float vertical;
    Vector3 movement;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move();
    }

    private void Move()
    {
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * movementSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rigidbody.MovePosition(transform.position + movement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
