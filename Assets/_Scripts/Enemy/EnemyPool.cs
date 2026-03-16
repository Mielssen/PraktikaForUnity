using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    private List<GameObject> pooledEnemies = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public GameObject GetEnemy(GameObject prefab)
    {
        if (prefab == null) return null;

        foreach (GameObject enemy in pooledEnemies)
        {
            if (!enemy.activeInHierarchy && enemy.name == prefab.name)
            {
                return enemy;
            }
        }

        GameObject newObj = Instantiate(prefab);
        newObj.name = prefab.name;
        newObj.SetActive(false);
        pooledEnemies.Add(newObj);

        return newObj;
    }
}