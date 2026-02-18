using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TowerType
    {
        Single,
        Sniper,
        Splash
    }
    [Header("Tower Type")]
    public TowerType type;

    [Header("Tower Stats")]
    public float range = 8f;
    public int damage = 25;
    public float fireRate = 1f;
    public int cost = 50;

    [Header("Splash Settings")]
    public float splashRadius = 2f;
    [Range(0f, 1f)] public float splashDamageMultiplier = 0.6f;

    [Header("Targeting Mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;

    [NonSerialized]
    public GameObject target;
    private float cooldown = 0f;

    [Header("Effects")]
    [SerializeField] GameObject fireEffect;



    void Start()
    {
        if (fireEffect)
            fireEffect.SetActive(false);
    }

    void Update()
    {
        if (!target) return;

        if (cooldown >= fireRate)
        {
            transform.right = target.transform.position - transform.position;

            switch (type)
            {
                case TowerType.Single:
                case TowerType.Sniper:
                    SingleAttack();
                    break;

                case TowerType.Splash:
                    SplashAttack();
                    break;
            }

            cooldown = 0f;
            if (fireEffect)
                StartCoroutine(FireEffect());
        }
        else
        {
            cooldown += Time.deltaTime;
        }
    }

    void SingleAttack()
    {
        target.GetComponent<Enemy>().damage(damage);
    }

    void SplashAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            target.transform.position,
            splashRadius
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>()
                   .damage(Mathf.RoundToInt(damage * splashDamageMultiplier));
            }
        }
    }

    IEnumerator FireEffect()
    {
        fireEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        fireEffect.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (type == TowerType.Splash)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, splashRadius);
        }
    }
#endif
}
