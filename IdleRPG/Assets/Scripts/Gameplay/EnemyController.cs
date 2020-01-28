using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health, basicAttackDamage;

    public float range;

    public float basicAttackCooldown;

    public bool isAttacking = false;

    public List<GameObject> allies = null;

    public GameObject target = null;

    public AllyController targetScript = null;

    public void RecieveDmg(int _dmg)
    {
        health -= _dmg;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void UpdateEnemy()
    {
        if(allies.Count == 0)
        {
            allies = Utils.CheckGOInRangeByTag(gameObject, "Ally", range);
        }
        else if(target == null)
        {
            CancelInvoke();
            isAttacking = false;
            allies = Utils.CheckGOInRangeByTag(gameObject, "Ally", range);
            target = Utils.GetClosestGOInList(gameObject, allies);

            try { targetScript = target.GetComponent<AllyController>(); } catch { Debug.Log("Couldn't get ally controller from = " + target); }
        }
        else if (target && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("BasicAttack", 0, basicAttackCooldown);
        }
    }

    public void BasicAttack()
    {
        if(target == null)
        {
            CancelInvoke();
            return;
        }
        targetScript.RecieveDmg(basicAttackDamage);

    }
}
