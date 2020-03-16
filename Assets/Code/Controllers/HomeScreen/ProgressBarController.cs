using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform  = default;
    
    [SerializeField] private float minBarValue = 0.0f;
    [SerializeField] private float maxBarValue = 330.0f;
    [SerializeField] private float speedOfAnimating = 3.0f;

    private bool isAnimating = false;

    public void SetBarProcent(float procent)
    {
        float barValue = Mathf.Lerp(maxBarValue, minBarValue, procent);
        StartCoroutine(AnimateMetter(rectTransform.offsetMax.x, -barValue));
    }
    
    private IEnumerator AnimateMetter(float start, float end)
    {
        if (isAnimating)
        {
            yield break;
        }

        isAnimating = true;
        if (start < end)
        {
            for (float progress = end; progress > start; progress -= speedOfAnimating)
            {
                rectTransform.offsetMax = new Vector2(progress, 0);
                yield return null;

            }
        }
        else
        {
            for (float progress = start; progress > end; progress -= speedOfAnimating)
            {
                rectTransform.offsetMax = new Vector2(progress, 0);
                yield return null;

            }
        }
        isAnimating = false;
        rectTransform.offsetMax = new Vector2(end, 0);
    }
}
