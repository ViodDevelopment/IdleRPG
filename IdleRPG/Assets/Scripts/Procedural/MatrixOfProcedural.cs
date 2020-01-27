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
    private string nameMesh;
    private void Update()
    {
        if(matrixVertexProcedural.Count <= 0)
        {
            nameMesh = gameObject.GetComponent<GeometryProcedural>().myMesh.name;
            if (File.Exists(Application.streamingAssetsPath + "/" + nameMesh + ".dat"))
                ObtainMatrixFromBinary();
        }

    }

    private void ObtainMatrixFromBinary()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.streamingAssetsPath + "/" + nameMesh + ".dat", FileMode.Open);

        MatrixToSave datos = (MatrixToSave)bf.Deserialize(file);

        matrixVertexProcedural = datos.GetMatrixOfVertexProcedural();

        file.Close();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < matrixVertexProcedural.Count; i++)
        {
            for (int j = 0; j < matrixVertexProcedural[i].Count; j++)
            {
                Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]), Vector3.one / 5);

            }
        }
    }

}
