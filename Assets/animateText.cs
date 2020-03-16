using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class animateText : MonoBehaviour
{
    Text startText;
    void Start()
    {
        startText.DOText(
            "Welcome to life. Press any button to start.", 2f, false, ScrambleMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
