using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;

    public Transform[] checkpoints;

    void Awake()
    {
        main = this;
    }
}
