using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ScreenTypes
{
    HomeScreen = 0,
    EmailSortingScreen = 1,
    ClickerScreen = 2,
    CountingScreen = 3
}

public class BaseScreenController : MonoBehaviour
{
    [SerializeField] private ScreenTypes screenType = ScreenTypes.HomeScreen;
    
    public ScreenTypes GetScreenType() {
        return screenType;
    }


    public virtual void OnScreenEnter()
    {
        Debug.Log(screenType.ToString("F") + "::OnScreenEnter()");
    }
    
    public virtual void OnScreenExit()
    {
        Debug.Log(screenType.ToString("F") + "::OnScreenExit()");
    }
    
    public virtual void OnScreenUpdate()
    {
        
    }
    
    public virtual void OnScreenEnable()
    {
        Debug.Log(screenType.ToString("F") + "::OnScreenEnable()");
        this.gameObject.SetActive(true);
    }
    
    public virtual void OnScreenDisable()
    {
        Debug.Log(screenType.ToString("F") + "::OnScreenDisable()");
        this.gameObject.SetActive(false);
    }


}
