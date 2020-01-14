using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryProcedural : MonoBehaviour
{
    [HideInInspector]
    private static int xSize = 0, zSize = 0;
    private float density = 0;
    private Mesh myMesh;
    private Vector3[] vertices;
    private int[] triangles;
    public void PassAllNums(int _xSize, int _zSize, float _density)
    {
        xSize = _xSize;
        zSize = _zSize;
        density = _density;
    }

    public void CreateAllVertexs()//Crea los vertices de la mesh dependiendo de los parametros dados
    {
        if (myMesh != null)
        {
            myMesh = null;
            GetComponent<MeshFilter>().mesh = null;
            vertices = null;
            triangles = null;
        }
        myMesh = new Mesh();


        int l_numOfVertex = (xSize + 1) * (zSize + 1);
        vertices = new Vector3[l_numOfVertex];


        for (int k = 0, num = 0; k <= zSize; k++)
        {
            for (int i = 0; i <= xSize; i++)
            {
                vertices[num] = new Vector3(i, 0, k);
                num++;
            }
        }

        myMesh.vertices = vertices;
        CreateAllTriangles();
    }

    private void CreateAllTriangles()
    {
        triangles = new int[xSize * zSize * 6];
        int l_vert = 0;
        int l_tris = 0;

        for (int k = 0; k < zSize; k++)
        {
            for (int i = 0; i < xSize; i++)
            {
                triangles[l_tris + 0] = l_vert + 0;
                triangles[l_tris + 1] = l_vert + xSize + 1;
                triangles[l_tris + 2] = l_vert + 1;
                triangles[l_tris + 3] = l_vert + 1;
                triangles[l_tris + 4] = l_vert + xSize + 1;
                triangles[l_tris + 5] = l_vert + xSize + 2;


                l_vert++;
                l_tris += 6;
            }
        }
        myMesh.triangles = triangles;
        myMesh.RecalculateNormals();
        myMesh.name = "My Mesh";

        GetComponent<MeshFilter>().mesh = myMesh;

    }
}
