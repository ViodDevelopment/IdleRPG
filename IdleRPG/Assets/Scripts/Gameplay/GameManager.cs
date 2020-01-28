using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ally, enemy, instance;

    public AllyController[] allies;
    public EnemyController[] enemies;


    public List<Vector3> enemiesSpawnPos;


    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < 4; i++)
        {
            instance = Instantiate(ally);
            instance.transform.position = Vector3.right * i * 1.2f;
        }

        allies = FindObjectsOfType<AllyController>();

        SpawnEnemies(enemiesSpawnPos.Count, Vector3.forward * 100, enemiesSpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AllyController al in allies)
        {
            al.UpdateAlly();
        }

        foreach(EnemyController en in enemies)
        {
            en.UpdateEnemy();
        }
    }

    void SpawnEnemies(int _n, Vector3 _initPos, List<Vector3> _relPositions)
    {

        for(int i = 0; i < _n; i++)
        {
            instance = Instantiate(enemy);
            instance.transform.position = _initPos + _relPositions[i];
        }

        enemies = FindObjectsOfType<EnemyController>();
    }

    //private void OnDrawGizmos()
    //{
    //    foreach(Vector3 pos in enemiesSpawnPos)
    //        Gizmos.DrawCube(pos, Vector3.one);
    //}
}
