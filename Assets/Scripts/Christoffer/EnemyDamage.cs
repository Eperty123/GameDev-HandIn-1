using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 5;
    
    Animator animator;
    float interval = 1.5f; 
    float nextUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Time.time >= nextUpdate) {
            nextUpdate += interval; 
            if(animator.GetBool("isAttacking") == true)
            {
                playerHealth.TakeDamage(damage);
            }
         }
        
    }   
}
