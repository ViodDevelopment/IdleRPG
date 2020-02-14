using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NibikiAbilities : MonoBehaviour 
{
    public List<GameObject> listOfEnemies = new List<GameObject>(); // sustituirlo por la lista de enemigos
    public List<GameObject> listOfAllies = new List<GameObject>();
    public float maxAngle;
    public float maxDistance;
    public float Timer4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
public class AlteracionTemporal : SpecialAbility
{
    public List<EnemyController> listOfEnemies;

    public AlteracionTemporal(string _name, float _cooldown, int _dmg, int _energy, List<EnemyController> _listOfEnemies) : base(_name, _cooldown, _dmg, _energy)
    {
        listOfEnemies = _listOfEnemies;
    }
    public override void UseAb()
    {
        EnemyController _enemigo = null;
        foreach (var item in listOfEnemies)
        {
            //item.atackVel
            //item.movVel
        }
    }
}

public class UltimoAliento : SpecialAbility
{
    public List<AllyController> listOfAllies;
    public float timer; 
    public UltimoAliento(string _name, float _cooldown, int _dmg, int _energy,  List<AllyController> _listOfAllies, float _timer ) : base(_name, _cooldown, _dmg, _energy)
    {
        listOfAllies = _listOfAllies;
        timer = _timer;
    }
    public override void UseAb()
    {
        
        foreach (var item in listOfAllies)
        {
            if(item.currentHP <= 0)
            {
                
                for (float Timer= timer;  Timer >= 0f; Timer-= Time.deltaTime )
                {
                    if (Timer <= 0)
                    {
                        item.CancelInvoke();
                        item.currentState = AllyPrimitive.StatesAlly.DEAD;
                    }

                }
            }
        }
    }

}

public class RecuperarAlma : SpecialAbility
{
    public List<AllyController> listOfAllies;
    public int vidaCurar;

    public RecuperarAlma(string _name, float _cooldown, int _dmg, int _energy, List<AllyController> _listOfAllies, int _vidaCurar) : base(_name, _cooldown, _dmg, _energy)
    {
        listOfAllies = _listOfAllies;
        vidaCurar = _vidaCurar;
    }
    public override void UseAb()
    {
        AllyController _aliado = null;
        float l_vidaMax = 1000f;

        foreach (var item in listOfAllies)
        {
            float vida = item.currentHP; // coger vida de la lista aliados
            if (vida < l_vidaMax)
            {
                l_vidaMax = vida;
                _aliado = item;
            }
        }
        if (_aliado != null)
        {
            _aliado.RecieveDmg( -vidaCurar);
        }
    }


}

public class CosechaAlmas : SpecialAbility
{
    float timer;
    public CosechaAlmas(string _name, float _cooldown, int _dmg, int _energy, int _timer) : base(_name, _cooldown, _dmg, _energy)
    {
        timer = _timer;
    }
    public override void UseAb()
    {
        for (float Timer = timer; Timer >= 0f; Timer -= Time.deltaTime)
        {
            if (Timer <= 0)
            {
                //destruccion de los muertos
            }

        }
    }
}