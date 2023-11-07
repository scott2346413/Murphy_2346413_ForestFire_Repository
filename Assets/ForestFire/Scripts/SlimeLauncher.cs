using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SlimeLauncher : MonoBehaviour
{
    [SerializeField] float minStartDelay;
    [SerializeField] float maxStartDelay;

    [SerializeField] Transform launchPoint;
    [SerializeField] GameObject slimeball;

    [SerializeField] float maxTimeBetweenAttacks;
    [SerializeField] float minTimeBetweenAttacks;
    [SerializeField] float slimeFlyTime;

    [SerializeField] float attackRange;

    float nextAttack;
    Transform player;
    Vector3 currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<XROrigin>().transform;
        nextAttack = Time.time + Random.Range(minStartDelay, maxStartDelay);
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
        if(Vector3.Distance(player.position, launchPoint.position) < attackRange)
        {
            currentTarget = player.position;
        }
        else
        {
            currentTarget = new Vector3(Random.Range(1, attackRange), 0, Random.Range(1, attackRange)) + launchPoint.position;
        }

        GameObject thisSlimeball = Instantiate(slimeball, launchPoint.position, Quaternion.identity);
        Rigidbody slimeRigidbody = thisSlimeball.GetComponent<Rigidbody>();

        if(slimeRigidbody == null)
        {
            return;
        }

        slimeRigidbody.velocity = calculateLaunchVelocity();

        nextAttack = Time.time + Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);

    }

    Vector3 calculateLaunchVelocity()
    {
        Vector3 vectorToTarget = currentTarget - launchPoint.position;
        vectorToTarget.y = 0;
        float horizontalDistanceToTarget = vectorToTarget.magnitude;

        float horizontalVelocity = horizontalDistanceToTarget / slimeFlyTime;
        float verticalVelocity = -launchPoint.position.y / slimeFlyTime + 9.8f * slimeFlyTime / 2;
        //float verticalVelocity = (horizontalDistanceToTarget * 9.8f) / (2f * horizontalVelocity);

        Vector3 velocity = Vector3.zero;
        velocity.y = verticalVelocity;
        velocity += vectorToTarget.normalized * horizontalVelocity;

        return velocity;
    }
}
