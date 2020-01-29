using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode]
public class MatrixOfProcedural : MonoBehaviour
{
    public List<List<VertexProcedural>> matrixVertexProcedural = new List<List<VertexProcedural>>();
    private static string nameMesh;
    public int precisionOfMatrix = 0;

    private void Awake()
    {
        if (precisionOfMatrix <= 0 || matrixVertexProcedural.Count <= 0)
        {
            nameMesh = gameObject.GetComponent<GeometryProcedural>().myMesh.name;
            if (File.Exists(Application.streamingAssetsPath + "/" + nameMesh + ".dat"))
            {
                ObtainMatrixFromBinary();
            }
        }

    }

    private void Update()//quitar cuando esté hecho
    {
        if (precisionOfMatrix <= 0 || matrixVertexProcedural.Count <= 0)
        {
            nameMesh = gameObject.GetComponent<GeometryProcedural>().myMesh.name;
            if (File.Exists(Application.streamingAssetsPath + "/" + nameMesh + ".dat"))
                ObtainMatrixFromBinary();
        }
    }

    private void ObtainMatrixFromBinary() //obtiene la matriz del binario
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.streamingAssetsPath + "/" + nameMesh + ".dat", FileMode.Open);

        MatrixToSave datos = (MatrixToSave)bf.Deserialize(file);

        matrixVertexProcedural = datos.GetMatrixOfVertexProcedural();
        precisionOfMatrix = datos.precision;
        file.Close();
    }

    public void HitPointOfMatrix(Vector3 _vector)//hacer en el otro scipt lo de crear un collider para el terreno
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        matrixVertexProcedural[l_x][l_z].transitable = true;
    }

    public void SaveMatrix()
    {
        gameObject.GetComponent<GeometryProcedural>().CreateBinaryFromMatrix();
        //UnityEditor.EditorApplication.SaveScene();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < matrixVertexProcedural.Count; i++)
        {
            for (int j = 0; j < matrixVertexProcedural[i].Count; j++)
            {
                if (!matrixVertexProcedural[i][j].transitable)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]), Vector3.one / 5);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]), Vector3.one / 5);
                }
            }
        }
    }

}
