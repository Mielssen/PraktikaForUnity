using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    [SerializeField] private float movespeed = 2f;
    [SerializeField] private int value = 10;

    private float originalSpeed;
    private Rigidbody2D rb;
    private Transform checkpoint;

    public int index = 0;
    [NonSerialized] public float distance = 0;

    private Animator[] anims;
    private SpriteRenderer[] srs;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anims = GetComponentsInChildren<Animator>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        originalSpeed = movespeed;
    }

    private void OnEnable()
    {
        index = 0;
        movespeed = originalSpeed;
        foreach (var anim in anims) anim.speed = 1;
        foreach (var sr in srs) sr.color = Color.white;

        if (EnemyManager.main != null && EnemyManager.main.checkpoints.Length > 0)
        {
            checkpoint = EnemyManager.main.checkpoints[index];
        }

        AbilityManager.OnFreezeEnemies += Freeze;
        AbilityManager.OnAcidRain += StartAcidRain;
    }

    private void OnDisable()
    {
        AbilityManager.OnFreezeEnemies -= Freeze;
        AbilityManager.OnAcidRain -= StartAcidRain;

        StopAllCoroutines();
    }

    private void Update()
    {
        if (index >= EnemyManager.main.checkpoints.Length) return;

        checkpoint = EnemyManager.main.checkpoints[index];
        distance = Vector2.Distance(transform.position, checkpoint.position);

        if (Vector2.Distance(checkpoint.position, transform.position) <= 0.1f)
        {
            index++;
            if (index >= EnemyManager.main.checkpoints.Length)
            {
                Player.main.damage(health);
                gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (index >= EnemyManager.main.checkpoints.Length) return;

        Vector2 direction = (checkpoint.position - transform.position).normalized;
        rb.linearVelocity = direction * movespeed;

        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SoundManager.instance.PlaySFX(SoundManager.instance.enemyDeath);
        Player.main.AddMoney(value);
        gameObject.SetActive(false);
    }

    private void Freeze()
    {
        StartCoroutine(FreezeRoutine());
    }

    IEnumerator FreezeRoutine()
    {
        movespeed = 0;
        foreach (var anim in anims) anim.speed = 0;
        foreach (var sr in srs) sr.color = Color.cyan;

        yield return new WaitForSeconds(5f);

        movespeed = originalSpeed;
        foreach (var anim in anims) anim.speed = 1;
        foreach (var sr in srs) sr.color = Color.white;
    }

    private void StartAcidRain()
    {
        StartCoroutine(AcidRoutine());
    }

    IEnumerator AcidRoutine()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (var sr in srs) sr.color = Color.green;
            health -= 10;

            if (health <= 0)
            {
                Die();
                yield break;
            }

            yield return new WaitForSeconds(1f);
            foreach (var sr in srs) sr.color = Color.white;
        }
    }
}