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

    [SerializeField] private LayerMask towerLayer;

    private GameObject selectedTower;
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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f, towerLayer);

            if (hit.collider != null)
            {
                GameObject range1 = selectedTower.transform.GetChild(1).gameObject;
                range1.GetComponent<SpriteRenderer>().enabled = false;
            }
            selectedTower = hit.collider.gameObject;
            GameObject range2 = selectedTower.transform.GetChild(1).gameObject;
            range2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.U) && selectedTower) 
        {
            selectedTower.GetComponent<TowerUpgrades>().Upgrade(); 
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
