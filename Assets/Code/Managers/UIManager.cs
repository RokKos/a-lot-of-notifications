using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [Header("BG Elements")]
   [SerializeField] private Image phoneScreenBg = null;
   [SerializeField] private Image cakeBgElem = null;
   [SerializeField] private Image bedBgElem = null;

   [Header("Progress Bars")]
   [SerializeField] private ProgressBarController workMeter = null;
   [SerializeField] private ProgressBarController socialMeter = null;
   [SerializeField] private ProgressBarController healthMeter = null;
   
   
   public void ChangeBgColor(Color bgColor)
   {
      bgColor.a = 0;
      phoneScreenBg.color = bgColor;
      phoneScreenBg.DOFade(1.0f, 1.00f);
   }

   public void ChangeWorkMeter(float diff)
   {
      workMeter.SetBarProcent(diff / 1000.0f);
   }
   
   public void ChangeSocialMeter(float diff)
   {
      socialMeter.SetBarProcent(diff / 1000.0f);
   }
   
   public void ChangeHealthMeter(float diff)
   {
      healthMeter.SetBarProcent(diff / 1000.0f);
   }

   public void EnableBedBgElement(bool enable)
   {
      bedBgElem.gameObject.SetActive(enable);
   }
   public void EnableCakeBgElement(bool enable)
   {
      cakeBgElem.gameObject.SetActive(enable);
   }
   
}
