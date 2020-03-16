﻿using System;
using TMPro;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText = null;
    [SerializeField] private TextMeshProUGUI daysText = null;
    [SerializeField] BaseScreenController homeScreen = default;


    [Header("In game time")]
    float gameHourInSeconds = 12; 
    int gameMinutes;
    int gameHours;
    int gameDays;
    [Header("Real life time")]
    [SerializeField] float realSeconds;
    private void Start() 
    {
        gameDays = 1;
        gameHours = 8;
        gameMinutes = 0;    
    }
    
    void Update()
    {

        
        realSeconds += gameHourInSeconds * Time.deltaTime;
        gameMinutes = (int) realSeconds;
        
        advanceGameTime();

        clockText.SetText(gameHours.ToString("D2") + ":" + gameMinutes.ToString("D2"));
        hideOrShowClock();

    }



    
    void advanceGameTime()
    {
        if (gameMinutes >= 59)
        {
            realSeconds = 0;
            gameMinutes = 0;
            gameHours++;
            if (gameHours == 24)
            {
                gameHours = 0;
                gameDays ++;
                daysText.SetText("Day: " + gameDays.ToString("D2"));

            }
        }
    }
    void hideOrShowClock()
    {
        var invisColor = new Color (1,1,1,0);
        var white = new Color (1,1,1,1);

        if(homeScreen.gameObject.activeInHierarchy == false)
        {
            clockText.color = invisColor;
            daysText.color = invisColor;
        }
        else
        {
            clockText.color = white;
            daysText.color = white;
        }
    }
}
