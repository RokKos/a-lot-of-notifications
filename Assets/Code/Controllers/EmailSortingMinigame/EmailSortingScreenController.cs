using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailSortingScreenController : BaseScreenController
{

    [SerializeField] private float spamTime = 0.5f;
    
    [SerializeField] NotificationSpawnerController notificationSpawnerController = null;
    
    [SerializeField] List<float> notificationSpawnerTimes = new List<float>();

    private int timeIndex = 0;
    public override void OnScreenEnter()
    {
        base.OnScreenEnter();
        StartCoroutine (SpamNotifications());

        notificationSpawnerController.correctAction += AddScore;
        notificationSpawnerController.incorectAction += SubstractScore;
        timeIndex = 0;
    }

    public override void OnScreenExit()
    {
        base.OnScreenExit();
        notificationSpawnerController.correctAction -= AddScore;
        notificationSpawnerController.incorectAction -= SubstractScore;
        FindObjectOfType<SoundController>().Play("click2");
        
    }

    private IEnumerator SpamNotifications()
    {
        while(timeIndex < notificationSpawnerTimes.Count)
        {
            Debug.Log(timeIndex.ToString("D") + " time: " + notificationSpawnerTimes[timeIndex].ToString());
            notificationSpawnerController.SpawnNotification();
            yield return new WaitForSeconds(notificationSpawnerTimes[timeIndex]);
            timeIndex++;
        }

        exitMinigame();
    }
}
