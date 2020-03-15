﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NotificationController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private Image bgImage = null;
    [SerializeField] private Image icon = null;
    
    [Header("Numbers")]
    [SerializeField] private float size = 106.0f;
    [SerializeField] private float goToLeftDirection = -100.0f;
    [SerializeField] private float goToRightDirection = 100.0f;
    [SerializeField] private Color spamColor = Color.black;
    [SerializeField] private Color usefullColor = Color.black;

    
    readonly List<Color> possibleColors = new List<Color>()
    {
        Color.cyan, Color.magenta, Color.red, Color.blue, Color.green
    };
    
    public void Init()
    {
        rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
        rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
        rectTransform.pivot = new Vector2(0.5f, 1.0f);
        rectTransform.anchoredPosition = new Vector3(0,0, 0);
        
        Color iconColor = possibleColors[Random.Range(0, possibleColors.Count)];
        iconColor.a = 0.0f;
        icon.color = iconColor;
        icon.DOFade(1.0f, 0.35f);

        Color bgColor = bgImage.color;
        bgColor.a = 0;
        bgImage.color = bgColor;
        bgImage.DOFade(1.0f, 0.35f);
    }

    public void MoveDown()
    {
        Vector2 endPost = rectTransform.anchoredPosition;
        endPost.y -= size;
        rectTransform.DOAnchorPos(endPost, 0.35f);
    }



    public void OnReadUsefull()
    {
        OnDestroyNotification(goToLeftDirection, usefullColor);
    }

    public void OnDeleteSpam()
    {
        OnDestroyNotification(goToRightDirection, spamColor);
    }

    private void OnDestroyNotification(float direction, Color color)
    {
        Vector2 endPost = rectTransform.anchoredPosition;
        endPost.x += direction;
        rectTransform.DOAnchorPos(endPost, 0.5f);

        color.a = 0.1f;
        bgImage.DOColor(color, 0.5f);
        icon.DOFade(0.1f, 0.5f);
        
        Destroy(this.gameObject, 0.5f);
    }
}
