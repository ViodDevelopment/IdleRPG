using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryProcedural : MonoBehaviour
{
    [HideInInspector]
    private static int sizeX = 0, sizeZ = 0;
    private static int density = 0;
    private Mesh myMesh;
    private Vector3[] vertices;
    private int[] triangles;
    public void PassAllNums(int _xSize, int _zSize, int _density)
    {
        sizeX = _xSize;
        sizeZ = _zSize;
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

        sizeX = sizeX * density;
        sizeZ = sizeZ * density;
        int l_numOfVertex = Mathf.Abs((sizeX + 1) * (sizeZ + 1));
        vertices = new Vector3[l_numOfVertex];

        for (int l_z = 0, num = 0; l_z <= sizeZ; l_z++)
        {
            for (int l_x = 0; l_x <= sizeX; l_x++, num++)
            {
                vertices[num] = new Vector3((float)l_x / density, 0, (float)l_z / density);
            }
        }
        myMesh.vertices = vertices;
        CreateAllTriangles();
    }

    private void CreateAllTriangles()
    {
        triangles = new int[sizeX * sizeZ * 6];

        for (int tris = 0, l_indexZ = 0, y = 0; y < sizeZ; y++, l_indexZ++)//Cambiar por mio en un futuro
        {
            for (int l_indexX = 0; l_indexX < sizeX; l_indexX++, tris += 6, l_indexZ++) //cambiar por mio en un futuro
            {
                triangles[tris] = l_indexZ;
                triangles[tris + 1] = l_indexZ + sizeX + 1;
                triangles[tris + 2] = l_indexZ + 1;
                triangles[tris + 3] = l_indexZ + 1;
                triangles[tris + 4] = l_indexZ + sizeX + 1;
                triangles[tris + 1] = l_indexZ + sizeX + 1;
                triangles[tris + 5] = l_indexZ + sizeX + 2;
            }
        }
        print("cantidad de vertices: " + vertices.Length + " cantidad de tris: " + triangles.Length);
        myMesh.triangles = triangles;
        myMesh.RecalculateNormals();
        myMesh.name = "My Mesh";

        GetComponent<MeshFilter>().mesh = myMesh;

    }
}
