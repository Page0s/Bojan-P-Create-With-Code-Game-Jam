using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float waitKickTime = 1.12f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float attackRate = 2f;
    [SerializeField] int attackDamage = 40;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Transform attackPoint;

    Rigidbody rigidbody;
    float horizontal;
    float vertical;
    float nextAttackTime = 0f;
    Vector3 movement;
    Animator animator;
    bool isKicking;
    bool canAttack;

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

    // Update is called once per frame
    void Update()
    {
        // pickup player input movement controles
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Add attack rate
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Space))
        {
            //canAttack = true;
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void Attack()
    {
        isKicking = true;
        canAttack = false;
        animator.SetTrigger("Kick");

        // Detect all kicked enemys
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage enemys
        foreach (Collider enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
        }

        // Wait kick animation to end
        WaitForAnimationEnd(waitKickTime);

        // Use player attack key
        if (Input.GetMouseButtonDown(0))
        {
            //
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

        // Move the player to it's current position plus the movement.
        if (IsWalking() && !IsRunning())
        {
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * movementSpeed * Time.deltaTime;

            rigidbody.MovePosition(transform.position + movement);
            animator.SetFloat("IsWalking", 3f);
        }
        else if (IsWalking() && IsRunning())
        {
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * (movementSpeed * 2) * Time.deltaTime;
            rigidbody.MovePosition(transform.position + movement);
            animator.SetFloat("IsWalking", 5f);
        }
        else
        {
            animator.SetFloat("IsWalking", 1f); ; 
        }
    }

    private bool IsRunning()
    {
        return IsWalking() && Input.GetKey(KeyCode.LeftShift);
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

    private bool IsWalking()
    {
        return horizontal != 0f || vertical != 0f;
    }
}
