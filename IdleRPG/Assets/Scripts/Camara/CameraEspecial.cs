using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEspecial : MonoBehaviour
{
    public bool avance;
    private Vector3 copiaPosicion;


    // Start is called before the first frame update
    void Start()
    {
        avance = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (avance)
        {
            copiaPosicion = transform.position;
            copiaPosicion.y = 12.5f;
            transform.position = copiaPosicion;
            print(transform.rotation);
            transform.rotation = Change(0.2f,0.4f,-0.1f);

        }
        else
        {
            copiaPosicion = transform.position;
            copiaPosicion.y = 18;
            transform.position = copiaPosicion;
            print(transform.rotation);
            transform.rotation = Change(0.3f, 0.3f, -0.1f);
        }
     
    }

    private static Quaternion Change(float x, float y, float z)
    {
        return new Quaternion(x, y, z, 1);
    }
}
