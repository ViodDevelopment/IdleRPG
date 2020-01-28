using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    enum States { IDLE, MOVING, FIGHTING, DEAD };

    States currentState = 0;

    #region Combat
    public int health, energy, basicAttackDmg;

    public int hitsTaken = 0, hitsDealt = 0;

    public float range;

    public List<GameObject> enemiesInRange = null;

    public GameObject target = null;

    public EnemyController targetScript = null;

    public float basicAttackCooldown;

    public bool isAttacking = false;


    public void RecieveDmg(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            currentState = States.DEAD;
        }
    }



    #endregion

    public void UpdateAlly()
    {
        if (enemiesInRange.Count == 0)
        {
            enemiesInRange = Utils.CheckGOInRangeByTag(gameObject, "Enemy", range);
        }
        else if (target == null || target.activeSelf == false)
        {
            CancelInvoke();
            isAttacking = false;
            enemiesInRange = Utils.CheckGOInRangeByTag(gameObject, "Enemy", range);
            target = Utils.GetClosestGOInList(gameObject, enemiesInRange);
            try { targetScript = target.GetComponent<EnemyController>(); } catch { Debug.Log("Couldn't get enemy controller from = " + target); }

        }
        else if (target && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("BasicAttack", 0, basicAttackCooldown);
        }

    }

    public void BasicAttack()
    {
        if (target == null)
        {
            CancelInvoke();
            return;
        }
        Debug.Log("Attacking :" + target);
        targetScript.RecieveDmg(basicAttackDmg);


    }


}
