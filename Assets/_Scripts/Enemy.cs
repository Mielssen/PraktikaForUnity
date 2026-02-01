using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float movespeed = 2f;

    private Rigidbody2D rb;

    private Transform checkpoint;
    private int index = 0;

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
        if (Vector2.Distance(checkpoint.transform.position, rb.transform.position) <= 0.1f)
        {
            index++;
            if(index >= EnemyManager.main.checkpoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.linearVelocity = direction * movespeed;
    }
}
