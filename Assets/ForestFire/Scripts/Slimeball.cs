using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimeball : MonoBehaviour
{
    [SerializeField] GameObject slimeArea; // the slime area to be instantiated on ground

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0) // if our position reaches the ground...
        {
            Vector3 spawnPosition = transform.position; // set spawn position of slime area
            spawnPosition.y = 0.1f; // y level should be 0.1
            Instantiate(slimeArea, spawnPosition, Quaternion.identity); // instantiate slime area
            Destroy(gameObject); // destroy the slime ball
        }
    }
}
