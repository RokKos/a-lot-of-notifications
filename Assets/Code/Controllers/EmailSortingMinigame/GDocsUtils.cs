﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class GDocsUtils : MonoBehaviour
{
    
    public static IEnumerator DownloadCSVCoroutine(string docId, bool saveAsset = true, string assetName = "EmailNotificationData", string sheetId = null)
    {
        string url =
            "https://docs.google.com/spreadsheets/d/" + docId + "/export?format=csv";

        if (!string.IsNullOrEmpty(sheetId))
            url += "&gid=" + sheetId;

        WWWForm form = new WWWForm();
        WWW download = new WWW(url, form);

        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {

            if (saveAsset)
            {
                if (!string.IsNullOrEmpty(assetName)) {
                    string file = "Assets/Resources/" + assetName + ".csv";
                    File.WriteAllText(file, download.text);

                    var data = Resources.Load(assetName) as TextAsset;
                    EmailDataHelper.data = ParseCSV(data.text);
                }
                else
                {
                    throw new System.Exception("assetName is null");
                }
            }
        }
    }


    public static readonly string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    public static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    public static readonly char[] TRIM_CHARS = { '\"' };

    public static List<List<string>> ReadCSV(string file)
    {
        var data = Resources.Load(file) as TextAsset;
        return ParseCSV(data.text);
    }

    public static List<List<string>> ParseCSV(string text)
    {
        text = CleanReturnInCsvTexts(text);

        var list = new List<List<string>>();
        var lines = Regex.Split(text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

        bool jumpedFirst = false;

        foreach (var line in lines)
        {
            if (!jumpedFirst)
            {
                jumpedFirst = true;
                continue;
            }
            var values = Regex.Split(line, SPLIT_RE);

            var entry = new List<string>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                var value = values[j];
                value = DecodeSpecialCharsFromCSV(value);
                entry.Add(value);
            }
            list.Add(entry);
        }
        list = CleanEmptyEntries(list);
        return list;
    }

    private static string DecodeSpecialCharsFromCSV(string value)
    {
        value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "").Replace("<br>", "\n").Replace("<c>", ",");
        return value;
    }

    private static string CleanReturnInCsvTexts(string text)
    {
        text = text.Replace("\"\"", "'");

        if (text.IndexOf("\"") > -1)
        {
            string clean = "";
            bool insideQuote = false;
            for (int j = 0; j < text.Length; j++)
            {
                if (!insideQuote && text[j] == '\"')
                {
                    insideQuote = true;
                }
                else if (insideQuote && text[j] == '\"')
                {
                    insideQuote = false;
                }
                else if (insideQuote)
                {
                    if (text[j] == '\n')
                        clean += "<br>";
                    else if (text[j] == ',')
                        clean += "<c>";
                    else
                        clean += text[j];
                }
                else
                {
                    clean += text[j];
                }
            }
            text = clean;
        }
        return text;
    }


    private static List<List<string>> CleanEmptyEntries(List<List<string>> entries) {
        for (int j = 0; j < entries.Count; ++j)
        {
            for (int i = 0; i < entries[j].Count; ++i)
            {
                if (entries[j][i] == "")
                {
                    entries[j].RemoveAt(i);
                    i--;
                }
            }
        }

        return entries;
    }

}
