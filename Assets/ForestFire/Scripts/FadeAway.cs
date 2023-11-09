using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    [SerializeField] float fadeDelay; // how long before object starts fading
    [SerializeField] float fadeTime; // how long it takes to fade

    float startFadingTime; // when is object going to start fading
    float destroyTime; // when should we destroy the object

    MeshRenderer _renderer; // the object's renderer

    // Start is called before the first frame update
    void Start()
    {
        startFadingTime = Time.time + fadeDelay; // set when to start fading
        destroyTime = Time.time + fadeDelay + fadeTime; // set when to destroy

        _renderer = GetComponent<MeshRenderer>(); // get the renderer
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startFadingTime) // if the time to start fading has passed, start fading
        {
            Color color = _renderer.material.color; // get material colour
            color.a = ((destroyTime-Time.time) / fadeTime) * 1f; // set alpha to a percentage of how long is left to fade
            _renderer.material.color = color; // set colour to renderer again
        }

        if(Time.time > destroyTime) // if time to destroy has passed
        {
            Destroy(gameObject); // destory the object once faded
        }
    }
}
