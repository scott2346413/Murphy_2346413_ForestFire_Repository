using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SlimeLauncher : MonoBehaviour
{
    [SerializeField] float minStartDelay; // the minimum amount of time to wait before we start launching
    [SerializeField] float maxStartDelay; // the maximum amount of time to wait before we start launching

    [SerializeField] Transform launchPoint; // the point that slime balls are laaunched/instantiated from
    [SerializeField] GameObject slimeball; // the prefab for a slime ball to be thrown

    [SerializeField] float maxTimeBetweenAttacks; // the maximum amount of time to wait between attacks
    [SerializeField] float minTimeBetweenAttacks; // the minimum amount of time to wait between attacks
    [SerializeField] float slimeFlyTime; // the amount of time the slime ball should be airborn before landing at player

    [SerializeField] float attackRange; // the range the player must be within to be targetted

    float nextAttack; // when is the next attack happening
    Transform player; // the player's position
    Vector3 currentTarget; // where are we currently targetting

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<XROrigin>().transform; // find the player's transform
        nextAttack = Time.time + Random.Range(minStartDelay, maxStartDelay); // set first attack's time after a random waiting period
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>nextAttack) // if the next scheduled attack time has passed, throw a slime
        {
            throwSlime();
        }
    }

    // Throw Slime calculates the initial velocity the slime ball should have, instantiates it, and sends it flying
    void throwSlime()
    {
        if(Vector3.Distance(player.position, launchPoint.position) < attackRange) // if the player is withing range, make it our target
        {
            currentTarget = player.position;
        }
        else // if player out of range, set a random target
        {
            currentTarget = new Vector3(Random.Range(1, attackRange), 0, Random.Range(1, attackRange)) + launchPoint.position;
        }

        GameObject thisSlimeball = Instantiate(slimeball, launchPoint.position, Quaternion.identity); // instantiate the slime ball at the launch point
        Rigidbody slimeRigidbody = thisSlimeball.GetComponent<Rigidbody>(); // get the slime's rigidbody

        if(slimeRigidbody == null) // if the slime doesn't have a rigidbody, return
        {
            return;
        }

        slimeRigidbody.velocity = calculateLaunchVelocity(); // set slime velocity to a calculated initial velocity

        nextAttack = Time.time + Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks); // set next attack to happen in random amount of time

    }

    // Calculate Launch Velocity uses the target distance, fly time
    // and gravity acceleration to determine what the slime ball's initial velocity should be
    Vector3 calculateLaunchVelocity()
    {
        Vector3 vectorToTarget = currentTarget - launchPoint.position; // find vector between launch and target
        vectorToTarget.y = 0; // ignore the y  value 
        float horizontalDistanceToTarget = vectorToTarget.magnitude; // get horizontal distance between launch and target

        float horizontalVelocity = horizontalDistanceToTarget / slimeFlyTime; // the horizontal velocity based on distance and fly time
        float verticalVelocity = -launchPoint.position.y / slimeFlyTime + 9.8f * slimeFlyTime / 2; // vertial velocity is calculated using other variables

        Vector3 velocity = Vector3.zero; // create a velocity vector
        velocity.y = verticalVelocity; // set y velocity
        velocity += vectorToTarget.normalized * horizontalVelocity; // use direction of target to add horizontal velocity

        return velocity; // return our calculated velocity
    }
}
