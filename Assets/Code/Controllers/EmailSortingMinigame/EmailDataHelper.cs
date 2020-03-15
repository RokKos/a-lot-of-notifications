using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class EmailDataHelper : MonoBehaviour
{
    public static List<List<string>> data;

    private static int _currNotification = -1;
    private const int KEmailSenderIndex = 0;
    private const int KShortDescriptionIndex = 1;
    private const int KIconIDIndex = 2;
    private const int KTypeIndex = 2;


    private const string KGDocID = "1ZMW6Aqlu3leX_RtEEBetpmrZTJ31uU_K0fv2G-2ZeYQ";
    
    public void ReloadCardData(bool reloadFromInternetReloadFromInternet = false)
    {
        if (reloadFromInternetReloadFromInternet)
        {
            IEnumerator coroutine = GDocsUtils.DownloadCSVCoroutine(KGDocID);
            StartCoroutine(coroutine);
        }
        else
        {
            data = GDocsUtils.ReadCSV("EmailNotificationData");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadCardData(true);
        }
    }

    public static string GetEmailSenderText()
    {
        return data[_currNotification][KEmailSenderIndex];
    }

    public static string GetShortDescriptionText()
    {
        return data[_currNotification][KShortDescriptionIndex];
    }

    public static int GetIconId()
    {
        return GetIntValue(data[_currNotification][KIconIDIndex]);
    }
    
    public static string GetType()
    {
        return data[_currNotification][KTypeIndex];
    }
    
    public static int GetIntValue(string s)
    {
        int change = 0;
        int sing = 1;
        Debug.Log(s);
        if (s.Substring(0, 1) == "−")
        {
            sing = -1;
            s = s.Substring(1);
        }

        System.Int32.TryParse(s, out change);
        return sing * change;
    }

    public static void SelectNotification()
    {
        _currNotification = Random.Range(0, data.Count);
    }
}