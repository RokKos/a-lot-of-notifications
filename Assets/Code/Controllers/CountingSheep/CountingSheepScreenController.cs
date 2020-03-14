using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountingSheepScreenController : BaseScreenController
{
    public GameObject sheepPrefab;
    public TextMeshProUGUI scoreTMP;

    int playerScore = 1;
    public float timeToReachEnd = 5f;
    bool ovacaInArea;

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
        if(ovacaInArea)
        {
            playerScore++;
            scoreTMP.text = ("Score: " + playerScore);
            timeToReachEnd = timeToReachEnd * 0.9f;    
        }

    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Ovca"))
        {
            ovacaInArea = true;
        }
        else
        {
            ovacaInArea = false;
        }
    }
}
