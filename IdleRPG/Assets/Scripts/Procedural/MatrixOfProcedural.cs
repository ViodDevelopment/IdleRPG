using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ExecuteInEditMode]
public class MatrixOfProcedural : MonoBehaviour
{
    public List<List<VertexProcedural>> matrixVertexProcedural = new List<List<VertexProcedural>>();
    public MatrixOfProcedural[] matrixAdyacent = new MatrixOfProcedural[4];//0 Arriba, 1 derecha, 2 abajo, 3 izquierda
    public bool searchAdyacnetMatrix = false;
    private static string nameMesh;
    private int[] sizeMesh = new int[2];
    public int precisionOfMatrix = 0;
    public Mesh mesh;

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

        if (searchAdyacnetMatrix)
        {
            SearchAdyacentMatrix();
            searchAdyacnetMatrix = false;
        }
    }

    public int[] GetSizeMesh()
    {
        return sizeMesh;
    }

    public void SearchAdyacentMatrix()
    {
        MatrixOfProcedural[] l_matrixOthers = GameObject.FindObjectsOfType<MatrixOfProcedural>();
        MatrixOfProcedural[] l_matrixSubs = new MatrixOfProcedural[4];
        bool adyacent = true;
        foreach (MatrixOfProcedural item in l_matrixOthers)
        {
            if (item != this)
            {
                Vector3 distance = item.transform.position - gameObject.transform.position;
                adyacent = true;

                if (distance.x != 0 && distance.z != 0)
                    adyacent = false;

                if (adyacent)
                {
                    if (distance.x == 0)
                        distance.x = distance.z * 2;
                    if (distance.z == 0)
                        distance.z = distance.x * 2;

                    if (Mathf.Abs(distance.x) < Mathf.Abs(distance.z))
                    {
                        if (distance.x > 0 && distance.x == sizeMesh[0] / 2)
                        {

                            if (matrixAdyacent[1] != null)
                            {
                                if (distance.x <= (matrixAdyacent[1].gameObject.transform.position - gameObject.transform.position).x)
                                {
                                    l_matrixSubs[1] = item;
                                }
                            }

                            else
                                l_matrixSubs[1] = item;
                        }
                        else if (distance.x * -1 == item.GetSizeMesh()[0] / 2)
                        {

                            if (matrixAdyacent[3] != null)
                            {
                                if (distance.x >= (matrixAdyacent[3].gameObject.transform.position - gameObject.transform.position).x)
                                {
                                    l_matrixSubs[3] = item;
                                }
                            }

                            else
                                l_matrixSubs[3] = item;
                        }
                    }
                    else
                    {
                        if (distance.z > 0 && distance.z == sizeMesh[1] / 2)
                        {

                            if (matrixAdyacent[0] != null)
                            {
                                if (distance.z <= (matrixAdyacent[0].gameObject.transform.position - gameObject.transform.position).z)
                                {
                                    l_matrixSubs[0] = item;
                                }
                            }

                            else
                                l_matrixSubs[0] = item;

                        }
                        else if (distance.z * -1 == item.GetSizeMesh()[1] / 2)
                        {
                            if (matrixAdyacent[2] != null)
                            {
                                if (distance.z >= (matrixAdyacent[2].gameObject.transform.position - gameObject.transform.position).z)
                                {
                                    l_matrixSubs[2] = item;
                                }
                            }

                            else
                                l_matrixSubs[2] = item;

                        }
                    }
                }
            }
        }

        matrixAdyacent = l_matrixSubs;
    }

    private void ObtainMatrixFromBinary() //obtiene la matriz del binario
    {
        if (sizeMesh.Length != 2)
            sizeMesh = new int[2];

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.streamingAssetsPath + "/" + nameMesh + ".dat", FileMode.Open);

        MatrixToSave datos = (MatrixToSave)bf.Deserialize(file);

        matrixVertexProcedural = datos.GetMatrixOfVertexProcedural();
        precisionOfMatrix = datos.precision;
        if (datos.size.Length == 2)
        {
            sizeMesh[0] = datos.size[0];
            sizeMesh[1] = datos.size[1];
        }
        file.Close();
    }

    #region Pincel
    public void PrintPath(Vector3 _vector, float _radiusPath, float _radiusEnvironment)//hacer en el otro scipt lo de crear un collider para el terreno
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        if (l_x >= 0 && l_x < matrixVertexProcedural.Count && l_z >= 0 && l_z < matrixVertexProcedural[0].Count)
        {
            matrixVertexProcedural[l_x][l_z].ResetPoint();
            matrixVertexProcedural[l_x][l_z].currentTypeVertex = VertexProcedural.typeOfVertex.PATH;
        }

        float l_incrementAngles = 5f / (_radiusPath + _radiusEnvironment);
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
                    matrixVertexProcedural[i][item.y].ResetPoint();
                    matrixVertexProcedural[i][item.y].currentTypeVertex = VertexProcedural.typeOfVertex.PATH;
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
                        matrixVertexProcedural[item.x][i].ResetPoint();
                        matrixVertexProcedural[item.x][i].currentTypeVertex = VertexProcedural.typeOfVertex.PATH;
                    }
                }
            }

        }

        l_positions.Clear();
        currentAngles = 0;
        for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
        {
            l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * (_radiusEnvironment + _radiusPath)) + l_x;
            l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * (_radiusEnvironment + _radiusPath)) + l_z;
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
                    if (matrixVertexProcedural[i][item.y].currentTypeVertex == VertexProcedural.typeOfVertex.PATH)
                        break;
                    else if (matrixVertexProcedural[i][item.y].currentTypeVertex != VertexProcedural.typeOfVertex.ENVIRONMENT)
                    {
                        matrixVertexProcedural[i][item.y].currentTypeVertex = VertexProcedural.typeOfVertex.ENVIRONMENT;
                        SearchOcupated(i, item.y);
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
                        if (matrixVertexProcedural[item.x][i].currentTypeVertex == VertexProcedural.typeOfVertex.PATH)
                            break;
                        else if (matrixVertexProcedural[item.x][i].currentTypeVertex != VertexProcedural.typeOfVertex.ENVIRONMENT)
                        {
                            matrixVertexProcedural[item.x][i].currentTypeVertex = VertexProcedural.typeOfVertex.ENVIRONMENT;
                            SearchOcupated(item.x, i);
                        }
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
        matrixVertexProcedural[l_x][l_z].ResetPoint();

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
                    matrixVertexProcedural[i][item.y].ResetPoint();
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
                        matrixVertexProcedural[item.x][i].ResetPoint();
                    }
                }
            }

        }
    }


    public void EnvironmentMode(Vector3 _vector, float _radiusEnvironment)
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        if (matrixVertexProcedural[l_x][l_z].currentTypeVertex != VertexProcedural.typeOfVertex.PATH)
        {
            matrixVertexProcedural[l_x][l_z].currentTypeVertex = VertexProcedural.typeOfVertex.ENVIRONMENT;
            SearchOcupated(l_x, l_z);
        }

        float l_incrementAngles = 5f / (_radiusEnvironment);
        float currentAngles = 0;
        List<Vector2Int> l_positions = new List<Vector2Int>();
        int l_xPos = 0;
        int l_zPos = 0;
        for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
        {
            l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * _radiusEnvironment) + l_x;
            l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * _radiusEnvironment) + l_z;
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
                    if (matrixVertexProcedural[i][item.y].currentTypeVertex != VertexProcedural.typeOfVertex.PATH)
                    {
                        matrixVertexProcedural[i][item.y].currentTypeVertex = VertexProcedural.typeOfVertex.ENVIRONMENT;
                        SearchOcupated(i, item.y);

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
                        if (matrixVertexProcedural[item.x][i].currentTypeVertex != VertexProcedural.typeOfVertex.PATH)
                        {
                            matrixVertexProcedural[item.x][i].currentTypeVertex = VertexProcedural.typeOfVertex.ENVIRONMENT;
                            SearchOcupated(item.x, i);
                        }
                    }
                }
            }

        }
    }

    public void CleanPath(Vector3 _vector, float _radiusPath)
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        if (matrixVertexProcedural[l_x][l_z].currentTypeVertex == VertexProcedural.typeOfVertex.PATH)
            matrixVertexProcedural[l_x][l_z].ResetPoint();

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
                    if (matrixVertexProcedural[i][item.y].currentTypeVertex == VertexProcedural.typeOfVertex.PATH)
                        matrixVertexProcedural[i][item.y].ResetPoint();
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
                        if (matrixVertexProcedural[item.x][i].currentTypeVertex == VertexProcedural.typeOfVertex.PATH)
                            matrixVertexProcedural[item.x][i].ResetPoint();
                    }
                }
            }

        }
    }

    public void CleanEnvironment(Vector3 _vector, float _radiusEnviromnet)
    {
        Vector3 l_localPos = transform.InverseTransformPoint(_vector);
        int l_x;
        int l_z;
        l_localPos *= precisionOfMatrix;
        l_x = Mathf.RoundToInt(l_localPos.x);
        l_z = Mathf.RoundToInt(l_localPos.z);
        if (matrixVertexProcedural[l_x][l_z].currentTypeVertex == VertexProcedural.typeOfVertex.ENVIRONMENT)
            matrixVertexProcedural[l_x][l_z].ResetPoint();

        float l_incrementAngles = 5f / (_radiusEnviromnet);
        float currentAngles = 0;
        List<Vector2Int> l_positions = new List<Vector2Int>();
        int l_xPos = 0;
        int l_zPos = 0;
        for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
        {
            l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * _radiusEnviromnet) + l_x;
            l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * _radiusEnviromnet) + l_z;
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
                    if (matrixVertexProcedural[i][item.y].currentTypeVertex == VertexProcedural.typeOfVertex.ENVIRONMENT)
                        matrixVertexProcedural[i][item.y].ResetPoint();
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
                        if (matrixVertexProcedural[item.x][i].currentTypeVertex == VertexProcedural.typeOfVertex.ENVIRONMENT)
                            matrixVertexProcedural[item.x][i].ResetPoint();
                    }
                }
            }

        }
    }

    public void CleanAll()
    {
        for (int i = 0; i < matrixVertexProcedural.Count; i++)
        {
            for (int j = 0; j < matrixVertexProcedural[i].Count; j++)
            {
                matrixVertexProcedural[i][j].ResetPoint();
            }
        }
    }

    #endregion

    public void SaveMatrix()
    {
        gameObject.GetComponent<GeometryProcedural>().CreateBinaryFromMatrix();
        //UnityEditor.EditorApplication.SaveScene();
    }

    private bool SearchOcupated(int _x, int _z)
    {
        if (!matrixVertexProcedural[_x][_z].ocupated && matrixVertexProcedural[_x][_z].currentTypeVertex == VertexProcedural.typeOfVertex.ENVIRONMENT)
        {
            GameObject _go = new GameObject();
            _go.AddComponent<MeshFilter>().mesh = mesh;
            _go.AddComponent<MeshRenderer>();
            _go.name = matrixVertexProcedural[_x][_z].positionsInFloats[0] + " " + matrixVertexProcedural[_x][_z].positionsInFloats[1] + " " + matrixVertexProcedural[_x][_z].positionsInFloats[2];
            matrixVertexProcedural[_x][_z].nameGO = _go.name;
            _go.transform.position = new Vector3(matrixVertexProcedural[_x][_z].positionsInFloats[0], matrixVertexProcedural[_x][_z].positionsInFloats[1], matrixVertexProcedural[_x][_z].positionsInFloats[2]);
            matrixVertexProcedural[_x][_z].ocupated = true;
            matrixVertexProcedural[_x][_z].myGameObject = true;
            _go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-5,5), 0, Random.Range(-5,5)),Vector3.up);
            //dependiendo de lo grande y la densidad cambiar el 4
            float l_radius = mesh.bounds.max.magnitude * matrixVertexProcedural[_x][_z].density + 3.5f;
            float l_incrementAngles = 5f / (l_radius);
            float currentAngles = 0;
            List<Vector2Int> l_positions = new List<Vector2Int>();
            int l_xPos = 0;
            int l_zPos = 0;
            for (int i = 0; currentAngles < 360; currentAngles += l_incrementAngles)
            {
                l_xPos = Mathf.RoundToInt(Mathf.Cos(currentAngles) * l_radius) + _x;
                l_zPos = Mathf.RoundToInt(Mathf.Sin(currentAngles) * l_radius) + _z;
                l_positions.Add(new Vector2Int(l_xPos, l_zPos));
            }

            foreach (var item in l_positions)
            {
                int multiplierX = 1;
                if (item.x - _x > 0)
                    multiplierX = -1;
                for (int i = item.x; i != _x; i += multiplierX)
                {
                    if (i < matrixVertexProcedural.Count && item.y < matrixVertexProcedural[0].Count && i >= 0 && item.y >= 0)
                    {
                        matrixVertexProcedural[i][item.y].ocupated = true;
                        matrixVertexProcedural[i][item.y].parentOcupated = matrixVertexProcedural[_x][_z];
                        matrixVertexProcedural[_x][_z].myVertexsX.Add(i);
                        matrixVertexProcedural[_x][_z].myVertexsZ.Add(item.y);

                    }
                }

                if (item.x == _x)
                {
                    int multiplierZ = 1;
                    if (item.y - _z > 0)
                        multiplierZ = -1;
                    for (int i = item.y; i != _z; i += multiplierZ)
                    {
                        if (item.x < matrixVertexProcedural.Count && i < matrixVertexProcedural[0].Count && item.x >= 0 && i >= 0)
                        {
                            matrixVertexProcedural[item.x][i].ocupated = true;
                            matrixVertexProcedural[item.x][i].parentOcupated = matrixVertexProcedural[_x][_z];
                            matrixVertexProcedural[_x][_z].myVertexsX.Add(item.x);
                            matrixVertexProcedural[_x][_z].myVertexsZ.Add(i);
                        }
                    }
                }

            }
            return false;
        }
        return true;
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < matrixVertexProcedural.Count; i++)
        {
            for (int j = 0; j < matrixVertexProcedural[i].Count; j++)
            {

                if (matrixVertexProcedural[i][j].currentTypeVertex == VertexProcedural.typeOfVertex.ENVIRONMENT)//por poner numero adecuado
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]) + gameObject.transform.position, Vector3.one / 5);
                }
                else if (matrixVertexProcedural[i][j].currentTypeVertex == VertexProcedural.typeOfVertex.NONE)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]) + gameObject.transform.position, Vector3.one / 5);
                }
                else if (matrixVertexProcedural[i][j].currentTypeVertex == VertexProcedural.typeOfVertex.PATH)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]) + gameObject.transform.position, Vector3.one / 5);
                }

                if (matrixVertexProcedural[i][j].ocupated)
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawCube(new Vector3(matrixVertexProcedural[i][j].positionsInFloats[0], matrixVertexProcedural[i][j].positionsInFloats[1], matrixVertexProcedural[i][j].positionsInFloats[2]) + gameObject.transform.position, Vector3.one / 5);
                }
            }
        }
    }

}
