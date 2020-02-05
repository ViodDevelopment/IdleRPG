using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public string name;
    public float cooldown;
    public int dmg;
    public int energyRequired;
    

    public SpecialAbility(string _name , float _cooldown, int _dmg, int _energy)
    {
        name = _name;
        cooldown = _cooldown;
        dmg = _dmg;
        energyRequired = _energy;
    }
    public virtual void UseAb()
    {
        Debug.Log("Hab used");
    }

}
