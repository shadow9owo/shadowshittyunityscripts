using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class utils : MonoBehaviour
{

    public static string inttodiffstring(int e)
    {
        switch (e)
        {
            case 0:
                return "none";
            case 1:
                return "normal";
            case 2:
                return "hard";
            default:
                break;
        }
        return "";
    }

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
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
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

    public static IEnumerator FadeOutText(Text text, float duration)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        Color originalColor = text.color;

        double startTime = Time.time;
        double endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = 1m - progress;
            double alpha = (double)alphaDecimal;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, (float)alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    public static IEnumerator FadeInText(Text text, float duration)
    {
        Color originalColor = text.color;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0f, 1f, progress));
            text.color = newColor;
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    private static IEnumerator FadeInAndOutText(Text text, float duration)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        Color originalColor = text.color;

        double startTime = Time.time;
        double endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = progress;
            double alpha = (double)alphaDecimal;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, (float)alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(1f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = 1m - progress;
            float alpha = (float)alphaDecimal;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private static IEnumerator FadeInAndOutRawImage(RawImage rawImage, float duration)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        Color originalColor = rawImage.color;

        double startTime = Time.time;
        double endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = progress;
            double alpha = (double)alphaDecimal;
            rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, (float)alpha);
            yield return null;
        }

        rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(1f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = 1m - progress;
            double alpha = (double)alphaDecimal;
            rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, (float)alpha);
            yield return null;
        }

        rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private static IEnumerator FadeInAndOutSpriteRenderer(SpriteRenderer spriteRenderer, float duration)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        Color originalColor = spriteRenderer.color;

        double startTime = Time.time;
        double endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = progress;
            double alpha = (float)alphaDecimal;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, (float)alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(1f);

        startTime = Time.time;
        endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = 1m - progress;
            double alpha = (double)alphaDecimal;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, (float)alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    public static IEnumerator FadeIn(RawImage transition, float duration)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        double startTime = Time.time;
        double endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;

            decimal alphaDecimal = progress;
            double alpha = (double)alphaDecimal;

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, (float)alpha);

            yield return null;
        }

        transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1f);
    }

    public static IEnumerator FadeOut(RawImage transition, float duration)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        if (transition.color.a == 0)
        {
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1);
        }
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = 1 - progress;
            double alpha = (double)alphaDecimal;

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, (float)alpha);

            yield return null;
        }

        transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 0f);
    }

    public static IEnumerator send_notification(string message, Text messagetext)
    {
        if (Time.timeScale == 0)
        {
            Debug.LogError("timescale is 0");
        }
        messagetext.text = message;
        if (messagetext.color != new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, 0f))
        {
            yield break;
        }
        double duration = 0.5f;
        double startTime = Time.time;
        double endTime = startTime + duration;

        while (Time.time < endTime)
        {
            decimal progress = (decimal)(Time.time - startTime) / (decimal)duration;
            decimal alphaDecimal = progress;
            double alpha = (double)alphaDecimal;

            messagetext.color = new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, (float)alpha);

            yield return null;
        }

        messagetext.color = new Color(messagetext.color.r, messagetext.color.g, messagetext.color.b, 1f);

        yield return new WaitForSeconds(1.3f);

        yield return FadeOutText(messagetext, 0.7f);
    }
}
