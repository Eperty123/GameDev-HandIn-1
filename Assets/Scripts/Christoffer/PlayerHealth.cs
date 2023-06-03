using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
            Destroy(gameObject);
    }
}
