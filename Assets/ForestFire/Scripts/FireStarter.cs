using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    float nextCooldown;

    [SerializeField] ForestFire3D forestFire;

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

        Vector2Int cellPosition = getCurrentCell();
        forestFire.forestFireCells[cellPosition.x, cellPosition.y].SetAlight();

    }

    Vector2Int getCurrentCell()
    {
        Vector2Int cellPosition = new Vector2Int(Mathf.FloorToInt(transform.position.x/4), Mathf.FloorToInt(transform.position.z/4));
        return cellPosition;
    }
}
