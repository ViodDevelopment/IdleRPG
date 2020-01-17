using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeometryProcedural))]
public class GeometryProceduralEditor : Editor
{
    private static int sizeX = 0, sizeZ = 0;
    private static int density = 0;
    private static Mesh myMesh;
    private Vector3[] vertices;
    private int[] triangles;
    public override void OnInspectorGUI()//La función predeterminada del editor
    {
        base.OnInspectorGUI();
        sizeX = Mathf.Clamp(EditorGUILayout.IntField("size X",sizeX), 1, 50);
        sizeZ = Mathf.Clamp(EditorGUILayout.IntField("size Z",sizeZ), 1, 50);
        density = Mathf.Clamp(EditorGUILayout.IntField("vertex per unit",density), 1, 4);

        GeometryProcedural l_geometryP = (GeometryProcedural)target;

        if (GUILayout.Button("Create Mesh"))
        {
            l_geometryP.PassAllNums(sizeX, sizeZ, density);
            l_geometryP.CreateAllVertexs();
        }
        

    }
}
