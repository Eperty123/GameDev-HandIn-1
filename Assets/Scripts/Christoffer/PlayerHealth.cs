using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject HealthUIPrefab;
    public Image healthBar;


    GameObject healthBarUI;

    private void Start()
    {
        if (HealthUIPrefab != null)
            healthBarUI = Instantiate(HealthUIPrefab);

        if (healthBarUI != null)
        {
            var images = healthBarUI.GetComponentsInChildren<Transform>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name.Equals("Health", System.StringComparison.OrdinalIgnoreCase))
                {
                    healthBar = images[i].GetComponent<Image>();
                    break;
                }
            }

        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar!.fillAmount = health / 100f;
        if (health <= 0)
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
