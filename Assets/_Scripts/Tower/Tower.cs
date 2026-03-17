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

    public GameObject obj;
    public SpriteRenderer sprite;
    public SpriteRenderer rangeSprite;

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

    
    private float originalFireRate;
    private bool isBoosted = false;

    void Awake()
    {
        
        originalFireRate = fireRate;
    }

    private void OnEnable()
    {
        
        AbilityManager.OnTowerBoost += StartTowerBoost;
    }

    private void OnDisable()
    {
        
        AbilityManager.OnTowerBoost -= StartTowerBoost;
    }

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
            obj.transform.right = target.transform.position - obj.transform.position;

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

    
    private void StartTowerBoost()
    {
        if (!isBoosted)
        {
            StartCoroutine(TowerBoostRoutine());
        }
    }

    IEnumerator TowerBoostRoutine()
    {
        isBoosted = true;

        
        fireRate = originalFireRate / 2f;

        
        if (sprite != null) sprite.color = new Color(1f, 0.8f, 0.4f);

        yield return new WaitForSeconds(5f); 

        
        fireRate = originalFireRate;
        if (sprite != null) sprite.color = Color.white;
        isBoosted = false;
    }

    public void Select(bool select)
    {
        rangeSprite.enabled = select;
    }

    void SingleAttack()
    {
        
        target.GetComponent<Enemy>().damage(damage);
        SoundManager.instance.PlaySFX(SoundManager.instance.towerShoot);
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
            Gizmos.DrawWireSphere(obj.transform.position, splashRadius);
        }
    }
#endif
}
