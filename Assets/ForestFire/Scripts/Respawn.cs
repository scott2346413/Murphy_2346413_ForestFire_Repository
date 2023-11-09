using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    // Respawn can be called by button to respawn player
    public void respawn()
    {
        SceneManager.LoadScene("ForestFire3D"); //reloads the fores fire 3D scene
    }
}
