using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    enum States { IDLE, MOVING, FIGHTING, DEAD };

    States currentState = 0;

    #region Combat
    public int health;
    public bool alive = true;

    public int hitsTaken = 0, hitsDealt = 0;

    public float range;
    public List<GameObject> enemiesInRange = null;
    public GameObject target = null;

    public float basicAttackDmg;

    public bool isAttacking = false;


    public void RecieveDmg(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            alive = false;
            currentState = States.DEAD;
        }
    }



    #endregion

    public void UpdateAlly()
    {
        enemiesInRange = Utils.CheckGOInRangeByTag(gameObject, "Enemy", range);
        if (target == null && enemiesInRange != null)
        {
            CancelInvoke();
            target = Utils.GetClosestGOInList(gameObject, enemiesInRange);
        }
        else if (target && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("BasicAttack", 0.5f, 0.5f);
        }

    }

    public void BasicAttack()
    {
        if (target == null) return;
        Debug.Log("Attacking :" + target);


    }


}
