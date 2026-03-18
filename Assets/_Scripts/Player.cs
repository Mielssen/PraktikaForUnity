using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player main;

    [Header("Lives")]
    [SerializeField] private int lives = 3;
    [SerializeField] private Image[] hearts;

    [Header("Economy")]
    public int money = 0;
    public int darkmatter = 0;
    private int moneyMultiplier = 1;

    [Header("UI Displays")]
    [SerializeField] private TextMeshProUGUI MoneyGUI;
    [SerializeField] private TextMeshProUGUI DarkMatterGUI;

    [Header("Status Panels")]
    [SerializeField] private GameObject gameOverGUI;
    [SerializeField] private GameObject winGUI;
    [SerializeField] private string nextSceneName;

    void Awake()
    {
        main = this;
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        AbilityManager.OnExtraLife += AddLife;
        AbilityManager.OnMoneyBuffStart += () => moneyMultiplier = 2;
        AbilityManager.OnMoneyBuffEnd += () => moneyMultiplier = 1;
    }

    private void OnDisable()
    {
        AbilityManager.OnExtraLife -= AddLife;
        AbilityManager.OnMoneyBuffStart -= () => moneyMultiplier = 2;
        AbilityManager.OnMoneyBuffEnd -= () => moneyMultiplier = 1;
    }

    void Update()
    {
        if (MoneyGUI != null) MoneyGUI.text = ": " + money.ToString();
        if (DarkMatterGUI != null) DarkMatterGUI.text = ": " + darkmatter.ToString();

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            ResetProgress();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount * moneyMultiplier;
    }

    public void AddDarkMatter(int amount)
    {
        darkmatter += amount;
        if (darkmatter < 0) darkmatter = 0;
    }

    public void damage(int damageAmount)
    {
        if (lives <= 0) return;
        lives--;
        if (lives < hearts.Length && hearts[lives] != null)
        {
            hearts[lives].gameObject.SetActive(false);
        }
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void AddLife()
    {
        if (lives < hearts.Length)
        {
            if (hearts[lives] != null) hearts[lives].gameObject.SetActive(true);
            lives++;
        }
    }

    private void GameOver()
    {
        if (gameOverGUI != null) gameOverGUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinLevel(int currentLevelNumber)
    {
        if (winGUI != null) winGUI.SetActive(true);
        Time.timeScale = 0;

        int nextLevel = currentLevelNumber + 1;
        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

        if (nextLevel > reachedLevel)
        {
            PlayerPrefs.SetInt("ReachedLevel", nextLevel);
            PlayerPrefs.Save();
        }
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name not specified!");
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    [ContextMenu("Reset Progress")]
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progress has been reset!");

        if (Application.isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}