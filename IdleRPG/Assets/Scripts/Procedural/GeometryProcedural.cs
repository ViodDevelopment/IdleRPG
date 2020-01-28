using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MatrixOfProcedural))]

public class GeometryProcedural : MonoBehaviour
{
    [HideInInspector]
    private static int sizeX = 0, sizeZ = 0;
    private static int density = 0;
    private static int precisionProcedural = 0;
    private static string nameMesh;
    public Mesh myMesh;
    private Vector3[] vertices;
    private int[] triangles;


    public void PassAllNums(int _xSize, int _zSize, int _density, int _precision, string _name)
    {
        sizeX = _xSize;
        sizeZ = _zSize;
        density = _density;
        precisionProcedural = _precision;
        nameMesh = _name;
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
                vertices[num] = new Vector3((float)l_x / density, 0, (float)l_z / density) + gameObject.transform.position;
            }
        }
        myMesh.vertices = vertices;
        CreateAllTriangles();
    }

    private void CreateAllTriangles()//crea los triangulos de la mesh
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
        print("cantidad de vertices: " + vertices.Length + " cantidad de tris: " + triangles.Length / 6);
        myMesh.triangles = triangles;
        myMesh.RecalculateNormals();
        myMesh.name = nameMesh;

        GetComponent<MeshFilter>().mesh = myMesh;

        CreateMatrixProcedural();
    }

    private void CreateMatrixProcedural()
    {
        int totalSizeX = sizeX * precisionProcedural;
        int totalSizeZ = sizeZ * precisionProcedural;

        MatrixOfProcedural matrixProcedural = gameObject.GetComponent<MatrixOfProcedural>();

        matrixProcedural.matrixVertexProcedural.Clear();

        float x = gameObject.transform.position.x, z = gameObject.transform.position.z;
        for (int numX = 0; numX <= totalSizeX; numX++)
        {
            matrixProcedural.matrixVertexProcedural.Add(new List<VertexProcedural>());
            for (int numZ = 0; numZ <= totalSizeZ; numZ++)
            {
                matrixProcedural.matrixVertexProcedural[numX].Add(new VertexProcedural());
                matrixProcedural.matrixVertexProcedural[numX][numZ].positionsInFloats[0] = x + gameObject.transform.position.x;
                matrixProcedural.matrixVertexProcedural[numX][numZ].positionsInFloats[1] = 0 + gameObject.transform.position.y;
                matrixProcedural.matrixVertexProcedural[numX][numZ].positionsInFloats[2] = z + gameObject.transform.position.z;

                matrixProcedural.matrixVertexProcedural[numX][numZ].posXMatrix = numX;
                matrixProcedural.matrixVertexProcedural[numX][numZ].posZMatrix = numZ;
                matrixProcedural.matrixVertexProcedural[numX][numZ].typeOfVertex = 0;
                matrixProcedural.matrixVertexProcedural[numX][numZ].transitable = false;
                z += 1f / precisionProcedural;
            }
            z = gameObject.transform.position.z;
            x += 1f / precisionProcedural;
        }
         
        CreateBinaryFromMatrix();
    }

    private void CreateBinaryFromMatrix()//hace una matriz imaginaria con x precisión y la guarda en binario
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.streamingAssetsPath + "/" + nameMesh + ".dat");

        MatrixToSave datos = new MatrixToSave();
        datos.SetMatrixOfVertexProcedural(gameObject.GetComponent<MatrixOfProcedural>().matrixVertexProcedural);
        datos.precision = precisionProcedural;
        bf.Serialize(file, datos);

        file.Close();

    }
}

[Serializable]
public class MatrixToSave
{
    private List<List<VertexProcedural>> matrixProcedural = new List<List<VertexProcedural>>();
    public int precision;

    public List<List<VertexProcedural>> GetMatrixOfVertexProcedural()
    {
        return matrixProcedural;
    }

    public void SetMatrixOfVertexProcedural(List<List<VertexProcedural>> _matrix)
    {
        matrixProcedural.Clear();
        matrixProcedural = _matrix;
    }
}


