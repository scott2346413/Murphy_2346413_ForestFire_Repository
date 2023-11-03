using System.Collections;
using System.Collections.Generic;
using MagicPigGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HorizontalProgressBar healthBar;
    [SerializeField] float maxHealth;
    [SerializeField] GameObject damageVolume;
    float currentHealth;

    bool tookDamage = false;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetProgress(1);
    }

    private void Update()
    {
        if (tookDamage)
        {
            damageVolume.SetActive(true);
            tookDamage = false;
        }
        else
        {
            damageVolume.SetActive(false);
        }
    }

    public void doDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetProgress(currentHealth / maxHealth);
        tookDamage = true;

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
