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
    }

    private IEnumerator SpamNotifications()
    {
        while(true)
        {
            notificationSpawnerController.SpawnNotification();
            Debug.Log ("SpamNotifications: "+(int)Time.time);
            yield return new WaitForSeconds(1f);
        }
    }
}
