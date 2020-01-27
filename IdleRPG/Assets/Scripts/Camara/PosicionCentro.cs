using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionCentro : MonoBehaviour
{

    public GameObject Champ_01;
    public GameObject Champ_02;
    public GameObject Champ_03;
    public GameObject Champ_04;

    private GameObject[] ListaChamps  = new GameObject[4];

    public GameObject FrontZone;
    public GameObject BackZone;

    

   
    // Start is called before the first frame update
    void Start()
    {
        ListaChamps[0] = Champ_01;
        ListaChamps[1] = Champ_02;
        ListaChamps[2] = Champ_03;
        ListaChamps[3] = Champ_04;

        Comprobador();
    }

    // Update is called once per frame
    void Update()
    {
    
       // transform.position = Calcular_Punto();
    }
    /*
    Vector3 Calcular_Punto()
    {
        Vector3 Resultado = new Vector3(0f, 0f, 0f);

        Resultado.x = (Champ_01.transform.position.x + Champ_02.transform.position.x + Champ_03.transform.position.x + Champ_04.transform.position.x + Champ_05.transform.position.x) / 5;
        Resultado.y = (Champ_01.transform.position.y + Champ_02.transform.position.y + Champ_03.transform.position.y + Champ_04.transform.position.y + Champ_05.transform.position.y) / 5;
        Resultado.z = (Champ_01.transform.position.z + Champ_02.transform.position.z + Champ_03.transform.position.z + Champ_04.transform.position.z + Champ_05.transform.position.z) / 5;

        return Resultado;
    }*/
    void Comprobador()
    {
        for (int i = 0; i<=3; i++)
        {
            if (ListaChamps[i].CompareTag("Front"))
            {
                //antes de pasarle la ubicacion hacer lo del overlapSphere


                Instantiate(ListaChamps[i], PuntoRandomBound(FrontZone.GetComponent<Collider>().bounds), Quaternion.identity);
            }
            else if (ListaChamps[i].CompareTag("Back"))
            {
                Instantiate(ListaChamps[i], PuntoRandomBound(BackZone.GetComponent<Collider>().bounds), Quaternion.identity);
            }
        }
    }
    public static Vector3 PuntoRandomBound(Bounds _bounds)
    {
        return new Vector3(
             Random.Range(_bounds.min.x, _bounds.max.x),
            // Random.Range(_bounds.min.y, _bounds.max.y),
                1f,
             Random.Range(_bounds.min.z, _bounds.max.z)
            );
    }


    void Posicionamiento()
    {

    }

    /*
     * private Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }*/

/*
 *  void ExplosionDamage(Vector3 center, float radius)
{
    Collider[] hitColliders = Physics.OverlapSphere(center, radius);
    int i = 0;
    while (i < hitColliders.Length)
    {
        hitColliders[i].SendMessage("AddDamage");
        i++;
    }
}
*/

}
