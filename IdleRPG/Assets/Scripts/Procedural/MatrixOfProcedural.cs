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

    public void HitPointOfMatrix(Vector3 _vector, float _radiusPath, float _radiusEnviroment)//hacer en el otro scipt lo de crear un collider para el terreno
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        matrixVertexProcedural[l_x][l_z].transitable = true;

        float l_incrementAngles =  5f / (_radiusPath + _radiusEnviroment);
        float currentAngles = 0;
        List<Vector2Int> l_positions = new List<Vector2Int>();
        int l_xPos = 0;
        int l_zPos = 0;
        for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
        {
            l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * _radiusPath) + l_x;
            l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * _radiusPath) + l_z;
            l_positions.Add(new Vector2Int(l_xPos, l_zPos));
        }

        foreach (var item in l_positions)
        {
            int multiplierX = 1;
            if (item.x - l_x > 0)
                multiplierX = -1;
            for (int i = item.x; i != l_x; i += multiplierX)
            {
                if (i < matrixVertexProcedural.Count && item.y < matrixVertexProcedural[0].Count && i >= 0 && item.y >= 0)
                    matrixVertexProcedural[i][item.y].transitable = true;
            }

            if (item.x == l_x)
            {
                int multiplierZ = 1;
                if (item.y - l_z > 0)
                    multiplierZ = -1;
                for (int i = item.y; i != l_z; i += multiplierZ)
                {
                    if (item.x < matrixVertexProcedural.Count && i < matrixVertexProcedural[0].Count && item.x >= 0 && i >= 0)
                        matrixVertexProcedural[item.x][i].transitable = true;
                }
            }

        }

        l_positions.Clear();
        currentAngles = 0;
        for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
        {
            l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * (_radiusEnviroment + _radiusPath)) + l_x;
            l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * (_radiusEnviroment + _radiusPath)) + l_z;
            l_positions.Add(new Vector2Int(l_xPos, l_zPos));
        }

        foreach (var item in l_positions)
        {
            int multiplierX = 1;
            if (item.x - l_x > 0)
                multiplierX = -1;
            for (int i = item.x; i != l_x; i += multiplierX)
            {
                if (i < matrixVertexProcedural.Count && item.y < matrixVertexProcedural[0].Count && i >= 0 && item.y >= 0)
                {
                    if (matrixVertexProcedural[i][item.y].transitable == true)
                        break;
                    else if(matrixVertexProcedural[i][item.y].typeOfVertex != 5)
                    {
                        matrixVertexProcedural[i][item.y].typeOfVertex = 5;
                    }
                }
            }

            if (item.x == l_x)
            {
                int multiplierZ = 1;
                if (item.y - l_z > 0)
                    multiplierZ = -1;
                for (int i = item.y; i != l_z; i += multiplierZ)
                {
                    if (item.x < matrixVertexProcedural.Count && i < matrixVertexProcedural[0].Count && item.x >= 0 && i >= 0)
                    {
                        if (matrixVertexProcedural[item.x][i].transitable == true)
                            break;
                        else if(matrixVertexProcedural[item.x][i].typeOfVertex != 5)
                            matrixVertexProcedural[item.x][i].typeOfVertex = 5;
                    }
                }
            }

        }


    }

    public void RubishMode(Vector3 _vector, float _radiusPath)
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        matrixVertexProcedural[l_x][l_z].transitable = false;

        float l_incrementAngles = 5f / (_radiusPath);
        float currentAngles = 0;
        List<Vector2Int> l_positions = new List<Vector2Int>();
        int l_xPos = 0;
        int l_zPos = 0;
        for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
        {
            l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * _radiusPath) + l_x;
            l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * _radiusPath) + l_z;
            l_positions.Add(new Vector2Int(l_xPos, l_zPos));
        }

        foreach (var item in l_positions)
        {
            int multiplierX = 1;
            if (item.x - l_x > 0)
                multiplierX = -1;
            for (int i = item.x; i != l_x; i += multiplierX)
            {
                if (i < matrixVertexProcedural.Count && item.y < matrixVertexProcedural[0].Count && i >= 0 && item.y >= 0)
                {
                    matrixVertexProcedural[i][item.y].transitable = false;
                    matrixVertexProcedural[i][item.y].typeOfVertex = 0;
                }
            }

            if (item.x == l_x)
            {
                int multiplierZ = 1;
                if (item.y - l_z > 0)
                    multiplierZ = -1;
                for (int i = item.y; i != l_z; i += multiplierZ)
                {
                    if (item.x < matrixVertexProcedural.Count && i < matrixVertexProcedural[0].Count && item.x >= 0 && i >= 0)
                    {
                        matrixVertexProcedural[item.x][i].transitable = false;
                        matrixVertexProcedural[item.x][i].typeOfVertex = 0;
                    }
                }
            }

        }
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
                    if (matrixVertexProcedural[i][j].typeOfVertex == 5)//por poner numero adecuado
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]), Vector3.one / 5);
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]), Vector3.one / 5);
                    }
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
