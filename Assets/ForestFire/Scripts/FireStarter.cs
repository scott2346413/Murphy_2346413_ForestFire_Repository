using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FireStarter : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    float nextCooldown;

    ForestFire3D forestFire;

    [SerializeField] XRRayInteractor interactor;
    [SerializeField] InputActionReference startFire;
    [SerializeField] Slider cooldownSlider;

    [SerializeField] AudioSource lighterSound;


    private void Start()
    {
        nextCooldown = Time.time;
        forestFire = FindObjectOfType<ForestFire3D>();
        cooldownSlider.maxValue = cooldownTime;
    }

    private void Update()
    {
        if (startFire.action.IsPressed())
        {
            Debug.Log("start fire");
            StartFire();
        }

        updateCooldownSlider();
    }

    void StartFire()
    {
        if(!(Time.time>nextCooldown))
        {
            return;
        }

        ForestFireCell cell = getCurrentCell();

        if(cell.cellState == ForestFireCell.State.Alight || cell.cellState == ForestFireCell.State.Burnt)
        {
            return;
        }

        cell.SetAlight();
        lighterSound.Play();
        nextCooldown = Time.time + cooldownTime;
    }

    ForestFireCell getCurrentCell()
    {
        RaycastHit hit;
        interactor.TryGetCurrent3DRaycastHit(out hit);

        if(hit.collider != null)
        {
            return hit.collider.GetComponent<ForestFireCell>();
        }

        return null;
    }

    void updateCooldownSlider()
    {
        cooldownSlider.gameObject.SetActive(Time.time < nextCooldown);

        float timeToCooldown = nextCooldown - Time.time;
        cooldownSlider.value = timeToCooldown;
    }
}
