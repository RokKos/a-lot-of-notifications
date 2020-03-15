using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailSortingScreenController : BaseScreenController
{

    [SerializeField] private float spamTime = 0.5f;
    
    [SerializeField] NotificationSpawnerController notificationSpawnerController = null;
    public override void OnScreenEnter()
    {
        base.OnScreenEnter();
        StartCoroutine (SpamNotifications());

        notificationSpawnerController.correctAction += AddScore;
        notificationSpawnerController.incorectAction += SubstractScore;
    }

    public override void OnScreenExit()
    {
        base.OnScreenExit();
        notificationSpawnerController.correctAction -= AddScore;
        notificationSpawnerController.incorectAction -= SubstractScore;
        
    }

    private IEnumerator SpamNotifications()
    {
        while(true)
        {
            notificationSpawnerController.SpawnNotification();
            yield return new WaitForSeconds(1f);
        }
    }
}
