using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionCentro : MonoBehaviour
{

    public GameObject champ_01;
    public GameObject champ_02;
    public GameObject champ_03;
    public GameObject champ_04;

    private GameObject[] listaChaps  = new GameObject[4];

    public GameObject frontZone;
    public GameObject backZone;

    private Vector3 zonaRandom;
    private float radioChamp = 3f;
    public int layerMask = 8;
    public bool zonaSinCollider = false;



    // Start is called before the first frame update
    void Start()
    {
        listaChaps[0] = champ_01;
        listaChaps[1] = champ_02;
        listaChaps[2] = champ_03;
        listaChaps[3] = champ_04;

        Comprobador();
    }

    // Update is called once per frame
    void Update()
    {
    
       // transform.position = Calcular_Punto();
    }
    
    void Comprobador()
    {
        for (int i = 0; i<=3; i++)
        {
            if (listaChaps[i].CompareTag("Front"))
            {

                while (!zonaSinCollider)
                {
                    zonaRandom = PuntoRandomBound(frontZone.GetComponent<Collider>().bounds);
                    Posicionamiento(zonaRandom, radioChamp, layerMask);
                }
                zonaSinCollider = false;

                Instantiate(listaChaps[i], zonaRandom, Quaternion.identity);
            }
            else if (listaChaps[i].CompareTag("Back"))
            {
                zonaRandom = PuntoRandomBound(backZone.GetComponent<Collider>().bounds);

                while (!zonaSinCollider)
                {
                    zonaRandom = PuntoRandomBound(backZone.GetComponent<Collider>().bounds);
                    Posicionamiento(zonaRandom, radioChamp, layerMask);
                }
                zonaSinCollider = false;

                Instantiate(listaChaps[i], zonaRandom, Quaternion.identity);
            }
        }
    }



    public static Vector3 PuntoRandomBound(Bounds _bounds)
    {
        return new Vector3(
             Random.Range(_bounds.min.x, _bounds.max.x),
             1f,
             Random.Range(_bounds.min.z, _bounds.max.z)
            );
    }


    void Posicionamiento( Vector3 _Centro, float _Radio, int _CapaMask)
    {
        int _layer = 1 << _CapaMask;
        Collider[] hitColliders = Physics.OverlapSphere(_Centro, _Radio, _layer);
        print(hitColliders.Length);

        if (hitColliders.Length==0)
        {
          zonaSinCollider = true;
        }
    }

 

}
