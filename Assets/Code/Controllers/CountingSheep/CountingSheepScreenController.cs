using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountingSheepScreenController : BaseScreenController
{
    public GameObject sheepPrefab;
    public TextMeshProUGUI scoreTMP;
    public Transform scoreFeedback;
    public int playerScore = 0;
    public float timeToReachEnd = 5f;
    public float timeToSpawn = 2f;
    public float timeToJump = 1f;

    public override void OnScreenEnter()
    {
        base.OnScreenEnter();
        sheepPrefab.GetComponent<sheepController>().initSheep();
        scoreTMP.text = ("Sheep count: 0/10");
    }

    public override void OnScreenUpdate()
    {
        base.OnScreenUpdate();
        
        if(playerScore == 10)
        {
            AddScore(playerScore * 700);
            playerScore = 0;
            sheepPrefab.GetComponent<sheepController>().resetSheep();
            exitMinigame();
        }        
    }

    public void addCounter()
    {
            playerScore++;
            scoreTMP.text = ("Sheep count: " + playerScore + "/10");
    }

    public override void OnScreenExit()
    {
        base.OnScreenExit();
        AddScore(playerScore * 700);
    }

}
