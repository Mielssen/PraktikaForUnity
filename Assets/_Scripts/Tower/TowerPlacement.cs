using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    [NonSerialized] public bool isPlacing = true;
    //private bool isRectricted = false;
    private int restrictedCount = 0;
    void Awake()
    {
        rangeCollider.enabled = false;
    }

    void Update()
    {
        if (isPlacing)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = mousePosition;
        }
        //if (Input.GetMouseButtonDown(1) && !isRectricted) 
        if (Input.GetMouseButtonDown(1) && restrictedCount == 0)
        {
            rangeCollider.enabled = true;
            isPlacing = false;
            GetComponent<TowerPlacement>().enabled = false;
        }
        bool isRectricted = restrictedCount > 0;
        if (isRectricted)
        { 
            rangeSprite.color = red;
        }
        else
        {
            rangeSprite.color = gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower") && isPlacing)
        {
            //isRectricted = true;
            restrictedCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Restricted" || collision.gameObject.tag == "Tower") && isPlacing)
        {
            //isRectricted = false;
            restrictedCount--;
        }
    }
}
