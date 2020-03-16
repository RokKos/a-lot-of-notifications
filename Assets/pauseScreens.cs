using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class pauseScreens : MonoBehaviour
{
    [SerializeField] bool EndScreen;

    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject endPanel;
    [SerializeField] Text startText;
    [SerializeField] GameObject maingame;

    private void Start() {
        if(!EndScreen)
        {
            startText.DOText("Welcome to life. Press any button to start.", 2f, false, ScrambleMode.All);
            FindObjectOfType<SoundController>().Play("typing");
        }
    }
    void Update()
    {
        
        if(Input.anyKey && EndScreen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;

        }
        if (Input.anyKey && !EndScreen)
        {
            Time.timeScale = 1f;
            startPanel.SetActive(false);
            maingame.SetActive(true);
        }
    }
}
