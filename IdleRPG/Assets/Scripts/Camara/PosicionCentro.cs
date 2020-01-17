using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionCentro : MonoBehaviour
{

    public GameObject Champ_01;
    public GameObject Champ_02;
    public GameObject Champ_03;
    public GameObject Champ_04;
    public GameObject Champ_05;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Corutina2");
        transform.position = Calcular_Punto();
    }

    Vector3 Calcular_Punto()
    {
        Vector3 Resultado = new Vector3(0f, 0f, 0f);
        
        Resultado.x = (Champ_01.transform.position.x + Champ_02.transform.position.x + Champ_03.transform.position.x + Champ_04.transform.position.x + Champ_05.transform.position.x)/5;
        Resultado.y = (Champ_01.transform.position.y + Champ_02.transform.position.y + Champ_03.transform.position.y + Champ_04.transform.position.y + Champ_05.transform.position.y)/5;
        Resultado.z = (Champ_01.transform.position.z + Champ_02.transform.position.z + Champ_03.transform.position.z + Champ_04.transform.position.z + Champ_05.transform.position.z)/5;

        return Resultado;
    }

    IEnumerator Corutina2 ()
    {

        yield return null;
    }
}
