using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class TowerManager : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject singleTower;
    [SerializeField] private GameObject sniperTower;
    [SerializeField] private GameObject splashTower;

    private GameObject placingTower;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setTower(singleTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setTower(sniperTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setTower(splashTower);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearSelected();
        }

        if (placingTower)
        {
            if (!placingTower.GetComponent<TowerPlacement>().isPlacing)
            {
                placingTower = null;
            }
        }
    }

    private void ClearSelected()
    {
        if (placingTower)
        {
            Destroy(placingTower);
            placingTower = null;
        }
    }

    private void setTower(GameObject tower)
    {
        ClearSelected();
        placingTower = Instantiate(tower);
    }
}
