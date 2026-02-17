using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private Tower tower;
    private List<GameObject> targets = new List<GameObject>();

    void Start()
    {
        transform.localScale = new Vector3(tower.range, tower.range, tower.range);
    }
    public void UpdateRange()
    {
        if (tower == null)
            tower = GetComponent<Tower>();

        transform.localScale = new Vector3(
            tower.range,
            tower.range,
            tower.range
        );
    }


    void Update()
    {
        if (targets.Count == 0)
        {
            tower.target = null;
            return;
        }

        if (tower.first)
        {
            tower.target = GetFirst();
        }
        else if (tower.last)
        {
            tower.target = GetLast();
        }
        else if (tower.strong)
        {
            tower.target = GetStrongest();
        }
        else
        {
            tower.target = targets[0];
        }
    }

    GameObject GetFirst()
    {
        GameObject best = null;
        int maxIndex = -1;
        float minDistance = Mathf.Infinity;

        foreach (var t in targets)
        {
            Enemy e = t.GetComponent<Enemy>();
            if (e.index > maxIndex || (e.index == maxIndex && e.distance < minDistance))
            {
                best = t;
                maxIndex = e.index;
                minDistance = e.distance;
            }
        }
        return best;
    }

    GameObject GetLast()
    {
        GameObject best = null;
        int minIndex = int.MaxValue;
        float maxDistance = -Mathf.Infinity;

        foreach (var t in targets)
        {
            Enemy e = t.GetComponent<Enemy>();
            if (e.index < minIndex || (e.index == minIndex && e.distance > maxDistance))
            {
                best = t;
                minIndex = e.index;
                maxDistance = e.distance;
            }
        }
        return best;
    }

    GameObject GetStrongest()
    {
        GameObject best = null;
        float maxHp = 0;

        foreach (var t in targets)
        {
            float hp = t.GetComponent<Enemy>().health;
            if (hp > maxHp)
            {
                maxHp = hp;
                best = t;
            }
        }
        return best;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            targets.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            targets.Remove(collision.gameObject);
    }
}
