using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


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
    
    [Header("Properties")]
    [SerializeField] public Color bgColor = Color.magenta;
    
    public ScreenTypes GetScreenType() {
        return screenType;
    }


    public virtual void OnScreenEnter()
    {
        Debug.Log(screenType.ToString("F") + "::OnScreenEnter()");
        gameObject.transform.DOPunchScale(Vector3.one * 0.1f, 0.4f);

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
