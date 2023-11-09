using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] float damagePerSecond;

    [SerializeField] GameObject damageSound;

    float nextDamage;

    private void Start()
    {
        nextDamage = Time.time;
        damageSound.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        tryDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        tryDamage(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            damageSound.SetActive(false);
        }
    }

    void tryDamage(Collider target)
    {
        PlayerHealth health = target.GetComponent<PlayerHealth>();

        if (health == null)
        {
            return;
        }

        health.doDamage(damagePerSecond * Time.deltaTime);
        damageSound.SetActive(true);
    }

}
