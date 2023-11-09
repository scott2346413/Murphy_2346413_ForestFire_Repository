using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FireStarter : MonoBehaviour
{
    //cooldown variables
    [SerializeField] float cooldownTime; // how long the lighter takes to cooldown
    float nextCooldown; // when will the lighter be cooled down
    [SerializeField] Slider cooldownSlider; // slider which shows the cooldown time

    //XR and interaction variables
    [SerializeField] XRRayInteractor interactor; // the ray interactor to get current cell
    [SerializeField] InputActionReference startFire; // input used to start fire

    //SFX variables
    [SerializeField] AudioSource lighterSound; // sound played when lighter activates


    private void Start()
    {
        nextCooldown = Time.time; // set next cooldown as now so lighter is ready to use
        cooldownSlider.maxValue = cooldownTime; // set cooldown sliders max value to be the cooldown time
    }

    private void Update()
    {
        if (startFire.action.IsPressed()) // if the input action is pressed, start a fire
        {
            StartFire(); // call start fire method
        }

        updateCooldownSlider(); // updates the cooldown slider value
    }

    //starts a fire at the selected cell
    void StartFire()
    {
        if(!(Time.time>nextCooldown)) // return if cooldown not done yet
        {
            return;
        }

        ForestFireCell cell = getCurrentCell(); // get the current cell being selected

        if(cell.cellState == ForestFireCell.State.Alight || cell.cellState == ForestFireCell.State.Burnt) // return if the cell has already been set alight or burnt
        {
            return;
        }

        cell.SetAlight(); // set cell alight
        lighterSound.Play(); // player lighter sound
        nextCooldown = Time.time + cooldownTime; // set next cooldown to happen after cooldown time
    }

    //Get Current Cell gets the cell being selected by XR Ray Interaction
    ForestFireCell getCurrentCell()
    {
        RaycastHit hit;
        interactor.TryGetCurrent3DRaycastHit(out hit); // Get the current raycast hit from interactor

        if(hit.collider != null) // if hit isn't null, get the cell component
        {
            return hit.collider.GetComponent<ForestFireCell>(); // return cell component
        }

        return null; // returns null when hit is null
    }

    //Update Cooldown Slider manages the cooldown slider, setting its values
    void updateCooldownSlider()
    {
        cooldownSlider.gameObject.SetActive(Time.time < nextCooldown); // only show when we are cooling down

        float timeToCooldown = nextCooldown - Time.time; // get how long we need to wait for cooldown
        cooldownSlider.value = timeToCooldown; // set values for cooldown slider
    }
}
