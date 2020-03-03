using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyuugaoAbilities : AllyController
{

    private Vector3 Punto0;
    public List<GameObject> listOfEnemies = new List<GameObject>(); // sustituirlo por la lista de enemigos
    public float maxAngle;
    public float maxDistance;
    public float def;
    public int contadorAliados;




    void Start()
    {
        
    }

    public override void Initialize()
    {
        specialAbilities = new SpecialAbility[numOfAb];
        //specialAbilities[0] = new GolpeNaginata();
    }

    void Update()
    {
        
    }



    
}

public class RugidoDragon: SpecialAbility
{
    public List<EnemyController> listOfEnemies;
    public float maxAngle;
    public float maxDistance;

    public RugidoDragon(string _name, int _dmg, int _energy, float _attackSpeed , List<EnemyController> _listOfEnemies, float _maxAngle, float _maxDistance) : base(_name, _dmg, _energy, _attackSpeed)
    {
        listOfEnemies = _listOfEnemies;
        maxAngle = _maxAngle;
        maxDistance = _maxDistance;
    }
    public override void UseAb()
    {
        EnemyController _enemigo = null;
        foreach (var item in listOfEnemies)
        {


            float l_angle = Vector3.Angle(item.transform.position - gameObject.transform.position, transform.forward);
            if (l_angle <= maxAngle)
            {

                if ((item.transform.position - gameObject.transform.position).magnitude <= maxDistance)
                {
                    
                    _enemigo = item;
                    if (_enemigo != null)
                    {
                        _enemigo.RecieveDmg(dmg);
                        print("le da a " + item);
                    }
                    
                }
                    
            }
        }
    }
}

public class GolpeNaginata : SpecialAbility 
{
    public List<EnemyController> listOfEnemies;
    RyuugaoAbilities ally;
    public GolpeNaginata(string _name, float _cooldown, int _dmg, int _energy, float _attackSpeed, List<EnemyController> _listOfEnemies, RyuugaoAbilities _ally) : base(_name, _dmg, _energy, _attackSpeed)
    {
        listOfEnemies = _listOfEnemies;
        ally = _ally;

    }

    public override void UseAb()
    {
        float l_distancia1;
        float l_distancia2 = 100f;
        EnemyController _enemigo = null;
        if (gameObject.GetComponent<AllyController>() != null)
            _enemigo = ally.targetScript;

        if (_enemigo == null)
        {
            foreach (EnemyController item in listOfEnemies)
            {
                l_distancia1 = (item.transform.position - gameObject.transform.position ).magnitude;
                if (l_distancia1 < l_distancia2)
                {
                    l_distancia2 = l_distancia1;
                    _enemigo = item;
                    print(_enemigo);
                }
            }
            _enemigo.RecieveDmg(dmg);
            //pegar al enemigo mas cercano
            print("Nuevo enemigo calculado");
        }
        else
        {
            _enemigo.RecieveDmg(dmg);
            print("Le pego 3 veces seguidas a " + _enemigo); // aplicar 3 veces el ataque 
        }
        
    }
    
}


public class Temple : SpecialAbility
{
    public List<AllyController> listOfAllies;
    RyuugaoAbilities ally;
    public Temple(string _name, float _cooldown, int _dmg, int _energy, float _attackSpeed, List<AllyController> _listOfAllies, RyuugaoAbilities _ally) : base(_name, _dmg, _energy, _attackSpeed)
    {
        listOfAllies = _listOfAllies;
        ally = _ally;
    }

    public override void UseAb()
    {
        int l_numMaxAllies = 4;
        int l_numActAllies = listOfAllies.Count;
        int Contador = 0;

        if(listOfAllies.Count == 4)
        {
            Contador = 0;
        }
        if (listOfAllies.Count< l_numActAllies)
        {
            l_numMaxAllies--;
            Contador++;
        }
        float l_cambioInt = ally.maxDef * 0.05f * Contador;
        ally.currentDef += (int)l_cambioInt; 
        
      
    }
}

public class EscamasDragon : SpecialAbility
{
  
    RyuugaoAbilities ally;
    public EscamasDragon(string _name, int _dmg, int _energy, float _attackSpeed, RyuugaoAbilities _ally) : base(_name, _dmg, _energy, _attackSpeed)
    {
        ally = _ally;
    }
    public override void UseAb()
    {
        float l_cambioInt = ally.maxDef * 0.1f;

        ally.currentDef +=  (int) l_cambioInt;
        
    }
}