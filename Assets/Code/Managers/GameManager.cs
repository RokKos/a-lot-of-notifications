using System;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<BaseScreenController> baseScreenControllers = new List<BaseScreenController>();

    private BaseScreenController curr_screen_controller = null;


    void Start()
    {
        foreach (var baseScreenController in baseScreenControllers)
        {
            baseScreenController.OnScreenDisable();
        }

        curr_screen_controller = FindScreen(ScreenTypes.HomeScreen);
        curr_screen_controller.OnScreenEnable();
    }


    void Update()
    {
    }

    public void SendEvent(string eventTypeStr)
    {
        EventType eventType = ConvertFromStr(eventTypeStr);

        curr_screen_controller.OnScreenExit();
        curr_screen_controller.OnScreenDisable();

        switch (eventType)
        {
            case EventType.Back:
            {
                Debug.Log("Back Event Send");
                break;
            }

            case EventType.ToHomeScreen:
            {
                curr_screen_controller = FindScreen(ScreenTypes.HomeScreen);
                Debug.Log("ToHomeScreen Event Send");
                break;
            }

            case EventType.ToEmailSortingScreen:
            {
                curr_screen_controller = FindScreen(ScreenTypes.EmailSortingScreen);
                Debug.Log("ToEmailSortingScreen Event Send");
                break;
            }

            case EventType.ToClickerScreen:
            {
                curr_screen_controller = FindScreen(ScreenTypes.ClickerScreen);
                Debug.Log("ToClickerScreen Event Send");
                break;
            }

            case EventType.ToCountingScreen:
            {
                curr_screen_controller = FindScreen(ScreenTypes.CountingScreen);
                Debug.Log("ToCountingScreen Event Send");
                break;
            }

            default:
            {
                Debug.Log("Event was not handled");
                break;
            }
        }
        
        curr_screen_controller.OnScreenEnable();
        curr_screen_controller.OnScreenEnter();
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

    readonly Dictionary<string, EventType> _stringToEventEnum = new Dictionary<string, EventType>()
    {
        {"Back", EventType.Back},
        {"ToHomeScreen", EventType.ToHomeScreen},
        {"ToEmailSortingScreen", EventType.ToEmailSortingScreen},
        {"ToClickerScreen", EventType.ToClickerScreen},
        {"ToCountingScreen", EventType.ToClickerScreen}
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