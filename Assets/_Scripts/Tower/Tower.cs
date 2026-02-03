using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 8f;
    public int damage = 25;
    public float fireRate = 1f;

    [Header("Targeting Mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;

    [NonSerialized]
    public GameObject target;
    private float cooldown = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (target) 
        {
            if (cooldown >= fireRate)
            {
                transform.right = target.transform.position - transform.position;

                target.GetComponent<Enemy>().damage(damage);
                cooldown = 0f;
            }
            else 
            {
                cooldown += 1 * Time.deltaTime;
            }
        }
    }
}
