using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class utils : MonoBehaviour
{
    public static bool isvalidhex(string hex)
    {
        return Regex.IsMatch(hex, "^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})$");
    }

    public static Color HexToRGBVector3(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }
        Color color = ColorUtility.TryParseHtmlString("#" + hex, out color) ? color : Color.black;
        return color;
    }

    public static IEnumerator fadeinandout(GameObject gameobject,float duration,int e = 0)
    {
        bool hide = false;
        if (!gameobject.activeSelf)
        {
            hide = true;
            gameobject.SetActive(true);
        }
        if (gameobject.GetComponent<Text>() != null)
        {
            Text textComponent = gameobject.GetComponent<Text>();
            yield return FadeInAndOutText(textComponent, duration);
        }
        else if (gameobject.GetComponent<RawImage>() != null)
        {
            RawImage rawImageComponent = gameobject.GetComponent<RawImage>();
            yield return FadeInAndOutRawImage(rawImageComponent, duration);
        }
        else if (gameobject.GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer spriteRendererComponent = gameobject.GetComponent<SpriteRenderer>();
            yield return FadeInAndOutSpriteRenderer(spriteRendererComponent, duration);
        }
        if (hide)
        {
            gameobject.SetActive(false);
            hide = false;
        }
        switch (e)
        {
            case 1:
                globalvars.isinfoshowingendless = false;
                break;
            default:
                break;
        }
    }

    private static IEnumerator FadeInAndOutText(Text text, float duration)
    {
        Color originalColor = text.color;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(0f, 1f, progress);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(1f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(1f, 0f, progress);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private static IEnumerator FadeInAndOutRawImage(RawImage rawImage, float duration)
    {
        Color originalColor = rawImage.color;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(0f, 1f, progress);
            rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(1f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(1f, 0f, progress);
            rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private static IEnumerator FadeInAndOutSpriteRenderer(SpriteRenderer spriteRenderer, float duration)
    {
        Color originalColor = spriteRenderer.color;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(0f, 1f, progress);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(1f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(1f, 0f, progress);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    public static IEnumerator FadeIn(RawImage transition, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(0f, 1f, progress);

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, alpha);

            yield return null;
        }

        transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1f);
    }

    public static IEnumerator FadeOut(RawImage transition, float duration)
    {
        if (transition.color.a == 0)
        {
            transition.color = new Color(transition.color.r,transition.color.g,transition.color.b,1);
        }
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(1f, 0f, progress);

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, alpha);

            yield return null;
        }

        transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 0f);
    }


    public static IEnumerator smooth_transition(RawImage transition,float delay = 0.5f)
    {

        transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1f);

        yield return new WaitForSeconds(delay);

        float startTime = Time.time;
        float endTime = startTime + 0.5f;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / 0.5f;
            float alpha = Mathf.Lerp(1f, 0f, progress);

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, alpha);

            yield return null;
        }

        transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 0f);
    }

    public static IEnumerator send_notification(string message, Text messagetext)
    {
        float duration = 0.5f;
        float startTime = Time.time;
        float endTime = startTime + duration;

        messagetext.text = message;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(0f, 1f, progress);

            messagetext.color = new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, alpha);

            yield return null;
        }

        messagetext.color = new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, 1f);

        yield return new WaitForSeconds(1.3f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(1f, 0f, progress);

            messagetext.color = new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, alpha);

            yield return null;
        }
        messagetext.color = new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, 0f);
    }
}
