using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEspecial : MonoBehaviour
{
    private Collider ObjetoGolpeado;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100) && hit.distance <= 10)
        {
            Vector3 PosicionPrueba = new Vector3(0f, 0f, 10f);
            //print("La distacia es " + hit.distance);
            ObjetoGolpeado = hit.collider;
            print(ObjetoGolpeado.transform.position.x);
            ObjetoGolpeado.transform.position = PosicionPrueba;
            //ObjetoGolpeado.attachedRigidbody.MovePosition(PosicionPrueba);

        }
        Debug.DrawLine(ray.origin, hit.point);

    }
}
