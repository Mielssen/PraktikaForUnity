using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Costs")]
    public int freezeCost = 20;
    public int acidCost = 30;
    public int towerBoostCost = 25;
    public int lifeCost = 50;
    public int moneyBuffCost = 40;

    public void ToggleShop()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void BuyFreeze()
    {
        if (Player.main.darkmatter >= freezeCost)
        {
            Player.main.AddDarkMatter(-freezeCost);
            AbilityManager.main.TriggerFreeze();
        }
    }

    public void BuyAcid()
    {
        if (Player.main.darkmatter >= acidCost)
        {
            Player.main.AddDarkMatter(-acidCost);
            AbilityManager.main.TriggerAcidRain();
        }
    }

    public void BuyTowerBoost()
    {
        if (Player.main.darkmatter >= towerBoostCost)
        {
            Player.main.AddDarkMatter(-towerBoostCost);
            AbilityManager.main.TriggerTowerBoost();
        }
    }

    public void BuyLife()
    {
        if (Player.main.darkmatter >= lifeCost)
        {
            Player.main.AddDarkMatter(-lifeCost);
            AbilityManager.main.TriggerExtraLife();
        }
    }

    public void BuyMoneyBuff()
    {
        if (Player.main.darkmatter >= moneyBuffCost)
        {
            Player.main.AddDarkMatter(-moneyBuffCost);
            AbilityManager.main.TriggerMoneyBuff(10f);
        }
    }
}