using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPrimitive : MonoBehaviour
{
    private InfoAllies myInfoAlly;
    public int currentHP;
    public int maxHP;
    public int currentAtack;
    public int currentDef;
    public int currentLevel;
    public int expNextLvl;
    public float currentAtackSpeed;

    private void Start()
    {

    }

    private void SetStats()//llamarlo cuando se inicia o sube de lvl
    {//Antes de entrar aqui tiene q haber pasado el progreso del personaje y pasarle el lvl que tiene de cada uno
        if (myInfoAlly != null)
        {
            maxHP = myInfoAlly.lifeList[currentLevel];
            currentHP = maxHP;
            currentDef = myInfoAlly.defensList[currentLevel];
            currentAtack = myInfoAlly.atackList[currentLevel];
            currentAtackSpeed = myInfoAlly.atackSpeed;
        }
    }

    public void AddExp(int _exp)
    {
        if (currentLevel < myInfoAlly.maxLevel)
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 1, myInfoAlly.maxLevel);
            SetStats();
        }
    }
}
