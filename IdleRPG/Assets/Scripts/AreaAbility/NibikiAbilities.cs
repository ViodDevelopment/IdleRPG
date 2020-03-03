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
    public float  reduc; // tanto por ciento de reduccion 

    public AlteracionTemporal(string _name, int _dmg, int _energy, float _attackSpeed, List<EnemyController> _listOfEnemies, float _reduc) : base(_name, _dmg, _energy, _attackSpeed)
    {
        listOfEnemies = _listOfEnemies;
        reduc = _reduc;
    }
    public override void UseAb()
    {
        //EnemyController _enemigo = null;
        foreach (var item in listOfEnemies)
        {
            item.basicAttackCooldown -= item.basicAttackCooldown*reduc; // reduccion velocidad de ataque
        
            //item.movVel
        }
    }
}

public class UltimoAliento : SpecialAbility
{
    public List<AllyController> listOfAllies;
    public float timer; 
    public UltimoAliento(string _name, int _dmg, int _energy, float _attackSpeed,  List<AllyController> _listOfAllies, float _timer ) : base(_name, _dmg, _energy, _attackSpeed)
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

    public RecuperarAlma(string _name, int _dmg, int _energy, float _attackSpeed, List<AllyController> _listOfAllies, int _vidaCurar) : base(_name, _dmg, _energy, _attackSpeed)
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
    public float timer;
    public GameObject NoMuerto;
    
    public CosechaAlmas(string _name,  int _dmg, int _energy, float _attackSpeed, int _timer, GameObject _NoMuerto) : base(_name,  _dmg, _energy, _attackSpeed)
    {
        timer = _timer;
        NoMuerto = _NoMuerto;
    }
    public override void UseAb()
    {
        GameObject l_noMuerto1;
        GameObject l_noMuerto2;

        l_noMuerto1 = Instantiate(NoMuerto, new Vector3 (transform.position.x + 3, transform.position.y, transform.position.z-2), transform.rotation);
        l_noMuerto2 = Instantiate(NoMuerto, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z + 2), transform.rotation);

        for (float Timer = timer; Timer >= 0f; Timer -= Time.deltaTime)
        {
            if (Timer <= 0)
            {
                Destroy(l_noMuerto1);
                Destroy(l_noMuerto2);
                //destruccion de los muertos
            }

        }
    }
}