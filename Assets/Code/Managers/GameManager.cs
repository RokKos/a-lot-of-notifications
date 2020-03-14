using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<BaseScreenController> baseScreenControllers = new List<BaseScreenController>();
    
    void Start()
    {
        foreach (var baseScreenController in baseScreenControllers)
        {
            baseScreenController.OnScreenDisable();
        }

        var homeScreen = FindScreen(ScreenTypes.HomeScreen);
        homeScreen.OnScreenEnable();
    }

    
    void Update()
    {
        
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
}
