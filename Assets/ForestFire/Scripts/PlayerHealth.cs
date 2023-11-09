using System.Collections;
using System.Collections.Generic;
using MagicPigGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HorizontalProgressBar healthBar; // The health progress bar
    [SerializeField] float maxHealth; // Player's maximum health
    float currentHealth; // Player's current health


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // set the current health to maximum
        healthBar.SetProgress(1); // progress bar set to full
    }

    //Do Damage deals damage to the player and does any relevant processing
    public void doDamage(float amount)
    {
        currentHealth -= amount; // reduce current health by a set amount
        healthBar.SetProgress(currentHealth / maxHealth); // update the health progress bar

        if(currentHealth <= 0) // if current health reaches 0, player dies
        {
            die();
        }
    }

    // Die loads the respawn scene
    void die()
    {
        SceneManager.LoadScene("ForestFireRespawn"); // load the respawn scene where player can then choose to respawn into forest fire
    }
}
