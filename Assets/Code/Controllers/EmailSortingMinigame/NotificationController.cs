using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private Image bgImage = null;
    [SerializeField] private Image icon = null;

    public void Init(int numNotifications)
    {
        rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
        rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
        rectTransform.pivot = new Vector2(0.5f, 1.0f);
        rectTransform.anchoredPosition = new Vector3(0,numNotifications * -106.0f, 0);
    }

    public void OnDestroy()
    {
        Vector2 endPost = rectTransform.anchoredPosition;
        endPost.x += 100;
        rectTransform.DOAnchorPos(endPost, 0.5f);
        bgImage.DOFade(0.1f, 0.5f);
        icon.DOFade(0.1f, 0.5f);
        Destroy(this.gameObject, 0.5f);
    }
}
