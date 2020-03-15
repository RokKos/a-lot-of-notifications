using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawnerController : MonoBehaviour
{
    [SerializeField] private NotificationController prefabNotificationController = null;
    
    [SerializeField] private Transform parentNotifications = null;


    private int numNotifications = 0;
    private void Start()
    {
        numNotifications = 0;
    }

    public void SpawnNotification()
    {
        NotificationController notificationController = Instantiate(prefabNotificationController, parentNotifications);
        notificationController.Init(numNotifications);

        numNotifications++;
    }

    public void DeleteNotification()
    {
        
        numNotifications--;
    }
}
