using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NibikiAbilities : MonoBehaviour , Abilities
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
    public void Ability1()
    {
       // a todos los enemigos les reduce su velocidad de ataque y de mov 
    }

    public void Ability2()
    {
        // si muere un aliado dura unos segundos mas
    }

    public void Ability3()
    {
        // sana al aliado con menos vida
        float l_vidaMax = 1000f; // vida maxima global
        GameObject _Allie = null;
        foreach (var item in listOfEnemies)
        {
            float vida =0/*= item.GetComponent<>()*/; // coger vida de la lista aliados
            if (vida < l_vidaMax)
            {
                l_vidaMax = vida;
               _Allie = item;
            }
        }
        if (_Allie!= null)
        {
            // sumar vida al GameObject
            print("Le curo vida a "+_Allie); 
        }

    }

    public void Ability4()
    {
       // levanta a dos muertos para que luchen pero solo durante unos pocos segundos


    }
}
