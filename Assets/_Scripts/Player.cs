using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player: MonoBehaviour
{
    public static Player main;

    [Header("Lives")]
    [SerializeField] private int lives = 3;
    [SerializeField] private Image[] hearts;

    public int money = 0;

    [SerializeField] private TextMeshProUGUI MoneyGUI;

    [SerializeField] private GameObject gameOverGUI;
    void Start()
    {
        main = this; 
    }
    void Update()
    {
        MoneyGUI.text = ": " + money.ToString();
    }

    public void damage(int damage)
    {
        if (lives <= 0) return;

        lives--;

        hearts[lives].gameObject.SetActive(false);

        if (lives <= 0)
        {
            gameOverGUI.SetActive(true);
        }

    }

    public void restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
