using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
