using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider_2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Centro;
    private float DistanciaColliderX;
    private float DistanciaColliderZ;
    private Vector3 Resultado;

    void Start()
    {
        Resultado = new Vector3(0f, 0f, 0f);
        DistanciaColliderX = -4;
        DistanciaColliderZ = 7;
    }

    private void Update()
    {
        Resultado.x = Centro.transform.position.x + DistanciaColliderX;
        Resultado.z = Centro.transform.position.z - DistanciaColliderZ;
        Resultado.y = transform.position.y;
        transform.position = Resultado;
    }

    void OnTriggerEnter(Collider _Collision)
    {
        print("Adios buenas tardes");
        //Vector3 PosicionPrueba = new Vector3(0f, 0f, 10f);
        _Collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //_Collision.gameObject.transform.position = PosicionPrueba;

    }
}
