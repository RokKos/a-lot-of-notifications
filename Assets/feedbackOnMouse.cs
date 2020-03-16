using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class feedbackOnMouse : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] TextMeshProUGUI feedbackText;
 
    void Update()
    {
        var mousePosition = Input.mousePosition;
        var mousWorldPos = camera.ScreenToWorldPoint(mousePosition);

        transform.position = mousePosition;
    }

    public void PunchPositiveFeedback(string feedbackMsg)
    {
        feedbackText.text = feedbackMsg;
        transform.DOPunchScale(Vector3.one, 0.2f, 4, 1);
    }


}
