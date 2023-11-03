using System.Collections;
using System.Collections.Generic;
using MagicPigGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    ForestFire3D forestFire;
    [SerializeField] HorizontalProgressBar forestHealth;
    [SerializeField] float percentGoal;

    // Start is called before the first frame update
    void Start()
    {
        forestFire = FindObjectOfType<ForestFire3D>();
        forestHealth.SetProgress(1);
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (forestFire.totalGrassAndTrees - forestFire.totalBurnt) / forestFire.totalGrassAndTrees;
        forestHealth.SetProgress(progress);

        if(progress < percentGoal)
        {
            SceneManager.LoadScene("ForestFireWin");
        }
    }
}
