using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public Image healthBar;

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.fillAmount = health / 100f;
        if(health <= 0)
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
