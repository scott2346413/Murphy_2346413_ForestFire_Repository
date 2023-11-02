using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SlimeLauncher : MonoBehaviour
{
    [SerializeField] Transform launchPoint;
    [SerializeField] GameObject slimeBall;
    [SerializeField] GameObject slimeArea;
    [SerializeField] float timeBetweenAttacks;

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
        Instantiate(slimeBall, launchPoint);

    }
}
