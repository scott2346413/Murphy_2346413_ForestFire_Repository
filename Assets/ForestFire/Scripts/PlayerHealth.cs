using System.Collections;
using System.Collections.Generic;
using MagicPigGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HorizontalProgressBar healthBar;
    [SerializeField] float maxHealth;
    float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetProgress(1);
    }

    public void doDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetProgress(currentHealth / maxHealth);

        if(currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
