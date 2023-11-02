using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SlimeLauncher : MonoBehaviour
{
    [SerializeField] Transform launchPoint;
    [SerializeField] GameObject slimeball;
    [SerializeField] GameObject slimeArea;
    [SerializeField] float timeBetweenAttacks;

    [SerializeField] float slimeFlyTime;

    float nextAttack;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<XROrigin>().transform;
        nextAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>nextAttack)
        {
            throwSlime();
        }
    }

    void throwSlime()
    {
        GameObject thisSlimeball = Instantiate(slimeball, launchPoint);
        Rigidbody slimeRigidbody = thisSlimeball.GetComponent<Rigidbody>();

        if(slimeRigidbody == null)
        {
            return;
        }

        slimeRigidbody.velocity = calculateLaunchVelocity();

        nextAttack = Time.time + timeBetweenAttacks;
    }

    Vector3 calculateLaunchVelocity()
    {
        Vector3 vectorToPlayer = player.position - launchPoint.position;
        vectorToPlayer.y = 0;
        float horizontalDistanceToPlayer = vectorToPlayer.magnitude;

        float horizontalVelocity = horizontalDistanceToPlayer / slimeFlyTime;
        float verticalVelocity = (horizontalDistanceToPlayer * 9.8f) / (2f * horizontalVelocity);

        Vector3 velocity = Vector3.zero;
        velocity.y = verticalVelocity;
        velocity += vectorToPlayer.normalized * horizontalVelocity;

        return velocity;
    }
}
