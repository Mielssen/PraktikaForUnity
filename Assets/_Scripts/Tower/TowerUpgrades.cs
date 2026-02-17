using System;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    class Level 
    {
        public float range = 8f;
        public int damage = 25;
        public float fireRate = 1f;
    }
    [SerializeField] private Level[] levels = new Level[3];
    [NonSerialized] public int currentlevel = 0;

    private Tower tower;
    [SerializeField] private TowerRange towerRange;
    void Awake()
    {

        tower = GetComponent<Tower>();
    }

    // Update is called once per frame
    public void Upgrade()
    {
        if (currentlevel < levels.Length)
        {
            tower.range = levels[currentlevel].range;
            tower.damage = levels[currentlevel].damage;
            tower.fireRate = levels[currentlevel].fireRate;
            towerRange.UpdateRange();

            currentlevel++;

            Debug.Log("Upgraded");
        }
    }
}
