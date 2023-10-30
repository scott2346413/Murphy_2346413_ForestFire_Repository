using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FireStarter : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    float nextCooldown;

    [SerializeField] XRRayInteractor interactor;


    private void Start()
    {
        nextCooldown = Time.time;
    }

    private void Update()
    {
        StartFire();
    }

    void StartFire()
    {
        if(!(Time.time>nextCooldown))
        {
            return;
        }

        ForestFireCell cell = getCurrentCell();
        Debug.Log(cell.gameObject);

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
}
