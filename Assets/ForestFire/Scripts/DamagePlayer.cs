using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] float damagePerSecond; // how much damage to deal each second
    [SerializeField] GameObject damageSound; // object which plays damaging sound

    private void Start()
    {
        damageSound.SetActive(false); // set damage sound object to false
    }

    //When anything enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        tryDamage(other); // try to damage
    }

    // When anything stays in the trigger
    private void OnTriggerStay(Collider other)
    {
        tryDamage(other); // try to damage
    }

    // When anything leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null) // if this is the player, deactivate the sound
        {
            damageSound.SetActive(false);
        }
    }

    // Try Damage tries to damage the target collider, checking if it is the player
    void tryDamage(Collider target)
    {
        PlayerHealth health = target.GetComponent<PlayerHealth>(); // try to get the player health component

        if (health == null) // if health is null, return (not player)
        {
            return;
        }

        health.doDamage(damagePerSecond * Time.deltaTime); // Do damage, scaled by deltaTime
        damageSound.SetActive(true); // set damaging sound to active
    }

}
