using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    enum States { IDLE, MOVING, FIGHTING, DEAD };

    #region Combat
    public int health;
    public bool alive = true;

    public int hitsTaken = 0, hitsDealt = 0;

    public float range;
    public List<GameObject> enemiesInRange;

    public float basicAttackDmg;


    public void RecieveDmg(int dmg)
    {
        health -= dmg;

        if (health <= 0) alive = false;
    }



    #endregion

    public void UpdateAlly()
    {

    }


}
