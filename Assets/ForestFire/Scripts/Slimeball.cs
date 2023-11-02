using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimeball : MonoBehaviour
{
    [SerializeField] GameObject slimeArea;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y = 0.1f;
            Instantiate(slimeArea, spawnPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
