using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health, basicAttackDamage;

    public float range;

    public float basicAttackCooldown; //velocidad de ataque

    public bool isAttacking = false;

    public List<GameObject> allies = null;

    public GameObject target = null;

    public AllyController targetScript = null;

    public GameController gc;

    public void Initialize()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void RecieveDmg(int _dmg)
    {
        health -= _dmg;

        if (health <= 0)
        {
            CancelInvoke();
            gameObject.SetActive(false);
            gc.RespawnEnemies();
        }
    }
    public void UpdateEnemy()
    {
        if(allies.Count == 0)
        {
            allies = Utils.CheckGOInRangeByTag(gameObject, "Ally", range);
        }
        else if(target == null || targetScript.currentState == AllyPrimitive.StatesAlly.DEAD)
        {
            CancelInvoke();
            isAttacking = false;
            allies = Utils.CheckGOInRangeByTag(gameObject, "Ally", range);
            target = Utils.GetClosestAllyInList(gameObject, allies);

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
        if(target == null || targetScript.currentState == AllyPrimitive.StatesAlly.DEAD)
        {
            CancelInvoke();
            return;
        }
        targetScript.RecieveDmg(basicAttackDamage);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 10);

        Gizmos.DrawWireSphere(transform.position, range);

    }
}
