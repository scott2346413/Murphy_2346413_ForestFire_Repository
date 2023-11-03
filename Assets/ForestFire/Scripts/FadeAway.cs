using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    [SerializeField] float fadeDelay;
    [SerializeField] float fadeTime;

    float startFadingTime;
    float destroyTime;

    MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        startFadingTime = Time.time + fadeDelay;
        destroyTime = Time.time + fadeDelay + fadeTime;

        _renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startFadingTime)
        {
            Color color = _renderer.material.color;
            color.a = ((destroyTime-Time.time) / fadeTime) * 1f;
            _renderer.material.color = color;
        }

        if(Time.time > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
