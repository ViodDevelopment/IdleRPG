﻿using System.Collections;
using System.Collections.Generic;
using System;
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
        
        for (int i = 0; i < 4; i++)
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

    //private void OnDrawGizmos()
    //{
    //    foreach(Vector3 pos in enemiesSpawnPos)
    //        Gizmos.DrawCube(pos, Vector3.one);
    //}
}
