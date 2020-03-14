using System;
using TMPro;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText = null;

    // Update is called once per frame
    void Update()
    {
        DateTime time = DateTime.Now;
        var hours = time.Hour;
        var minute = time.Minute;
        clockText.SetText(hours.ToString("D2") + ":" + minute.ToString("D2"));
    }
}
