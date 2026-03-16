using UnityEngine;
using System;
using System.Collections;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager main;

    public static event Action OnFreezeEnemies;
    public static event Action OnTowerBoost;
    public static event Action OnAcidRain;
    public static event Action OnExtraLife;
    public static event Action OnMoneyBuffStart;
    public static event Action OnMoneyBuffEnd;

    private void Awake()
    {
        if (main == null) main = this;
    }
    public void TriggerFreeze() => OnFreezeEnemies?.Invoke();

    public void TriggerTowerBoost() => OnTowerBoost?.Invoke();

    public void TriggerAcidRain() => OnAcidRain?.Invoke();

    public void TriggerExtraLife() => OnExtraLife?.Invoke();

    public void TriggerMoneyBuff(float duration = 10f)
    {
        StartCoroutine(MoneyBuffRoutine(duration));
    }

    private IEnumerator MoneyBuffRoutine(float duration)
    {
        OnMoneyBuffStart?.Invoke();
        yield return new WaitForSeconds(duration);
        OnMoneyBuffEnd?.Invoke();
    }
}