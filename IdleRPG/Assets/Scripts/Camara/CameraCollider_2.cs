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
    private float Speed = 0.02f;
    private float Min;
    private float Max = 1f;

    void Start()
    {
        Resultado = new Vector3(0f, 0f, 0f);
        DistanciaColliderX = -6;
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
        //print("Adios buenas tardes");
        StartCoroutine(Fade(_Collision));

    }
    private IEnumerator Fade(Collider _Collision)
    {
        Color color = _Collision.gameObject.GetComponent<Renderer>().material.color;
        Min = color.a;
        for (float ft = Min; ft <= Max; ft += Speed)
        {
            color.a = ft;
            print(color.a);
            _Collision.gameObject.GetComponent<Renderer>().material.color = color;

            yield return null;
        }

    }
}

