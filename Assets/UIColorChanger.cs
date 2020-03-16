using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIColorChanger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image alaram = default;
    [SerializeField] Image wifi = default;
    [SerializeField] Image signal = default;
    [SerializeField] Image battery1 = default;
    [SerializeField] TextMeshProUGUI batteryTxt = default;
    [SerializeField] Image homeButton;

    [SerializeField] BaseScreenController socialScreen;



    // Update is called once per frame
    void Update()
    {
        if(socialScreen.gameObject.activeInHierarchy)
        {
            alaram.color = Color.black;
            wifi.color = Color.black;
            signal.color = Color.black;
            battery1.color = Color.black;
            batteryTxt.color = Color.black;
            homeButton.color = Color.black;
        }
        else
        {
            alaram.color = Color.white;
            wifi.color = Color.white;
            signal.color = Color.white;
            battery1.color = Color.white;
            batteryTxt.color = Color.white;
            homeButton.color = Color.white;
        }
    }
}
