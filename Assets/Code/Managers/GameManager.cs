using System;
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

    private BaseScreenController _currScreenController = null;
    private BaseScreenController _prevScreenController = null;


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

        _prevScreenController = _currScreenController;
        _currScreenController = FindScreen(screenType);

        uiManager.ChangeBgColor(_currScreenController.bgColor);
        _currScreenController.OnScreenEnable();
        _currScreenController.OnScreenEnter();
    }

    void HandleBackButton()
    {
        if (_prevScreenController != null)
        {
            _currScreenController.OnScreenExit();
            _currScreenController.OnScreenDisable();

            uiManager.ChangeBgColor(_prevScreenController.bgColor);
            _prevScreenController.OnScreenEnable();
            _prevScreenController.OnScreenEnter();
            
            _currScreenController = _prevScreenController;
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
}