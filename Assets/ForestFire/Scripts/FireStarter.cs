using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FireStarter : MonoBehaviour
{
    //cooldown variables
    [SerializeField] float cooldownTime;
    float nextCooldown;
    [SerializeField] Slider cooldownSlider;

    //XR and interaction variables
    [SerializeField] XRRayInteractor interactor;
    [SerializeField] InputActionReference startFire;

    //SFX variables
    [SerializeField] AudioSource lighterSound;


    private void Start()
    {
        //set cooldown values
        nextCooldown = Time.time;
        cooldownSlider.maxValue = cooldownTime;
    }

    private void Update()
    {
        //start a fire if the action button is pressed
        if (startFire.action.IsPressed())
        {
            Debug.Log("start fire");
            StartFire();
        }

        updateCooldownSlider();
    }

    //starts a fire at the selected cell
    void StartFire()
    {
        //return if cooldown not done yet
        if(!(Time.time>nextCooldown))
        {
            return;
        }

        ForestFireCell cell = getCurrentCell();

        //return if the cell has already been set alight or burnt
        if(cell.cellState == ForestFireCell.State.Alight || cell.cellState == ForestFireCell.State.Burnt)
        {
            return;
        }

        //set cell alight, play sound and reset timer
        cell.SetAlight();
        lighterSound.Play();
        nextCooldown = Time.time + cooldownTime;
    }

    //gets the cell being selected by XR Ray Interaction
    ForestFireCell getCurrentCell()
    {
        //get raycast hit from interactor
        RaycastHit hit;
        interactor.TryGetCurrent3DRaycastHit(out hit);

        //if not null, get cell component
        if(hit.collider != null)
        {
            return hit.collider.GetComponent<ForestFireCell>();
        }

        return null;
    }

    //manages the cooldown slider, setting its values
    void updateCooldownSlider()
    {
        cooldownSlider.gameObject.SetActive(Time.time < nextCooldown);

        float timeToCooldown = nextCooldown - Time.time;
        cooldownSlider.value = timeToCooldown;
    }
}
