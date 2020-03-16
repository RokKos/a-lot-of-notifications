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
        DOTween.Sequence()
            .Append(transform.DOPunchScale(new Vector3(1.3f,1.3f,1.3f), 1.2f, 1, 1))
            .Append(transform.DOScale(Vector3.one, 0.2f));
        
    }


}
