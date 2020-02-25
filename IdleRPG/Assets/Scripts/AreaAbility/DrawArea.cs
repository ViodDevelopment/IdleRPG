using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArea : MonoBehaviour
{
   // public int NumeroDeVectores;
    //public Vector3[] Vectores = new Vector3[NumeroDeVectores]; //[] objects = new GameObject[SIZE];
    //public List<Vector3> Vectores = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
       /* GameObject o =  new GameObject();
        o.GetComponent<Abilities>().Ability1();
    */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform target;
    public Transform target2;

    void OnDrawGizmosSelected()
    {
        if (target != null && target2 !=null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawLine(transform.position, target2.position);
        }
    }
}
