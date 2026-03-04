using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;

    public Transform spawnpoint;

    public Transform[] checkpoints;

    [SerializeField] private GameObject Default;
    [SerializeField] private GameObject Fast;
    [SerializeField] private GameObject Tank;

    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyCount = 6;
    [SerializeField] private float EnemyCountRate = 0.2f;
    [SerializeField] private float SpawnDelayMax = 1f;
    [SerializeField] private float SpawnDelayMin = 0.75f;

    [SerializeField] private float DefaultRate = 0.5f;
    [SerializeField] private float FastRate = 0.4f;
    [SerializeField] private float TankRate = 0.1f;

    [SerializeField] private GameObject wavePanel;

    private bool wavedone = false;
    private bool waveover = false;
    private List<GameObject> wavest = new List<GameObject>();
    private int enemyLeft;

    private int DefaultCount;
    private int FastCount;
    private int TankCount;
    void Awake()
    {
        main = this;
    }

    void Start()
    {
        SetWave();   
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (!waveover && wavedone && enemies.Length == 0)
        {
            Player.main.money += 50 + (10 * wave);
            waveover = true;
            wavePanel.SetActive(true);
        }

    }

    private void SetWave()
    {
        DefaultCount = Mathf.RoundToInt(enemyCount * (DefaultRate + TankRate));
        FastCount = Mathf.RoundToInt(enemyCount * FastRate);
        TankCount = 0;

        if (wave % 5 == 0)
        {
            DefaultCount = Mathf.RoundToInt(enemyCount * DefaultRate);
            TankCount = Mathf.RoundToInt(enemyCount * TankRate);
        }

        enemyLeft = DefaultCount + FastCount + TankCount;
        enemyCount = enemyLeft;

        wavest = new List<GameObject>();

        for (int i = 0; i < DefaultCount; i++)
        {
            wavest.Add(Default);
        }
        for (int i = 0; i < FastCount; i++)
        {
            wavest.Add(Fast);
        }
        for (int i = 0; i < TankCount; i++)
        {
            wavest.Add(Tank);
        }
        wavest = Shuffle(wavest);

        StartCoroutine(spawn());
    }

    public List<GameObject> Shuffle(List<GameObject> waveSet) 
    {
        List<GameObject> temp = new List<GameObject>();
        List<GameObject> result = new List<GameObject>();
        temp.AddRange(waveSet);
        for (int i = 0; i < waveSet.Count; i++)
        {
            int index = Random.Range(0, temp.Count - 1);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        return result;
    }

    public void NextWave()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        wavePanel.SetActive(false);

        if (wavedone && enemies.Length == 0)
        {
            wave++;
            wavedone = false;
            waveover = false;
            enemyCount += Mathf.RoundToInt(enemyCount * EnemyCountRate);
            SetWave();
        }
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < wavest.Count; i++)
        {
            GameObject enemyObj = Instantiate(wavest[i], spawnpoint.position, Quaternion.identity);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (wavest[i] == Default) enemy.health = 50;
            else if (wavest[i] == Fast) enemy.health = 30;
            else if (wavest[i] == Tank) enemy.health = 100;
            yield return new WaitForSeconds(Random.Range(SpawnDelayMin, SpawnDelayMax));
        }
        wavedone = true;
    }
}
