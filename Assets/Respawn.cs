using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public void respawn()
    {
        SceneManager.LoadScene("ForestFire3D");
    }
}
