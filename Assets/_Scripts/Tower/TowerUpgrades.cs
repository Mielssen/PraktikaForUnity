using System;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    [System.Serializable]
    class Level 
    {
        public float range = 8f;
        public int damage = 25;
        public float fireRate = 1f;
        public int cost = 100;
        public Sprite sprite;
    }
    [SerializeField] private Level[] levels = new Level[3];
    [NonSerialized] public int currentlevel = 0;
    [NonSerialized] public string currentCost;

    private Tower tower;
    [SerializeField] private TowerRange towerRange;

    [SerializeField] private SpriteRenderer sr;

    void Awake()
    {
        tower = GetComponent<Tower>();

        if (levels.Length > 0 && levels[0].sprite != null)
        {
            sr.sprite = levels[0].sprite;
        }

        if (levels.Length > 1)
            currentCost = levels[1].cost.ToString();
        else
            currentCost = "MAX";

        tower = GetComponent<Tower>();
        currentCost = levels[0].cost.ToString();
    }

    public void Upgrade()
    {
        if (currentlevel + 1 >= levels.Length)
            return;

        int nextLevel = currentlevel + 1;

        if (levels[nextLevel].cost > Player.main.money)
            return;

        Player.main.money -= levels[nextLevel].cost;

        currentlevel = nextLevel;

        tower.range = levels[currentlevel].range;
        tower.damage = levels[currentlevel].damage;
        tower.fireRate = levels[currentlevel].fireRate;

        towerRange.UpdateRange();

        if (levels[currentlevel].sprite != null)
        {
            sr.sprite = levels[currentlevel].sprite;
        }

        if (currentlevel + 1 >= levels.Length)
            currentCost = "MAX";
        else
            currentCost = levels[currentlevel + 1].cost.ToString();
    }
}
