using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountingSheepScreenController : BaseScreenController
{
    public GameObject sheepPrefab;
    public TextMeshProUGUI scoreTMP;

    int playerScore = 0;
    public float timeToReachEnd = 5f;

    public override void OnScreenEnter()
    {
        base.OnScreenEnter();
        sheepPrefab.GetComponent<sheepController>().initSheep();
    }

    public override void OnScreenUpdate()
    {
        base.OnScreenUpdate();

        
    }

    public void addCounter()
    {

            playerScore++;
            scoreTMP.text = ("Sheep count: " + playerScore);
            timeToReachEnd = timeToReachEnd * 0.9f;    
    }

}
