using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField] private Image phoneScreenBg = null;

   public void ChangeBgColor(Color bgColor)
   {
      bgColor.a = 0;
      phoneScreenBg.color = bgColor;
      phoneScreenBg.DOFade(1.0f, 1.00f);
   }
}
