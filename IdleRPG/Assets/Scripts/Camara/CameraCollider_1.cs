using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider_1 : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject Centro;
    private float DistanciaColliderX;
    private float DistanciaColliderZ;
    private Vector3 Resultado;


    void Start()
    {
        Resultado = new Vector3(0f, 0f , 0f);
        DistanciaColliderX = 4;
        DistanciaColliderZ = 7;
    }

    private void Update()
    {
        Resultado.x= Centro.transform.position.x + DistanciaColliderX;
        Resultado.z= Centro.transform.position.z - DistanciaColliderZ;
        Resultado.y = transform.position.y;
        transform.position = Resultado;
    }
     void OnTriggerEnter (Collider _Collision)
    {
        print("Hola buenos dias");
        Vector3 PosicionPrueba = new Vector3(0f, 0f, 10f);
        
        StartCoroutine(Fade(_Collision));
        
        //_Collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //_Collision.gameObject.transform.position = PosicionPrueba;

    }
    
    private IEnumerator Wait()
    {
        print("Que tal le va a usted");
        yield return new WaitForSeconds(2.0f);
    }

    private IEnumerator Fade ( Collider _Collision)
    {
        for (float ft = 1f; ft>= 0.2; ft -=0.05f)
        {
            Color color = _Collision.gameObject.GetComponent<Renderer>().material.color;
            color.a = ft;
            _Collision.gameObject.GetComponent<Renderer>().material.color = color;
            StartCoroutine(Wait());
        }
        yield return null;
    }
   

}
