using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeometryProcedural))]
public class GeometryProceduralEditor : Editor
{
    private static float xSize = 0, zSize = 0, density = 0;
    private static Mesh myMesh;
    private Vector3[] vertices;
    private int[] triangles;
    public override void OnInspectorGUI()//La función predeterminada del editor
    {
        base.OnInspectorGUI();
        xSize = EditorGUILayout.Slider(xSize, 0.1f, 10f);
        zSize = EditorGUILayout.Slider(zSize, 0.1f, 10f);
        density = EditorGUILayout.Slider(density, 0.01f, 1f);

        GeometryProcedural l_geometryP = (GeometryProcedural)target;

        if (GUILayout.Button("Create Mesh"))
        {
            l_geometryP.PassAllNums(Mathf.FloorToInt(xSize), Mathf.FloorToInt(zSize), density);
            l_geometryP.CreateAllVertexs();
        }
        

    }
}
