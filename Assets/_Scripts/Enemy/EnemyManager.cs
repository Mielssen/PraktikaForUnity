using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;

    [Header("Spawn Settings")]
    public Transform spawnpoint;
    public Transform[] checkpoints;
    [SerializeField] private GameObject wavePanel;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject Default;
    [SerializeField] private GameObject Fast;
    [SerializeField] private GameObject Tank;

    [Header("Wave Stats")]
    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyCount = 6;
    [SerializeField] private float EnemyCountRate = 0.2f;
    [SerializeField] private float SpawnDelayMax = 1f;
    [SerializeField] private float SpawnDelayMin = 0.75f;

    [Header("Spawn Rates")]
    [SerializeField] private float DefaultRate = 0.5f;
    [SerializeField] private float FastRate = 0.4f;
    [SerializeField] private float TankRate = 0.1f;

    [Header("Level Settings")]
    [SerializeField] private int currentLevelNumber = 1;
    [SerializeField] private int maxWaves = 20; 

    private bool wavedone = false;
    private bool waveover = false;
    private List<GameObject> wavest = new List<GameObject>();

    void Awake() => main = this;

    void Start() => Invoke("SetWave", 0.1f);

    void Update()
    {
        if (waveover || !wavedone) return;

        int activeEnemies = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var e in enemies) if (e.activeInHierarchy) activeEnemies++;

        if (activeEnemies == 0)
        {
            EndWave();
        }
    }

    private void SetWave()
    {
        wavest.Clear();

        int dCount = Mathf.RoundToInt(enemyCount * (DefaultRate + (wave % 5 != 0 ? TankRate : 0)));
        int fCount = Mathf.RoundToInt(enemyCount * FastRate);
        int tCount = (wave % 5 == 0) ? Mathf.RoundToInt(enemyCount * TankRate) : 0;

        for (int i = 0; i < dCount; i++) wavest.Add(Default);
        for (int i = 0; i < fCount; i++) wavest.Add(Fast);
        for (int i = 0; i < tCount; i++) wavest.Add(Tank);

        wavest = Shuffle(wavest);
        StartCoroutine(spawn());
    }

    private void EndWave()
    {

        Player.main.money += 50 + (10 * wave);
        if (wave % 3 == 0) Player.main.AddDarkMatter(30);

        if (wave >= maxWaves)
        {
            Player.main.WinLevel(currentLevelNumber);
            waveover = true; 
            return;
        }

        waveover = true;
        if (wavePanel != null) wavePanel.SetActive(true);
    }

    public void NextWave()
    {
        wave++;
        wavedone = false;
        waveover = false;
        if (wavePanel != null) wavePanel.SetActive(false);
        enemyCount += Mathf.RoundToInt(enemyCount * EnemyCountRate);
        SetWave();
    }

    IEnumerator spawn()
    {
        foreach (GameObject prefab in wavest)
        {
            if (prefab == null || EnemyPool.Instance == null) continue;

            GameObject enemyObj = EnemyPool.Instance.GetEnemy(prefab);
            if (enemyObj != null)
            {
                enemyObj.transform.position = spawnpoint.position;
                enemyObj.transform.rotation = Quaternion.identity;

                Enemy enemyScript = enemyObj.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.index = 0;
                    if (prefab == Default) enemyScript.health = 50;
                    else if (prefab == Fast) enemyScript.health = 30;
                    else if (prefab == Tank) enemyScript.health = 100;
                }
                enemyObj.SetActive(true);
            }
            yield return new WaitForSeconds(Random.Range(SpawnDelayMin, SpawnDelayMax));
        }
        wavedone = true;
    }

    private List<GameObject> Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}