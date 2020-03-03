using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public string name;
    public float cooldownBase, currentCooldown;
    public int dmg;
    public int energyRequired;
    

    public SpecialAbility(string _name , int _dmg, int _energy, float _attackSpeed)
    {
        name = _name;
        cooldownBase = _energy * _attackSpeed * 2;
        dmg = _dmg;
        energyRequired = _energy;
    }
    public virtual void UseAb()
    {
        Debug.Log("Hab used");
    }
    private void Start()
    {
        currentCooldown = cooldownBase;
    }


    private void FixedUpdate()
    {
        currentCooldown -= Time.deltaTime;
    }

}
