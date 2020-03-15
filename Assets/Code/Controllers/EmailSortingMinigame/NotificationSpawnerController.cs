using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class NotificationSpawnerController : MonoBehaviour
{
    [SerializeField] private NotificationController prefabNotificationController = null;
    
    [SerializeField] private Transform parentNotifications = null;
    
    private List<NotificationController> notificationsDisplayed = new List<NotificationController>();

    
    private void Start()
    {
        notificationsDisplayed = new List<NotificationController>();
    }

    public void SpawnNotification()
    {
        NotificationController notificationController = Instantiate(prefabNotificationController, parentNotifications);
        notificationController.Init(notificationsDisplayed.Count);
        notificationsDisplayed.Add(notificationController);
        
    }

    public void DeleteNotification()
    {
        NotificationController lastNotification = notificationsDisplayed.Last();
        notificationsDisplayed.Remove(lastNotification);
        lastNotification.OnDestroy();
    }
    
    public void OnSwipeHandler(string id)
    {
        switch(id)
        {
            case DirectionId.ID_UP:
                Debug.Log("OnSwipeHandler::ID_UP");
                break;

            case DirectionId.ID_DOWN:
                Debug.Log("OnSwipeHandler::ID_DOWN");
                break;

            case DirectionId.ID_LEFT:
                Debug.Log("OnSwipeHandler::ID_LEFT");
                DeleteNotification();
                break;

            case DirectionId.ID_RIGHT:
                Debug.Log("OnSwipeHandler::ID_RIGHT");
                DeleteNotification();
                break;
        }
    }
}
