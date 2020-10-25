using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float waitKickTime = 1.3f;

    int currentHealth;
    Animator animator;
    bool takingDamage;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt anim
        animator.SetTrigger("Hurt");
        takingDamage = true;

        // Wait kick animation to end
        WaitForAnimationEnd(waitKickTime);

        if (currentHealth <= 0)
        {
            Die();
        }
}

    private void WaitForAnimationEnd(float waitTime)
    {
        Coroutine timeWaiter = StartCoroutine(WaitForTime(waitTime));
        if (!takingDamage)
        {
            StopCoroutine(timeWaiter);
        }
    }

    private IEnumerator WaitForTime(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        takingDamage = false;
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy
        GetComponent<MoveForward>().enabled = false;
        this.enabled = false;
    }
}
