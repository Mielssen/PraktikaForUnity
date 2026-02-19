using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.EventSystems;
public class TowerManager : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject singleTower;
    [SerializeField] private GameObject sniperTower;
    [SerializeField] private GameObject splashTower;

    [SerializeField] private LayerMask towerLayer;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerLevel;
    [SerializeField] private TextMeshProUGUI UpgradeCost;
    [SerializeField] private TextMeshProUGUI towerTargetting;

    private GameObject selectedTower;
    private GameObject placingTower;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
                if (selectedTower)
                {
                    GameObject range1 = selectedTower.transform.GetChild(1).gameObject;
                    range1.GetComponent<SpriteRenderer>().enabled = false;
                }

                selectedTower = hit.collider.gameObject;
                GameObject range2 = selectedTower.transform.GetChild(1).gameObject;
                range2.GetComponent<SpriteRenderer>().enabled = true;

                panel.SetActive(true);
                towerName.text = selectedTower.name.Replace("(Clone)", "").Trim();
                towerLevel.text = "Tower LVL: " + selectedTower.GetComponent<TowerUpgrades>().currentlevel.ToString();
                UpgradeCost.text = selectedTower.GetComponent<TowerUpgrades>().currentCost;

                Tower tower = selectedTower.GetComponent<Tower>();
                if(tower.first)
                {
                    towerTargetting.text = "First";
                }
                else if (tower.last)
                {
                    towerTargetting.text = "Last";
                }
                else if (tower.strong)
                {
                    towerTargetting.text = "Strong";
                }
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && selectedTower) 
            {
                panel.SetActive(false);
                GameObject range1 = selectedTower.transform.GetChild(1).gameObject;
                range1.GetComponent<SpriteRenderer>().enabled = false;
                selectedTower = null;
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

    public void setTower(GameObject tower)
    {
        ClearSelected();
        placingTower = Instantiate(tower);
    }

    public void UpgradeSelected()
    {
        if (selectedTower)
        {
            selectedTower.GetComponent<TowerUpgrades>().Upgrade();
            towerLevel.text = "Tower LVL: " + selectedTower.GetComponent<TowerUpgrades>().currentlevel.ToString();
            UpgradeCost.text = selectedTower.GetComponent<TowerUpgrades>().currentCost;
        }
    }
    public void ChangeTargetting()
    {
        if (selectedTower)
        {
            Tower tower = selectedTower.GetComponent<Tower>();

            if (tower.first)
            {
                tower.first = false;
                tower.last = true;
                tower.strong = false;
                towerTargetting.text = "Last";
            }
            else if (tower.last)
            {
                tower.first = false;
                tower.last = false;
                tower.strong = true;
                towerTargetting.text = "Strong";
            }
            else if (tower.last)
            {
                tower.first = true;
                tower.last = false;
                tower.strong = false;
                towerTargetting.text = "First";
            }
        }
    }
}
