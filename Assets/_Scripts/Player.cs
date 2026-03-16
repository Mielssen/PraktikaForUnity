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

    [SerializeField] private TextMeshProUGUI MoneyGUI;
    [SerializeField] private TextMeshProUGUI DarkMatterGUI;
    [SerializeField] private GameObject gameOverGUI;

    void Awake()
    {
        main = this;
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
        MoneyGUI.text = ": " + money.ToString();
        DarkMatterGUI.text = ": " + darkmatter.ToString();
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

        if (lives < hearts.Length)
        {
            hearts[lives].gameObject.SetActive(false);
        }

        if (lives <= 0)
        {
            gameOverGUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void AddLife()
    {
        if (lives < hearts.Length)
        {
            hearts[lives].gameObject.SetActive(true);
            lives++;
        }
    }

    public void restart()
    {
        Time.timeScale = 1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}