using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AllyController : MonoBehaviour
{
   
    enum States { IDLE, MOVING, FIGHTING, DEAD };

    States currentState = 0;

    #region Combat

    [Header("Cosas que si salen")]
    public int health, energy, basicAttackDmg;

    public float range;

    public float basicAttackCooldown;


    [Header("Cosas que solo son de debug")]

    public List<GameObject> enemiesInRange = null;

    public GameObject target = null;

    public EnemyController targetScript = null;

    public bool isAttacking = false;

    public int hitsTaken = 0, hitsDealt = 0;


    public void RecieveDmg(int dmg)
    {
        health -= dmg;

        hitsTaken += 1;

        if (health <= 0)
        {
            CancelInvoke();
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

        hitsDealt += 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,255,0,10);
        
        Gizmos.DrawWireSphere(transform.position, range);

    }


}
