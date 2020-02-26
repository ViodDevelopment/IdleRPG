using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllyController : AllyPrimitive
{
   
    #region Combat

    [Header("Cosas que solo son de debug")]

    public List<GameObject> enemiesInRange = null;

    public GameObject target = null;

    public EnemyController targetScript = null;

    public bool isAttacking = false;

    public SpecialAbility[] specialAbilities;

    public int selectedAbility, numOfAb;

    


    public void BasicAttack()
    {
        if (target == null)
        {
            CancelInvoke();
            return;
        }
        Debug.Log("Attacking :" + target);
        targetScript.RecieveDmg(currentAttack);

        hitsDealt++;
    }

    public void SpecialAbility()
    {
        if (specialAbilities[selectedAbility].energyRequired <= currentEnergy)
        {
            specialAbilities[selectedAbility].UseAb();
            currentEnergy -= specialAbilities[selectedAbility].energyRequired;
        }
        else
        {
            //Not enough energy
        }
        Debug.Log("Special ability from" + gameObject.name);
        
    }

    #endregion

    public virtual void Initialize()
    {
        specialAbilities = new SpecialAbility[numOfAb];
       // specialAbilities[0] = new PielFerrea("PielFerrea", 2, 12, 3);
        
    }

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
            InvokeRepeating("BasicAttack", 0, currentAttackSpeed);
        }

    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,255,0,10);
        
        Gizmos.DrawWireSphere(transform.position, range);
    }

    


}


