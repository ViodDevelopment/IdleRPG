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
        if (Input.GetMouseButtonDown(0))
        {
            //Ability1();
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.color = Color.blue;
    }


    public void Ability2()
    {

        def += def * 0.1f;
        // aumenta un 10% su armadura por sus escamas de dragon

    }
    public void Ability3()
    {

        foreach (var item in listOfEnemies)
        {


            float l_angle = Vector3.Angle(item.transform.position - Punto0, transform.forward);
            if (l_angle <= maxAngle)
            {

                if ((item.transform.position - Punto0).magnitude <= maxDistance)
                    print("le da a " + item);
            }
        }
    }
    public void Ability4()
    {
        int MaxAliados = contadorAliados;

        if (contadorAliados < MaxAliados)
        {
            MaxAliados--;
            def += def * 0.05f;
        }
        //cada vez que un aliado muere.
    }
}

public class RugidoDragon: SpecialAbility
{
    public RugidoDragon(string _name, float _cooldown, int _dmg, int _energy) : base(_name, _cooldown, _dmg, _energy)
    {

    }
}

public class GolpeNaginata : SpecialAbility 
{
    public List<EnemyController> listOfEnemies;
    RyuugaoAbilities ally;
    public GolpeNaginata(string _name, float _cooldown, int _dmg, int _energy, List<EnemyController> _listOfEnemies, RyuugaoAbilities _ally) : base(_name, _cooldown, _dmg, _energy)
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
