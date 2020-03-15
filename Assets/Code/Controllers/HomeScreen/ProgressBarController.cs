using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    
    [SerializeField] private float minBarValue = 0.0f;
    [SerializeField] private float maxBarValue = 330.0f;

    
    
    public void SetBarProcent(float procent)
    {
        float barValue = Mathf.Lerp(maxBarValue, minBarValue, procent);
        rectTransform.offsetMax = new Vector2(-barValue, 0); 
    }
}
