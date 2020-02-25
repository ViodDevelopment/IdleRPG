using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPrimitive : MonoBehaviour
{
    public enum StatesAlly { IDLE, MOVING, FIGHTING, DEAD };

    public StatesAlly currentState = StatesAlly.IDLE;
    private InfoAllies myInfoAlly;
    public int currentHP;
    public int maxHP;
    public int currentAtack;
    public int currentDef;
    public int maxDef;
    public int currentLevel;
    public int expNextLvl;
    public float currentAtackSpeed;
    public int currentEnergy;
    protected float range;
    protected int hitsTaken = 0, hitsDealt = 0;


    private void Start()
    {
        //recoger los datos del player, despues cargar los stats
        SetStats();
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

            if (myInfoAlly.melee)
            {
                range = 0.5f;
            }
            else
                range = 2.5f;
        }
    }

    public void AddLvl()
    {
        if (currentLevel < myInfoAlly.maxLevel)
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 1, myInfoAlly.maxLevel);
            SetStats();
        }
    }

    public void RecieveDmg(int _dmg)
    {
        currentHP = Mathf.Clamp(currentHP - _dmg, 0, maxHP);

        hitsTaken++;

        if (currentHP == 0)
        {
            CancelInvoke();
            currentState = AllyPrimitive.StatesAlly.DEAD;
        }
    }
}
