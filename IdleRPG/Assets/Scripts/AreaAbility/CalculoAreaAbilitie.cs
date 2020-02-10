using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculoAreaAbilitie : MonoBehaviour, Abilities
{

    private Vector3 Punto0;
    public List<GameObject> listOfEnemies = new List<GameObject>();
    public float maxAngle;
    public float maxDistance;

  
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ability1();
        }
    }

  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.color = Color.blue;
    }

    public void Ability1()
    {
        float l_distancia1;
        float l_distancia2= 100f;
        GameObject _enemigo = null;
        if (gameObject.GetComponent<AllyController>() != null)
            _enemigo = gameObject.GetComponent<AllyController>().target; 
        if (_enemigo == null)
        {
                foreach (var item in listOfEnemies)
                {
                    l_distancia1 = (item.transform.position - Punto0).magnitude;
                    if (l_distancia1 < l_distancia2)
                    {
                        l_distancia2 = l_distancia1;
                    _enemigo = item;
                        print(_enemigo);
                    }
                }
                print("no hay ningun enemigo cerca");
        }
        else {
            print("Le pego 3 veces seguidas a "+ _enemigo); // aplicar 3 veces el ataque 

        }


    }
    public void Ability2()
    {

    }
    public void Ability3()
    {
        foreach (var item in listOfEnemies)
        {
            //print("La distancia entre el punto 1 y punto 0 es" + (item.transform.position - Punto0).magnitude);

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

    }
}
