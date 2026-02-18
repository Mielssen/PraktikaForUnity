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
    }
    [SerializeField] private Level[] levels = new Level[3];
    [NonSerialized] public int currentlevel = 0;
    [NonSerialized] public string currentCost;

    private Tower tower;
    [SerializeField] private TowerRange towerRange;
    void Awake()
    {
        tower = GetComponent<Tower>();
        currentCost = levels[0].cost.ToString();
    }

    public void Upgrade()
    {
        if (currentlevel < levels.Length && levels[currentlevel].cost <= Player.main.money)
        {
            tower.range = levels[currentlevel].range;
            tower.damage = levels[currentlevel].damage;
            tower.fireRate = levels[currentlevel].fireRate;
            towerRange.UpdateRange();

            Player.main.money -= levels[currentlevel].cost;
            currentlevel++;

            if(currentlevel >= levels.Length)
            {
                currentCost = "MAX";
            }
            else
            {
                currentCost = levels[currentlevel].cost.ToString();
            }

        }
    }
}
