using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float waitKickTime = 1.12f;

    Rigidbody rigidbody;
    float horizontal;
    float vertical;
    Vector3 movement;
    Animator animator;
    bool isKicking;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        if (!isKicking)
        {
            // Move the player around the scene.
            Move();

            // Turn the player to face the mouse cursor.
            Turning();
        }
    }

    private void Turning()
    {
        if (IsWalking())
        {
            Vector3 lookVector = new Vector3(horizontal, 0f, vertical).normalized;
            lookVector.y = 0f;
            rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookVector), rotationSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * movementSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        if (IsWalking())
        {
            rigidbody.MovePosition(transform.position + movement);
            animator.SetFloat("IsWalking", 2f);
        }
        else
        {
            animator.SetFloat("IsWalking", 1f); ; 
        }
    }

    private void WaitForAnimationEnd(float waitTime)
    {
        Coroutine timeWaiter = StartCoroutine(WaitForTime(waitTime));
        if (!isKicking)
        {
            StopCoroutine(timeWaiter);
        }
    }

    private IEnumerator WaitForTime(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        isKicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // pickup player input movement controles
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Use player attack key
        if (Input.GetMouseButtonDown(0))
        {
            isKicking = true;
            animator.SetTrigger("Kick");
            // Wait kick animation to end
            WaitForAnimationEnd(waitKickTime);
        }
    }

    private bool IsWalking()
    {
        return horizontal != 0f || vertical != 0f;
    }
}
