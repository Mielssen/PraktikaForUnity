using UnityEngine;
using UnityEngine.Video; // ќбов'€зково додай це
using System.Collections;

public class EasterEggHandler : MonoBehaviour
{
    public CanvasGroup easterEggWindow;
    public VideoPlayer videoPlayer; // ѕерет€гни сюди св≥й Video Player
    public float fadeDuration = 1f;

    private int sClickCount = 0;
    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            float currentTime = Time.time;
            if (currentTime - lastClickTime > doubleClickThreshold) sClickCount = 0;

            sClickCount++;
            lastClickTime = currentTime;

            if (sClickCount == 2)
            {
                StopAllCoroutines();
                StartCoroutine(PlayVideoAndFade());
                sClickCount = 0;
            }
        }
    }

    IEnumerator PlayVideoAndFade()
    {
        // 1. ¬микаЇмо в≥кно та запускаЇмо в≥део
        easterEggWindow.alpha = 1f;

        if (videoPlayer != null)
        {
            videoPlayer.Play();

            // 2. „екаЇмо, поки в≥део зак≥нчитьс€ (або заданий час)
            // якщо хочеш чекати саме к≥нц€ в≥део:
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(2f); // «апасний вар≥ант, €кщо в≥део немаЇ
        }

        // 3. «атуханн€
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            easterEggWindow.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        easterEggWindow.alpha = 0f;
        if (videoPlayer != null) videoPlayer.Stop();
    }
}