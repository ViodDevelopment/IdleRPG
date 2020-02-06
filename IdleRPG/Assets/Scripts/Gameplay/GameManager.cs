using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ally, enemy, abilityPrefab, canvas, instance;

    public int nAllies;

    public AllyController[] allies;
    public EnemyController[] enemies;
    public GameObject[] abilities;


    public List<Vector3> enemiesSpawnPos;

    #region CANVAS_POSITIONS_ABILITIES
        float initialPaddingRight = 300f;
        float initialPaddingUp = 10f;
        float abilityWidth = 95f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {


        SpawnAllies();
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
        foreach (EnemyController en in enemies)
            Destroy(en.gameObject);

        for(int i = 0; i < _n; i++)
        {
            instance = Instantiate(enemy);
            instance.transform.position = _initPos + _relPositions[i];
        }

        enemies = FindObjectsOfType<EnemyController>();

        foreach (EnemyController en in enemies)
        {
            en.Initialize();
        }

        instance = null;
    }

    public bool NoMoreEnemies()
    {
        bool l_noMoreEnemies = true;
        foreach(EnemyController en in enemies)
        {
           if(l_noMoreEnemies = (!en.gameObject.activeSelf && l_noMoreEnemies)) { }
        }
        return l_noMoreEnemies;
    }

    public void RespawnEnemies()
    {
        if(NoMoreEnemies())
        {
            SpawnEnemies(enemiesSpawnPos.Count, Vector3.forward * 100, enemiesSpawnPos);
        }
        else
        {
            Debug.Log("There are enemies left");
        }
    }

    public void SpawnAllies()
    {
        abilities = new GameObject[nAllies];

        for (int i = 0; i < nAllies; i++)
        {
            instance = Instantiate(ally);
            instance.transform.position = Vector3.right * i * 1.2f;
            allies = FindObjectsOfType<AllyController>();

            instance = Instantiate(abilityPrefab);
            instance.transform.SetParent(canvas.transform);

            instance.transform.position = Vector3.right * i * abilityWidth + Vector3.right * initialPaddingRight + Vector3.up * initialPaddingUp;
            instance.GetComponent<Button>().onClick.AddListener(allies[i].SpecialAbility);

        }

        foreach (AllyController al in allies)
        {
            al.Initialize();
        }

        instance = null;
    }

    //private void OnDrawGizmos()
    //{
    //    foreach(Vector3 pos in enemiesSpawnPos)
    //        Gizmos.DrawCube(pos, Vector3.one);
    //}
}
