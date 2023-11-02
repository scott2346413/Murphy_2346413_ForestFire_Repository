using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float damageDelay;

    float nextDamage;

    private void Start()
    {
        nextDamage = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        tryDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        tryDamage(other);
    }

    void tryDamage(Collider target)
    {
        PlayerHealth health = target.GetComponent<PlayerHealth>();

        if (health == null)
        {
            return;
        }

        if(Time.time > nextDamage)
        {
            health.doDamage(damage);
            nextDamage += damageDelay;
        }
    }

}
