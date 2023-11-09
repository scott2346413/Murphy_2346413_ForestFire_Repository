using System.Collections;
using System.Collections.Generic;
using MagicPigGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    ForestFire3D forestFire; // The Forest Fire script so we can check percentage of forest burnt
    [SerializeField] HorizontalProgressBar forestHealth; // Progress bar for forest health
    [SerializeField] float percentGoal; // How much of the forest needs to be burned

    // Start is called before the first frame update
    void Start()
    {
        forestFire = FindObjectOfType<ForestFire3D>(); // Get the forest fire script
        forestHealth.SetProgress(1); // Set starting forest health to full
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (forestFire.totalGrassAndTrees - forestFire.totalBurnt) / forestFire.totalGrassAndTrees; // Calculate the percentage of the forest still unburnt
        forestHealth.SetProgress(progress); // Set the progress bar value

        if(progress < percentGoal) // if progress reaches below the goal, load win scene
        {
            SceneManager.LoadScene("ForestFireWin"); // Loads the final win scene of the game
        }
    }
}
