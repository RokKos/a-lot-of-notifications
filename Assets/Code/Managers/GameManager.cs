﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EventType
{
    Back = 0,
    ToHomeScreen = 1,
    ToEmailSortingScreen = 2,
    ToClickerScreen = 3,
    ToCountingScreen = 4
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private List<BaseScreenController> baseScreenControllers = new List<BaseScreenController>();
    [SerializeField] EmailDataHelper emailDataHelper = null;

    private BaseScreenController _currScreenController = null;
    private BaseScreenController _prevScreenController = null;

    
    private float _workMeterProcent = 500.0f;
    private float _socialMeterProcent = 500.0f;
    private float _healthMeterProcent = 500.0f;

    [SerializeField] private float rateOfMeterDropping = 8.33333333f;
    [SerializeField] private float timeIntervalOfDropping = 1.0f;

    void Start()
    {
        foreach (var baseScreenController in baseScreenControllers)
        {
            baseScreenController.OnScreenDisable();
        }

        _currScreenController = FindScreen(ScreenTypes.HomeScreen);
        _currScreenController.OnScreenEnable();
        _currScreenController.OnScreenEnter();
        _prevScreenController = null;
        
        DOTween.Init(true, true, LogBehaviour.Default);
        emailDataHelper.ReloadCardData(false);
        
        StartCoroutine(DecressMetters());
    }


    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            SendEvent("Back");
        }
        
        _currScreenController.OnScreenUpdate();
    }

    public void SendEvent(string eventTypeStr)
    {
        EventType eventType = ConvertFromStr(eventTypeStr);

        switch (eventType)
        {
            case EventType.Back:
            {
                Debug.Log("Event Send::Back");
                HandleBackButton();
                break;
            }

            case EventType.ToHomeScreen:
            {
                Debug.Log("Event Send::ToHomeScreen");
                ProcessButtonInput(ScreenTypes.HomeScreen);
                break;
            }

            case EventType.ToEmailSortingScreen:
            {
                Debug.Log("Event Send::ToEmailSortingScreen");
                ProcessButtonInput(ScreenTypes.EmailSortingScreen);
                break;
            }

            case EventType.ToClickerScreen:
            {
                Debug.Log("Event Send::ToClickerScreen");
                ProcessButtonInput(ScreenTypes.ClickerScreen);
                break;
            }

            case EventType.ToCountingScreen:
            {
                Debug.Log("Event Send::ToCountingScreen");
                ProcessButtonInput(ScreenTypes.CountingScreen);
                break;
            }

            default:
            {
                Debug.Log("Event was not handled");
                break;
            }
        }
    }


    // HELPER FUNCTIONS

    BaseScreenController FindScreen(ScreenTypes type)
    {
        foreach (var baseScreenController in baseScreenControllers)
        {
            if (baseScreenController.GetScreenType() == type)
            {
                return baseScreenController;
            }
        }

        Debug.Log("No base screen type found");
        return baseScreenControllers[0];
    }

    void ProcessButtonInput(ScreenTypes screenType)
    {
        _currScreenController.OnScreenExit();
        _currScreenController.OnScreenDisable();
        _currScreenController.exitMinigame -= ForceExitMinigame;
            
        _prevScreenController = _currScreenController;
        _currScreenController = FindScreen(screenType);

        uiManager.ChangeBgColor(_currScreenController.bgColor);
        _currScreenController.exitMinigame += ForceExitMinigame;
        
        _currScreenController.OnScreenEnable();
        _currScreenController.OnScreenEnter();
    }

    private void ForceExitMinigame()
    {
        switch (_currScreenController.GetScreenType())
        {
            case ScreenTypes.EmailSortingScreen:
            {
                _workMeterProcent += _currScreenController.GetMiniGameScore();
                _workMeterProcent = Mathf.Max(0.0f,Mathf.Min(1000.0f, _workMeterProcent));
                
                uiManager.ChangeWorkMeter(_workMeterProcent);
                break;
            }
            
            case ScreenTypes.ClickerScreen:
            {
                _socialMeterProcent += _currScreenController.GetMiniGameScore();
                _socialMeterProcent = Mathf.Max(0.0f,Mathf.Min(1000.0f, _socialMeterProcent));
                uiManager.ChangeSocialMeter(_socialMeterProcent);
                break;
            }
            
            case ScreenTypes.CountingScreen:
            {
                _healthMeterProcent += _currScreenController.GetMiniGameScore();
                _healthMeterProcent = Mathf.Max(0.0f,Mathf.Min(1000.0f, _healthMeterProcent));
                uiManager.ChangeSocialMeter(_healthMeterProcent);
                break;
            }
            
        }
        SendEvent("Back");
    }

    void HandleBackButton()
    {
        if (_prevScreenController != null)
        {
            _currScreenController.OnScreenExit();
            _currScreenController.OnScreenDisable();
            _currScreenController.exitMinigame -= ForceExitMinigame;

            uiManager.ChangeBgColor(_prevScreenController.bgColor);
            _prevScreenController.OnScreenEnable();
            _prevScreenController.OnScreenEnter();
            
            _currScreenController = _prevScreenController;
            
            _currScreenController.exitMinigame += ForceExitMinigame;
            _prevScreenController = null;
        }
        else
        {
            Debug.Log("BACK EVENT NOT HANDLED YOU ARE AT HOME SCREEN");
        }
    }

    readonly Dictionary<string, EventType> _stringToEventEnum = new Dictionary<string, EventType>()
    {
        {"Back", EventType.Back},
        {"ToHomeScreen", EventType.ToHomeScreen},
        {"ToEmailSortingScreen", EventType.ToEmailSortingScreen},
        {"ToClickerScreen", EventType.ToClickerScreen},
        {"ToCountingScreen", EventType.ToCountingScreen}
    };

    private EventType ConvertFromStr(string str)
    {
        EventType conv;

        if (_stringToEventEnum.TryGetValue(str, out conv))
        {
            return conv;
        }

        Debug.Log("WRONG STRING FOR EVENT");
        return EventType.ToHomeScreen;
    }

    private void UpdateMeters()
    {
        if (_currScreenController.GetScreenType() == ScreenTypes.HomeScreen)
        {
            uiManager.ChangeWorkMeter(_workMeterProcent);
            uiManager.ChangeSocialMeter(_socialMeterProcent);
            uiManager.ChangeHealthMeter(_healthMeterProcent);
        }
    }
    
    private IEnumerator DecressMetters()
    {
        while(true)
        {
            _workMeterProcent -= rateOfMeterDropping;
            _socialMeterProcent -= rateOfMeterDropping;
            _healthMeterProcent -= rateOfMeterDropping;
            UpdateMeters();
            yield return new WaitForSeconds(timeIntervalOfDropping);
            
        }
    }
}