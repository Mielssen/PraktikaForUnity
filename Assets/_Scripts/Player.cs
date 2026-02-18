using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
public class Player: MonoBehaviour
{
    public static Player main;
    [SerializeField] private int health = 500;
    public int money = 0;

    [SerializeField] private TextMeshProUGUI HPGui;
    [SerializeField] private TextMeshProUGUI MoneyGUI;
    void Start()
    {
        main = this; 
    }
    void Update()
    {
        HPGui.text = "HP:" + health.ToString();
        MoneyGUI.text = "Money:" + money.ToString();
    }
}
