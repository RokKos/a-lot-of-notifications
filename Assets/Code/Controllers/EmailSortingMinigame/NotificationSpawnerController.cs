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
    
    private List<NotificationController> _notificationsDisplayed = new List<NotificationController>();

    
    public delegate void CorrectAction(int bonus);
    public CorrectAction correctAction;
    
    public delegate void IncorectAction(int penalty);
    public IncorectAction incorectAction;
    
    private void Awake()
    {
        _notificationsDisplayed = new List<NotificationController>();
    }

    public void SpawnNotification()
    {
        NotificationController insertedNotificationController = Instantiate(prefabNotificationController, parentNotifications);
        insertedNotificationController.Init();

        foreach (var notificationController in _notificationsDisplayed)
        {
            notificationController.MoveDown();
        }

        if (_notificationsDisplayed.Count == 0)
        {
            _notificationsDisplayed.Add(insertedNotificationController);
        }
        else
        {
            _notificationsDisplayed.Insert(0,insertedNotificationController);
        }



    }

    private void DeleteNotification()
    {
        var lastNotification = RemoveNotification();
        if (lastNotification != null)
        {
            lastNotification.OnDeleteSpam();

            switch (lastNotification.type)
            {
                case NotificationType.Bad:
                {
                    correctAction(0);
                    break;
                }

                case NotificationType.Medium:
                {
                    incorectAction(25);
                    break;
                }

                case NotificationType.Good:
                {
                    incorectAction(100);
                    break;
                }
            }
        }
    }
    
    private void ReadNotification()
    {
        var lastNotification = RemoveNotification();
        if (lastNotification != null)
        {
            lastNotification.OnReadUsefull();

            switch (lastNotification.type)
            {
                case NotificationType.Bad:
                {
                    incorectAction(50);
                    break;
                }

                case NotificationType.Medium:
                {
                    correctAction(25);
                    break;
                }

                case NotificationType.Good:
                {
                    correctAction(100);
                    break;
                }
            }
        }
    }

    private NotificationController RemoveNotification()
    {
        if (_notificationsDisplayed.Count > 0)
        {
            NotificationController lastNotification = _notificationsDisplayed.Last();
            _notificationsDisplayed.Remove(lastNotification);
            return lastNotification;
        }

        return null;
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
                ReadNotification();
                break;

            case DirectionId.ID_RIGHT:
                Debug.Log("OnSwipeHandler::ID_RIGHT");
                DeleteNotification();
                break;
        }
    }
}
