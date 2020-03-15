using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform = null;

    public void Init(int numNotifications)
    {
        rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
        rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
        rectTransform.pivot = new Vector2(0.5f, 1.0f);
        rectTransform.anchoredPosition = new Vector3(0,numNotifications * -106.0f, 0);
    }
}
