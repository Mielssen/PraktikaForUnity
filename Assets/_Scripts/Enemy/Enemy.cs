using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Enemy : MonoBehaviour
{
    public int health = 50;
    [SerializeField] private float movespeed = 2f;

    private Rigidbody2D rb;

    private Transform checkpoint;
    [NonSerialized]public int index = 0;
    [NonSerialized] public float distance = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        checkpoint = EnemyManager.main.checkpoints[index];
    }

    private void Update()
    {
        checkpoint = EnemyManager.main.checkpoints[index];
        distance = Vector2.Distance(transform.position, EnemyManager.main.checkpoints[index].position);
        if (Vector2.Distance(checkpoint.transform.position, rb.transform.position) <= 0.1f)
        {
            index++;
            if(index >= EnemyManager.main.checkpoints.Length)
            {
                Destroy(gameObject);
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.linearVelocity = direction * movespeed;
    }

    public void damage(int damage)
    {
        health -= damage; 
    }
}
