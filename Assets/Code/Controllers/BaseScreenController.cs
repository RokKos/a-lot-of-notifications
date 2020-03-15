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
        
    private int _minigameScore = 0;
    
    public delegate void ExitMinigame();
    public ExitMinigame exitMinigame;
    protected virtual void AddScore(int score) {
        _minigameScore += score;
        Debug.Log("Score is now:" + _minigameScore.ToString("D") + " increased by: " + score.ToString("D"));
    }
    
    protected virtual void SubstractScore(int score) {
            _minigameScore -= score;
            Debug.Log("Score is now:" + _minigameScore.ToString("D") + " Decrease by: " + score.ToString("D"));
        }
    
    public int GetMiniGameScore() {
        return _minigameScore;
    }
    
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
        _minigameScore = 0;
    }
    
    public virtual void OnScreenDisable()
    {
        Debug.Log(screenType.ToString("F") + "::OnScreenDisable()");
        this.gameObject.SetActive(false);
    }


}
