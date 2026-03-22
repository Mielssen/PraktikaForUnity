using UnityEngine;
using System.Collections;

public class EasterEggPhoto : MonoBehaviour
{
    public CanvasGroup photoWindow;   // Панель із фото
    public float displayTime = 3f;    // Скільки секунд показувати фото
    public float fadeDuration = 1f;   // Час затухання

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
                StartCoroutine(ShowPhotoAndFade());
                sClickCount = 0;
            }
        }
    }

    IEnumerator ShowPhotoAndFade()
    {
        // 1. Показуємо фото
        photoWindow.alpha = 1f;

        // 2. Чекаємо заданий час
        yield return new WaitForSeconds(displayTime);

        // 3. Плавне затухання
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            photoWindow.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        photoWindow.alpha = 0f;
    }
}