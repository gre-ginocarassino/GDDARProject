using System.Collections;
using UnityEngine;


public class FadeUI
{
    public static FadeUI instance;
    private static float fadeSpeed = 3f;
    private static float currentValue;
    public static bool routineIsActive = false;
    public float fadeDuration = 1f;
    private void Awake()
    {
        instance = this;
    }
   
    
    public static IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup, float fadeDuration)
    {
        routineIsActive = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
        routineIsActive = false;
    }
}


    

