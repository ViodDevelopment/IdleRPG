using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> listPath = new List<Transform>();

    public void UpdateCamino()
    {
        List<bool> existentes = new List<bool>();
        bool modify = false;

        for (int i = 0; i < listPath.Count; i++)
        {
            if (listPath[i] == null)
            {
                modify = true;
                existentes.Add(false);
            }
            else
            {
                existentes.Add(true);
            }
        }
        if (modify)
        {
            for (int i = existentes.Count - 1; i >= 0; i--)
            {
                if (!existentes[i])
                {
                    listPath.RemoveAt(i);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {

        for (int i = 0; i < listPath.Count; i++)
        {
            if (listPath[i] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(listPath[i].position, Vector3.one);

                if (i + 1 < listPath.Count)
                {
                    if(listPath[i+1] != null)
                        Gizmos.DrawLine(listPath[i].position, listPath[i + 1].position);
                }
            }
        }

        UpdateCamino();//provisional

    }
}
